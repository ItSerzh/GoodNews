using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.View
{
    public class CommentsViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
    }
}
