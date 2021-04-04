using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.ViewModels
{
    public class NewsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime DateCollect { get; set; }

        public Guid RssSourceId { get; set; }
        public string RssSourceName  { get; set; }
    }
}
