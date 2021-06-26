using HtmlAgilityPack;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class ForPdaParser : IWebPageParser
    {
        public async Task<string> Parse(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var node = htmlDoc.DocumentNode
                .SelectSingleNode("//div[@class = 'container']");

            node = HtmlCleanup.RemoveElementsByXpath(node,
                new[] {
                    "//meta",
                    "//div[@class='more-box']",
                    "//p[@class='mb_source']"
                }
                );

            return node?.InnerHtml;
        }
    }
}
