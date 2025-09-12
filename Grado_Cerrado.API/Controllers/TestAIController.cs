using Grado_Cerrado.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grado_Cerrado.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAIController : ControllerBase
{
    private readonly IAILegalTutorService _aiLegalTutorService;

    public TestAIController(IAILegalTutorService aiLegalTutorService)
    {
        _aiLegalTutorService = aiLegalTutorService;
    }

    [HttpPost("explain")]
    public async Task<IActionResult> TestExplanation([FromBody] TestAIModel model)
    {
        try
        {
            var explanation = await _aiLegalTutorService.GetLegalExplanationAsync(
                model.Question,
                model.UserAnswer,
                model.CorrectAnswer
            );

            return Ok(new { Success = true, Explanation = explanation });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Error = ex.Message });
        }
    }

    public class TestAIModel
    {
        public string Question { get; set; }
        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}