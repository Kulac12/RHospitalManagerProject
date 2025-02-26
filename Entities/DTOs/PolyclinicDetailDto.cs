using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PolyclinicDetailDto:IDto
    {

        public string PoliclinicName { get; set; }
        public string PoliclinicDescription { get; set; }
        public List<string> DoctorNames { get; set; } = new List<string>(); // sadece isimleri 


    }
}
