using HtmlAgilityPack;
using NewsAnalizer.Core.Interfaces.Services;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class OnlinerParser : IWebPageParser
    {
        public async Task<string> Parse(string url)
        {
            
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var node = htmlDoc.DocumentNode.SelectNodes("(//div[@class='news-container'])[1]")[0];

            //var retVal = HtmlCleanup.ReadElemtnsUntil(node, "<hr>");

            node = HtmlCleanup.RemoveElementsByXpath(node,
                new[] {
                    "//meta",
                    "//script",
                    "//div[contains(@class,'js-banner-container')]",
                    "//div[contains(@class, 'news-reference')]",
                    "//div[contains(@class, 'news-popular')]",
                    "//div[contains(@class, 'news-discussion')]",
                    "//div[contains(@class, 'news-widget')]",
                    "//div[@class='news-header__flex']",
                    "//div[contains(@class, 'news-incut')]",
                    "(//p)[last()]",
                    "(//p)[last()]"
                }
                );

            node = HtmlCleanup.ReplaceBackgroundImageWithImg(node, "//div[@class='news-header__image']");

            return node?.InnerHtml;
        }
    }
}
