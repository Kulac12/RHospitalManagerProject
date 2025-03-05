using Core.Entities;
using Core.Utilities.Models.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ResModels
{
    //Kullanıcı kayıt olduktan sonra ilgili user kaydını bu detaylar ile döner.
    public class UserResponseDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public bool Status { get; set; }
        public UserType Type { get; set; }
        public int? PoliklinikId { get; set; }

    }
}
