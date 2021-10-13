using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Rentacar.DTO
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} boş olamaz")
                .MaximumLength(50)
                .WithMessage("{PropertyName} {MaxLength} karakterden uzun olamaz");
        }
    }
}