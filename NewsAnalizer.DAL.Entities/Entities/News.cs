using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class News
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime DateCollect { get; set; }

        public Guid RssSourceId { get; set; }
        public virtual RssSource RssSource { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
