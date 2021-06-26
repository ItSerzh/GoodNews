using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
    }
}
