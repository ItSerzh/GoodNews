using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.Core.DataTransferObjects
{
    public class RssSourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
