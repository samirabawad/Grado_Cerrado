namespace Grado_Cerrado.Application.Interfaces
{
    public interface IAILegalTutorService
    {
        Task<string> GetLegalExplanationAsync(string question, string userAnswer, string correctAnswer, CancellationToken cancellationToken = default);
    }
}
