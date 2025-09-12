using Microsoft.AspNetCore.Mvc;
using Grado_Cerrado.Infrastructure.Services;
using Qdrant.Client.Grpc;
using System.Text.Json;

namespace Grado_Cerrado.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QdrantController : ControllerBase
    {
        private readonly QdrantService _qdrant;

        public QdrantController(QdrantService qdrant)
        {
            _qdrant = qdrant;
        }

        public record InsertarRequest(string Texto);
        public record BuscarRequest(string Consulta, int TopK = 3);

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] InsertarRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Texto))
                return BadRequest("Texto vacío.");

            var vector = Enumerable.Repeat(0.5f, 1536).ToArray();

            var point = new PointStruct
            {
                Id = (ulong)DateTime.UtcNow.Ticks, 
                Vectors = vector, 
                Payload =
                {
                    ["texto"] = req.Texto,
                    ["fuente"] = "prueba",
                    ["tema"] = "demo",
                    ["jurisdiccion"] = "CL"
                }
                        };


            await _qdrant.UpsertBatchAsync(new[] { point });

            return Ok(new { ok = true, msg = "Insertado en Qdrant" });
        }

        [HttpPost("buscar")]
        public async Task<IActionResult> Buscar([FromBody] BuscarRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Consulta))
                return BadRequest("Consulta vacía.");

            var queryVector = Enumerable.Repeat(0.5f, 1536).ToArray();

            var results = await _qdrant.SearchAsync(
                query: queryVector,
                k: Math.Max(1, req.TopK),
                filter: null
            );

            var data = results.Select(r => new
            {
                Id = r.Id.ToString(), // Por si es PointId.Number
                r.Score,
                Texto = r.Payload.TryGetValue("texto", out var t) ? t.StringValue : ""
            });

            return Ok(data);
        }

        [HttpDelete("borrar-todo")]
        public async Task<IActionResult> BorrarTodo()
        {
            await _qdrant.BorrarColeccionAsync();
            return Ok(new { ok = true, msg = "Colección eliminada y recreada con éxito" });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var results = await _qdrant.SearchAsync(
                query: Enumerable.Repeat(0.5f, 1536).ToArray(),  // dummy query
                k: 1000,  // cantidad máxima a traer
                filter: null
            );

            var agrupados = results
                .Select(r => new
                {
                    Materia = r.Payload.TryGetValue("materia", out var m) ? m.ToString() : "Desconocida",
                    Texto = r.Payload.TryGetValue("texto", out var t) ? t.ToString()?.Substring(0, Math.Min(300, t.ToString()?.Length ?? 0)) + "..." : "(sin texto)",
                    Fuente = r.Payload.TryGetValue("fuente", out var f) ? f.ToString() : "(sin fuente)"
                })
                .GroupBy(x => x.Materia)
                .ToDictionary(g => g.Key, g => g.ToList());

            return Ok(agrupados);
        }


    }
}
