using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data.Models;

namespace CourierApp.WebApp.Mapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile(IReviewService reviewService)
        {
            CreateMap<Courier, CourierListItemViewModel>().ForMember(dest => dest.Mark, opt => opt.MapFrom(src => reviewService.GetCourierAvgMark(src.Id)));
        }
    }
}
