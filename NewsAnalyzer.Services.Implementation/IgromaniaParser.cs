using HtmlAgilityPack;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class IgromaniaParser : IWebPageParser
    {
        public async Task<string> Parse(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var node = htmlDoc.DocumentNode
                .SelectSingleNode("//div[contains(@class, 'page_news') and contains(@class, 'noselect')]");

            node = HtmlCleanup.RemoveElementsByXpath(node,
                new[] {
                    "//meta",
                    "//div[contains(@class, 'share_block')]",
                    "//div[contains(@class, 'favorite_block')]",
                    "//div[contains(@class, 'news_info')]",
                    "//div[contains(@class, 'vn-player')]",
                    "//div[contains(@class, 'uninote console')]",
                    "//div[contains(@class, 'nepncont')]",
                }
                );
            return node?.InnerHtml;
        }
    }
}
