using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Rentacar.DTO
{
    public class OrderDTO
    {
        public Guid CarId { get; set; }
        public string Name { get; set; }
        
        public string SurName { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Telephone { get; set; }
        
        public string IdentificationNumber { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public DateTime ReceiptDate { get; set; }
        
        public DateTime IssuanceDate { get; set; }
        
        public string DeliveryAddress { get; set; }
        
        public string Note { get; set; }
    }
    
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        private bool CheckDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        
        public OrderDTOValidator()
        {
            RuleFor(x => x.CarId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz");
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.SurName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .EmailAddress();
            RuleFor(x => x.DateOfBirth)
                .Must(CheckDate).WithMessage("{PropertyName} boş olamaz");
            RuleFor(x => x.ReceiptDate)
                .Must(CheckDate).WithMessage("{PropertyName} boş olamaz");
            RuleFor(x => x.IssuanceDate)
                .Must(CheckDate).WithMessage("{PropertyName} boş olamaz");
            RuleFor(x => x.DeliveryAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(500)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.Note)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(200)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
        }
    }
}