using AutoMapper;
using MicroserviceProj.Basket.Api.Data;
using MicroserviceProj.Basket.Api.Dtos;

namespace MicroserviceProj.Basket.Api.Features.Baskets
{
    public class BasketMapping:Profile
    {
        public BasketMapping() 
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
