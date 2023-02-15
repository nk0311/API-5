using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Joke
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("setup")]
        public string Setup { get; set; }

        [JsonProperty("punchline")]
        public string Punchline { get; set; }

    }

    //public class Type
    //{
    //    [JsonProperty("name")]
    //    public string Name { get; set; }
    //}


    class Program
    {

        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {

                    Console.WriteLine("Enter any character to retrieve a joke. Press enter to quit");
                    var jokeName = Console.ReadLine();
                    if (string.IsNullOrEmpty(jokeName))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://official-joke-api.appspot.com/random_joke");
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var joke = JsonConvert.DeserializeObject<Joke>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Joke: " + joke.Id);
                    Console.WriteLine("Type: " + joke.Type);
                    Console.WriteLine("Setup: " + joke.Setup);
                    Console.WriteLine("Punchline: " + joke.Punchline);
                 
                    Console.WriteLine("\n---");
                }

                catch (Exception)
                {
                    Console.WriteLine("Error. The joke you are looking for cannot be found. Please try Again!");
                }
            }
        }
    }

}




