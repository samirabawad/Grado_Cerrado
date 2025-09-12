using Google.Cloud.AIPlatform.V1;
using Grado_Cerrado.Application.Interfaces;

namespace Grado_Cerrado.Infrastructure.Services;

public class GeminiAILegalTutorService : IAILegalTutorService
{
    private readonly PredictionServiceClient _predictionServiceClient;
    private readonly string _modelName;
    private const string _projectId = "default"; // Usar "default" con API Key

    public GeminiAILegalTutorService(IConfiguration configuration)
    {
        var apiKey = configuration["Gemini:ApiKey"]; // Lee la API Key de config
        _modelName = configuration["Gemini:ModelName"] ?? "gemini-1.5-flash-001";

        _predictionServiceClient = new PredictionServiceClientBuilder
        {
            ApiKey = apiKey
        }.Build();
    }

    public async Task<string> GetLegalExplanationAsync(string question, string userAnswer, string correctAnswer, CancellationToken cancellationToken = default)
    {
        var prompt = $"""
        Eres un tutor experto en derecho chileno. 
        Analiza la siguiente respuesta del estudiante y proporciona una explicación jurídica concisa y didáctica.

        PREGUNTA: {question}

        RESPUESTA DEL ESTUDIANTE: {userAnswer}

        RESPUESTA CORRECTA: {correctAnswer}

        EXPLICACIÓN:
        """;

        var generateContentRequest = new GenerateContentRequest
        {
            Model = $"models/{_modelName}",
            Contents =
            {
                new Content
                {
                    Role = "user",
                    Parts = { new Part { Text = prompt } }
                }
            },
            GenerationConfig = new GenerationConfig
            {
                Temperature = 0.3f, // Baja temperatura para respuestas más deterministicas y jurídicas
                MaxOutputTokens = 800,
                TopP = 0.8f
            }
        };

        try
        {
            var response = await _predictionServiceClient.GenerateContentAsync(generateContentRequest, cancellationToken);
            return response.Candidates[0].Content.Parts[0].Text;
        }
        catch (Exception ex)
        {
            // Loggear error aquí
            throw new ApplicationException("Error al obtener explicación de la IA", ex);
        }
    }
}
