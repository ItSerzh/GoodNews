using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface IRssSourceService
    {
        Task<IEnumerable<RssSourceDto>> GetRssSources(Guid? id = null);
    }
}
