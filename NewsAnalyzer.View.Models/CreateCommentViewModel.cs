using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.View
{

    public class CreateCommentViewModel
    {
        public Guid NewsId { get; set; }
        public string CommentText { get; set; }
    }

}
