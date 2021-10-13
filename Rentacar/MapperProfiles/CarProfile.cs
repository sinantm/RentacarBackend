using AutoMapper;
using Rentacar.DTO;
using Rentacar.Models;

namespace Rentacar.MapperProfiles
{
    public class CarProfile:Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDTO>();
            CreateMap<CarDTO, Car>();
            CreateMap<Car, CarGetDTO>();
            CreateMap<CarGetDTO, CarGetDTO>();
        }
        
    }
}