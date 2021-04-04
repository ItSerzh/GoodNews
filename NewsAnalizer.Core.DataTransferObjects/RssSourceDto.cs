using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.Core.DataTransferObjects
{
    public class RssSourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
