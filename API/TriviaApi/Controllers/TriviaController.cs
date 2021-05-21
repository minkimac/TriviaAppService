using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TriviaApi.Models;
using TriviaModels.OutputModels;
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

        [ProducesResponseType(typeof(List<Trivia>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TriviaException), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TriviaException), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TriviaException), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetTrivia()
        {
            var trivia = await _triviaOrchestrator.GetTrivia();
            return Ok(trivia);
        }
    }
}
