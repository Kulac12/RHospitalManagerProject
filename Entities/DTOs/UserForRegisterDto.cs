using Core.Entities;
using Core.Utilities.Models.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForRegisterDto : IDto
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; } // Kullanıcı tipi eklendi (Doctor veya Patient)
        public int? PoliklinikId { get; set; } // Eğer doktor ise poliklinik seçilebilir

    }
}
