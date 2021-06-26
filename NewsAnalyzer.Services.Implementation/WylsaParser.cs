using HtmlAgilityPack;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class WylsaParser : IWebPageParser
    {
        public async Task<string> Parse(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var node = htmlDoc.DocumentNode
                .SelectSingleNode("//article[1]");

            node = HtmlCleanup.RemoveElementsByXpath(node,
                new[] {
                    "//meta",
                    "//script",
                    "//div[@class='headline__stamps']",
                    "//div[@class='embeded-post-info']",
                    "//div[@class='source sa-source-wrapper']"
                });

            node = HtmlCleanup.ReplaceBackgroundImageWithImg(node, "//section[@class='article__img']");

            return node?.InnerHtml;
        }
    }
}
