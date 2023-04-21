using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace chatgpt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPTController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> UseChatGpt(string query)
        {
            string outPutResult = "";

            //API Configuration
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var openAIConfig = config.GetSection("OpenAI");
            var apiKey = openAIConfig.GetValue<string>("ApiKey");

            // API Object
            var openai = new OpenAIAPI(apiKey);

            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = query;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

            //Send solicitation and wait answer
            var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                outPutResult += completion.Text;
            }
            //Result
            return Ok(outPutResult);
        }
    }
}
