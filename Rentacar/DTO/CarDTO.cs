
using System;
using FluentValidation;
using Rentacar.Constants.Enums;

namespace Rentacar.DTO
{
    public class CarDTO
    {
        public string Macer { get; set; }
        
        public string Model { get; set; }
        
        public string KiloMeter { get; set; }
        
        public FuelType FuelType { get; set; }
        
        public Gear Gear { get; set; }
        
        public CarType CarType { get; set; }
        
        public string Color { get; set; }
    }
    
    
    
    public class CarDTOValidator : AbstractValidator<CarDTO>
    {
        public CarDTOValidator()
        {
            RuleFor(x => x.Macer)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(4)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.KiloMeter)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz");
            RuleFor(x => x.FuelType)
                .IsInEnum();
            RuleFor(x => x.Gear)
                .IsInEnum();
            RuleFor(x => x.CarType)
                .IsInEnum();
            RuleFor(x => x.Color)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz");
        }
    }
    
    
}