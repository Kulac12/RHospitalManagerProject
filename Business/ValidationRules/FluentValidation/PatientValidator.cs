using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {

            RuleFor(p => p.IdentityNumber)
             .NotEmpty().WithMessage("Kimlik numarası boş olamaz.")
             .Length(11).WithMessage("Kimlik numarası 11 haneli olmalıdır.")
             .Matches("^[0-9]*$").WithMessage("Kimlik numarası sadece rakamlardan oluşmalıdır.");

            RuleFor(p => p.IdentityNumber).NotEmpty();

           
        }
    }
}
