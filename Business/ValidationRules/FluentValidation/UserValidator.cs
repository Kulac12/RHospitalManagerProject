using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.");

            RuleFor(u => u.PasswordHash)
                .NotEmpty().WithMessage("Şifre boş olamaz.");

            RuleFor(u => u.PasswordSalt)
                .NotEmpty().WithMessage("Şifre boş olamaz.");

            // Şifre kuralları
            RuleFor(u => u.PasswordHash.ToString()) // Şifre hashlenmeden önce kontrol edilmeli
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük harf içermelidir.");
        }
    }
}
