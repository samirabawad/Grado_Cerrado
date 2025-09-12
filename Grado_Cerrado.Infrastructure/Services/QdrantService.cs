using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace Grado_Cerrado.Infrastructure.Services;

public class QdrantService
{
    private readonly QdrantClient _client;
    private readonly string _collection;

    public QdrantService(string url, string apiKey, string collection = "material_juridico")
    {
        var uri = new Uri(url);
        var host = uri.Host;
        var useTls = uri.Scheme == "https";
        var port = uri.IsDefaultPort ? 6334 : uri.Port; // gRPC por defecto

        _client = new QdrantClient(host, port, useTls, apiKey);
        _collection = collection;
    }

    public async Task EnsureCollectionAsync(int dim = 1536)
    {
        try
        {
            await _client.CreateCollectionAsync(
                collectionName: _collection,
                vectorsConfig: new VectorParams
                {
                    Size = (ulong)dim,
                    Distance = Distance.Cosine
                }
            );
            Console.WriteLine($"✅ Colección `{_collection}` creada.");
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            Console.WriteLine($"⚠️ La colección `{_collection}` ya existe. Se omite la creación.");
            // No hacemos nada: ya existe
        }
    }



    public Task UpsertBatchAsync(IEnumerable<PointStruct> points)
        => _client.UpsertAsync(_collection, points.ToArray());

    public Task<IReadOnlyList<ScoredPoint>> SearchAsync(float[] query, int k = 5, Filter? filter = null)
        // ✅ Usa parámetros con nombre para respetar el orden de la versión instalada
        => _client.SearchAsync(
            collectionName: _collection,
            vector: query,
            filter: filter,
            limit: (ulong)k
        );

    public async Task BorrarColeccionAsync()
    {
        await _client.DeleteCollectionAsync(_collection);
        await EnsureCollectionAsync(); // recrea la colección vacía
    }



}
