﻿using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetRefreshTokenByTokenValueQuery : IRequest<RefreshTokenDto>
    {
        public string TokenValue { get; set; }
    }
}
