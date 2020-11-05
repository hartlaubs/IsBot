using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Damurka
{
    public static class IsBot
    {
        private static readonly string json = File.ReadAllText("list.json");
        private static List<string> jsonList = JsonSerializer.Deserialize<List<string>>(json);

        private static Regex regex;

        static IsBot()
        {
            Update();
        }

        private static void Update()
        {
            regex = new Regex($"({string.Join("|", jsonList)})", RegexOptions.IgnoreCase);
        }

        public static bool Matches(string userAgent)
        {
            return regex.IsMatch(userAgent);
        }

        public static string Find(string userAgent)
        {
            var match = regex.Match(userAgent);
            return match.Value;
        }

        public static void Extend(params string[] additionalFilters)
        {
            foreach (var filter in additionalFilters)
            {
                var find = Find(filter);
                if (find != filter.ToLower())
                    jsonList.Add(filter);
            }

            Update();
        }

        public static void Exclude(params string[] excludedFilters)
        {
            foreach (var filter in excludedFilters)
            {
                var rx = new Regex(filter, RegexOptions.IgnoreCase);
                var index = jsonList.Find(x => rx.IsMatch(x));
                if (index != default)
                    jsonList.Remove(index);
            }

            Update();
        }

        public static void Reset()
        {
            jsonList = JsonSerializer.Deserialize<List<string>>(json);
            Update();
        }
    }
}
