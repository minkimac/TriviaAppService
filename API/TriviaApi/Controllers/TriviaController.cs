using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TriviaOrchestratorContract;

namespace TriviaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly ILogger<TriviaController> _logger;
        private readonly ITriviaOrchestrator _triviaOrchestrator;

        public TriviaController(ILogger<TriviaController> logger, ITriviaOrchestrator triviaOrchestrator)
        {
            _logger = logger;
            _triviaOrchestrator = triviaOrchestrator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrivia()
        {
            var trivia = await _triviaOrchestrator.GetTrivia();
            return Ok(trivia);
        }
    }
}
