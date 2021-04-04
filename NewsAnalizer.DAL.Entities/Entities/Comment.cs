using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastChangeDate { get; set; }

        public Guid NewsId { get; set; }
        public virtual News News { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
