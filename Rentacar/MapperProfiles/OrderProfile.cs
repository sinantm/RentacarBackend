using AutoMapper;
using Rentacar.DTO;
using Rentacar.Models;

namespace Rentacar.MapperProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
            
            CreateMap<OrderGetDTO, Order>();
            CreateMap<Order, OrderGetDTO>();
        }
    }
}