using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Core.Interfaces.Services
{
    public interface IWebPageParser
    {
        Task<string> Parse(string url);
    }
}
