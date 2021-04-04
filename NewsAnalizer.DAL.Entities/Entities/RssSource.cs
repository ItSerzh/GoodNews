using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class RssSource
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<News> NewsCollection { get; set; }
    }
}
