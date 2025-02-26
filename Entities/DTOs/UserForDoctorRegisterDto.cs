using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForDoctorRegisterDto:IDto
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Guid PolyclinicId { get; set; }
        public string PolyclinicName { get; set; }
        public string? DoctorSpecialty { get; set; }


    }
}
