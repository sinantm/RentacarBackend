using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Rentacar.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
    }
    
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("{PropertyName} geçerli bir adres değil! Lütfen geçerli bir mail adresi giriniz.");
            RuleFor(x => x.PhoneNumber)
                .Must(x => x.Length == 11)
                .WithMessage("{PropertyName} 11 Haneli telefon numarası olmalıdır.");
        }
    }
}