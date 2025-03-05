using Core.Utilities.Models.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string IdentityNumber { get; set; } // Kimlik numarası
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; }
        public UserType Type { get; set; } // Kullanıcı tipi (Doktor / Hasta)
        public int? PoliklinikId { get; set; } // Nullable Poliklinik ID
        public DateTime CreateTime { get; set; }

        // 0 silinmemiş 1 silinmiş kullanıcı
        public bool Deleted { get; set; }
    }
}
