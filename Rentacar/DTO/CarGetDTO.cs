using System;
using Rentacar.Constants.Enums;

namespace Rentacar.DTO
{
    public class CarGetDTO
    {
        public Guid CarId { get; set; }
        
        public string Macer { get; set; }
        
        public string Model { get; set; }
        
        public string KiloMeter { get; set; }
        
        public FuelType FuelType { get; set; }
        
        public Gear Gear { get; set; }
        
        public CarType CarType { get; set; }
        
        public string Color { get; set; }
    }
}