using System.Collections.Generic;
using System.Threading.Tasks;
using TriviaModels.OutputModels;

namespace TriviaOrchestratorContract
{
    public interface ITriviaOrchestrator
    {
        Task<List<Trivia>> GetTrivia();
    }
}