using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rentacar.Authentication;
using Rentacar.Constants.Enums;

namespace Rentacar.Models
{
    public class Car
    {
        [Required]
        public Guid CarId { get; set; }
        
        [Required]
        public string Macer { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        [Required]
        public string KiloMeter { get; set; }
        
        [Required]
        public FuelType FuelType { get; set; }
        
        [Required]
        public Gear Gear { get; set; }
        
        [Required]
        public CarType CarType { get; set; }
        
        public string Color { get; set; }
        
        public  string AddedUserName { get; set; } 
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}