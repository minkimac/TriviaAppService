using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using TriviaApi.Models;
using TriviaModels.OutputModels;
using TriviaOrchestratorContract;

namespace TriviaOrchestratorImplementation
{
    public class TriviaOrchestratorImpl: ITriviaOrchestrator
    {
        public async Task<List<Trivia>> GetTrivia()
        {
            try
            {
                string triviaJsonPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TriviaConfig\TriviaData.json");
                string jsonString = await File.ReadAllTextAsync(triviaJsonPath);
                List<Trivia> trivia = JsonSerializer.Deserialize<List<Trivia>>(jsonString);
                return trivia;
            }
            catch(JsonException ex)
            {
                throw new TriviaException($"Reading data source failed with message: {ex.Message}");
            }
            catch(Exception ex)
            {
                throw new TriviaException($"An error occured with message: {ex.Message}");
            }
        }
    }
}
