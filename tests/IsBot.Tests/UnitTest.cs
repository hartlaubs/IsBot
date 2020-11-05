using System;
using Xunit;

namespace Damurka.Tests
{
    public class UnitTest
    {
        private readonly string crawlerUserAgent = "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; Googlebot/2.1; +http://www.google.com/bot.html) Safari/537.36";
        private readonly string browserUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

        [Fact]
        public void IdentifyABotCorrectly()
        {
            var isBot = IsBot.Matches(crawlerUserAgent);
            Assert.True(isBot);
        }

        [Fact]
        public void ExcludesBrowserFromDetection()
        {
            IsBot.Reset();
            var isBot = IsBot.Matches(browserUserAgent);
            Assert.False(isBot);
        }

        [Fact]
        public void ExcludeBotFromDetection()
        {
            IsBot.Exclude("google", "bot", "http");
            var isBot = IsBot.Matches(crawlerUserAgent);
            Assert.False(isBot);
        }

        [Fact]
        public void IncludeBrowserAsBot()
        {
            IsBot.Extend("chrome");
            var isBot = IsBot.Matches(browserUserAgent);
            Assert.True(isBot);
        }
    }
}
