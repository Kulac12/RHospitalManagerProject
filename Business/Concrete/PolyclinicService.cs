using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PolyclinicService : IPolyclinicService
    {
        IPolyclinicRepository _polyclinicRepository;
        public PolyclinicService(IPolyclinicRepository polyclinicRepository)
        {
            _polyclinicRepository = polyclinicRepository;
        }
     
        public Polyclinic GetById(Guid id)
        {
            return _polyclinicRepository.Get(p => p.Id == id);
        }

        public Polyclinic GetByName(string name)
        {
            return _polyclinicRepository.Get(p => p.PoliclinicName == name);
        }

        public void Add(Polyclinic polyclinic)
        {
            _polyclinicRepository.Add(polyclinic);
        }

        public void Update(Polyclinic polyclinic)
        {
            _polyclinicRepository.Update(polyclinic);
        }

        public void Delete(Polyclinic polyclinic)
        {
            _polyclinicRepository.Delete(polyclinic);
        }

        public List<PolyclinicDetailDto> GetAll()
        {
            var list = _polyclinicRepository.GetAll();
            var result = list.Select(p => new PolyclinicDetailDto
            {
                PoliclinicName = p.PoliclinicName,
                PoliclinicDescription = p.PoliclinicDescription,
                DoctorNames = p.Doctor?.Select(d => $"{d.DoctorFirstName} {d.DoctorLastName}").ToList() ?? new List<string>() // Doktorlar null olabilir
            }).ToList();

            return result;
        }
        public Polyclinic GetBySameName(string name)
        {
            return _polyclinicRepository.GetAll(p => p.PoliclinicName == name).SingleOrDefault();
        }

        public bool PolyclinicExistByName(string name)
        {
            // Verilen isimde poliklinik var mı? Eğer varsa true, yoksa false 
            return _polyclinicRepository.GetAll(p => p.PoliclinicName == name).Any();
        }

      
    }
}
