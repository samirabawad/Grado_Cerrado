namespace Grado_Cerrado.Application.Use_Cases;

using Grado_Cerrado.Application.Interfaces;

public class GenerateExplanationUseCase
{
    private readonly IAILegalTutorService _aiLegalTutorService;

    public GenerateExplanationUseCase(IAILegalTutorService aiLegalTutorService)
    {
        _aiLegalTutorService = aiLegalTutorService;
    }

    public async Task<string> Execute(string question, string userAnswer, string correctAnswer)
    {
        return await _aiLegalTutorService.GetLegalExplanationAsync(question, userAnswer, correctAnswer);
    }
}