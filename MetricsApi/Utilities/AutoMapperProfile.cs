using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MetricsApi.DataAccess.EntityModels;
using MetricsApi.Models;
using MetricsApi.Dtos;

namespace MetricsApi.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, m => m.MapFrom(src => src.PasswordHash));
            CreateMap<UserDto, User>();

            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
        }
    }
}
