using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Interfaces.Services
{
    public interface IRssSourceService
    {
        Task<IEnumerable<RssSourceDto>> FindRssSource();
        Task<RssSourceDto> GetRssSourceById(Guid? id);
        Task<int> AddRssSource(RssSourceDto rssSourceDto);
        Task<IEnumerable<RssSourceDto>> AddRange(IEnumerable<RssSourceDto> rssSourceDto);
        Task<int> Edit(RssSourceDto rssSourceDto);
        Task<int> Delete(RssSourceDto rssSourceDto);
    }
}
