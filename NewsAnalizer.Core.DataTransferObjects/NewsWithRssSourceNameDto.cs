using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.Core.DataTransferObjects
{
    public class NewsWithRssSourceNameDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime DateCollect { get; set; }

        public Guid RssSourceId { get; set; }
        public string RssSourceName { get; set; }
    }
}
