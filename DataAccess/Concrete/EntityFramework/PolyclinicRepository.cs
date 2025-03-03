using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class PolyclinicRepository:EfEntityRepositoryBase<Polyclinic, HospitalManagerContext>, IPolyclinicRepository
    {

        public List<PolyclinicDetailDto> GetAllPolyclinics()
        {
            using (HospitalManagerContext context = new HospitalManagerContext())
            {
                var polclinics = context.Polyclinic
                 .Include(p => p.Doctor)
                 .ToList();

                var result = polclinics.Select(p => new PolyclinicDetailDto
                {
                    PoliclinicName = p.PoliclinicName,
                    PoliclinicDescription = p.PoliclinicDescription,
                    DoctorNames = p.Doctor?.Select(d => $"{d.DoctorFirstName} {d.DoctorLastName}").ToList() ?? new List<string>()
                }).ToList();
                return result;
            }




            
        }
    }
}
