using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ResModels
{
    public class PolyclinicDto :IDto
    {
        public string Name { get; set; } // Poliklinik adı
        public string Description { get; set; }
    }
}
