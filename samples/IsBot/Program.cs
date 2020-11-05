using Damurka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = await File.ReadAllTextAsync("crawler-user-agents.json");
            var crawlers = JsonSerializer.Deserialize<List<Agent>>(file);

            //var isBot = IsBot.Find("Chrome-Lighthouse");
            //IsBot.Exclude("Chrome-Lighthouse");
            //isBot = IsBot.Find("Chrome-Lighthouse");

            // IsBot.Exclude("http", "bot", "google");
            IsBot.Extend("chrome");
            foreach (var crawler in crawlers)
            {
                var counter = 0;
                Console.WriteLine($"{crawler.Pattern} has {crawler.Instances.Count} instances");
                foreach (var instance in crawler.Instances)
                {
                    if (IsBot.Matches(instance))
                        counter++;
                }
                Console.WriteLine($"{crawler.Pattern} found {counter} matches");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
