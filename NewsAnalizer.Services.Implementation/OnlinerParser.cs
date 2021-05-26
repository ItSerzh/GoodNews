﻿using HtmlAgilityPack;
using NewsAnalizer.Core.Interfaces.Services;
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
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='news-text']");

            return node.InnerHtml;
        }
    }
}
