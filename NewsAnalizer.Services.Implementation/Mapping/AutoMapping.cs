﻿using AutoMapper;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.Services.Implementation.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();

            CreateMap<News, NewsWithRssSourceNameDto >()
                .ForMember(dest => dest.RssSourceName, opt => opt.MapFrom(src => src.RssSource.Name)); 
        }
    }
}