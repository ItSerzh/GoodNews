using System;

namespace NewsAnalyzer.Core.DataTransferObjects
{
    public class NewsDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public float? Rating { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime DateCollect { get; set; }
        public string Body { get; set; }

        public Guid RssSourceId { get; set; }
    }
}
