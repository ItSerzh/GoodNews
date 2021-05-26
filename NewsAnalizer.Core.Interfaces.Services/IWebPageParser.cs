using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Interfaces.Services
{
    public interface IWebPageParser
    {
        Task<string> Parse(string url);
    }
}
