using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace AAISaveChatFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Setup
            var key = Environment.GetEnvironmentVariable("AzureOpenAI-Key");
            var endpoint = new Uri("https://systementoropenaiinstance.openai.azure.com/");
            var deploymentName = "Systementor-o4-mini";
            var client = new AzureOpenAIClient(endpoint, new AzureKeyCredential(key));

            ChatClient chatClient = client.GetChatClient(deploymentName);

            List<ChatMessage> messages = new List<ChatMessage>()
            {
                new SystemChatMessage("Your name is Samuel L Jackson."),
                new SystemChatMessage("You are a hollywood actor."),
                new SystemChatMessage("You can only answer in american english. Think about your spelling"),
                new SystemChatMessage("I want you to start every response with 'Hello Bitchass fool!'."),
                new SystemChatMessage("I want you to end every response with 'Mutha Fukka!'."),
                new UserChatMessage("Mitt namn är Richard."),
            };

            Console.WriteLine("Skriv 'exit' för att avsluta");

            // Start the chat loop
            while (true)
            {
                Console.Write("Q: ");
                var userInput = Console.ReadLine();

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (string.IsNullOrEmpty(userInput)) break;
                messages.Add(new UserChatMessage(userInput));
                var response = chatClient.CompleteChat(messages);

                var reply = response.Value.Content[0].Text;
                messages.Add(new AssistantChatMessage(reply));

                Console.WriteLine("A: " + response.Value.Content[0].Text);
            }

            
        }
    }
}
