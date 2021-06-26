using HtmlAgilityPack;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class ShazooParser : IWebPageParser
    {
        public async Task<string> Parse(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var headerNode = htmlDoc.DocumentNode
                .SelectSingleNode("//div[contains(@class, 'entryContextHeader clearfix')]");

            headerNode = HtmlCleanup.RemoveElementsByXpath(headerNode,
                new[] {
                    "//meta",
                    "//div[contains(@class, 'byline')]",
                    "//div[contains(@class, 'favorite_block')]",
                    "//div[contains(@class, 'news_info')]",
                    "//div[contains(@class, 'vn-player')]",
                    "//div[contains(@class, 'uninote console')]",
                    "//div[contains(@class, 'nepncont')]",
                }
                );

            var contentNode = htmlDoc.DocumentNode
              .SelectSingleNode("//div[@id = 'contentWrapper']");

            contentNode = HtmlCleanup.RemoveElementsByXpath(contentNode,
                new[] {
                    "//meta",
                    "//script",
                    "//section[@class = 'related']",
                    "//section[@class ='sources entryMeta']",
                    "//section[@class = 'tags clearfix entryMeta']",
                    "//div[contains(@class, 'mtl mbl')]",
                    "//div[@id = 'commentsContainer']",
                    "//aside[@id = 'aside']"
                    //"//div[contains(@class, 'nepncont')]",
                }
                );
            return $"{headerNode?.InnerHtml}{contentNode?.InnerHtml}";
        }
    }
}
