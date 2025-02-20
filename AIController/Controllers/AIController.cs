using Microsoft.AspNetCore.Mvc;
using AIManager.Services;
using AIModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AIController.Controllers
{


    [ApiController]
    [Route("api/ai")]
    public class AIController : ControllerBase
    {
        private readonly AIService _aiService;

        public AIController(AIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskAI([FromBody] AIRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest("The question cannot be empty.");
            }

            try
            {
                string aiResponse = await _aiService.GetAIResponseAsync(request.Question);
                return Ok(new { response = aiResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error communicating with AI: {ex.Message}");
            }
        }
    }

}
