using NewsAnalizer.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class IgromaniaParser : IWebPageParser
    {
        public Task<string> Parse(string url)
        {
            throw new NotImplementedException();
        }
    }
}
