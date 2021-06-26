using NewsAnalyzer.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.Services.Implementation
{
    public class WebPageParse
    {
        public delegate IWebPageParser ServiceResolver(string key);
    }
}
