﻿using AutoMapper;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Services.Implementation.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();

            CreateMap<NewsWithRssSourceNameDto, NewsViewModel>()
                .ForSourceMember(src => src.Body, opt => opt.DoNotValidate());

            CreateMap<RssSource, RssSourceDto>();
            CreateMap<RssSourceDto, RssSource>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();

            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();

            CreateMap<News, NewsWithRssSourceNameDto >()
                .ForMember(dest => dest.RssSourceName, opt => opt.MapFrom(src => src.RssSource.Name));
        }
    }
}
