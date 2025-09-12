using Grado_Cerrado.Application.Interfaces;
using Grado_Cerrado.Application.Use_Cases;
using Grado_Cerrado.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAILegalTutorService, GeminiAILegalTutorService>();
builder.Services.AddScoped<GenerateExplanationUseCase>();

// ⬅️ Registro del servicio Qdrant
builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var url = cfg["Qdrant:Url"];
    var key = cfg["Qdrant:ApiKey"];
    var svc = new QdrantService(url!, key!);

    // 🧼 Borrar si ya existe
    svc.BorrarColeccionAsync().GetAwaiter().GetResult();

    // ✅ Crear nueva vacía
    svc.EnsureCollectionAsync().GetAwaiter().GetResult();

    return svc;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
