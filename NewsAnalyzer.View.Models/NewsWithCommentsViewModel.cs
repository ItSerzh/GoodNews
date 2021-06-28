using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.View
{
    public class NewsWithCommentsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public float? Rating { get; set; }
        public DateTime NewsDate { get; set; }
        public Guid RssSourceId { get; set; }
        public string RssSourceName { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
