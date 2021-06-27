using System.Collections.Generic;

namespace NewsAnalyzer.Models.View
{
    public class NewsListWithPaginationInfo
    {
        public IEnumerable<NewsViewModel> NewsPerPage { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
