using System;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Completions;

namespace chatConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Key Configuration
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var openAIConfig = config.GetSection("OpenAI");
            var apiKey = openAIConfig.GetValue<string>("ApiKey");

            //Object
            var openai = new OpenAIAPI(apiKey);

            // Loop
            while (true)
            {

                Console.Write("Você: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input == "\u001b") // ESC
                    break;

                var request = new CompletionRequest
                {
                    Model = OpenAI_API.Models.Model.DavinciText,
                    Prompt = input,
                    Temperature = 0.5,
                    MaxTokens = 120
                };

                var completions = await openai.Completions.CreateCompletionAsync(request);
                var response = completions.Completions[0].Text;

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("ChatGpt: ");
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine(response);

                Console.ResetColor();
            }
        }
    }
}