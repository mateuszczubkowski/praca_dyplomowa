using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Data.Models;
using CourierApp.WebApp.Dto.Courier;

namespace CourierApp.WebApp.Mapper
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<GetCourierListDto, Courier>();
        }
    }
}
