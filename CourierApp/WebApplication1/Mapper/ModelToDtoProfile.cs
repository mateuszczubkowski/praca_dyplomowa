using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Data.Models;
using CourierApp.WebApp.Dto.Courier;

namespace CourierApp.WebApp.Mapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile(IReviewService reviewService)
        {
            CreateMap<Courier, GetCourierListDto>().ForMember(dest => dest.Mark, opt => opt.MapFrom(src => reviewService.GetCourierAvgMark(src.Id)));
        }
    }
}
