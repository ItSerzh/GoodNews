﻿using AutoMapper;
using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalyzer.DAL.CQRS;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class RssSourceCqsService : IRssSourceService
    {
        private IMapper _mapper;
        private IMediator _mediator;

        public RssSourceCqsService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<RssSourceDto>> GetRssSources(Guid? id = null)
        {
            var getRssSourceByIdQuery = new GetRssSourceByIdQuery(id);
            
            return await _mediator.Send(getRssSourceByIdQuery);
        }
    }
}
