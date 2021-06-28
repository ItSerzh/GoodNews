using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers.Requests
{
    public class CommentRequest
    {
        public string Text { get; set; }
        public Guid NewsId { get; set; }
    }
}
