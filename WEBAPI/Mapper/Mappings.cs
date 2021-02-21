using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Models;
using WEBAPI.Models.Dtos;

namespace WEBAPI
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<PaymentState, PaymentStateDto>().ReverseMap();
        }
    }
}
