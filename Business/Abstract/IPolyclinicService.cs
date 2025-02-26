using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPolyclinicService
    {
        List<PolyclinicDetailDto> GetAll(); //Tüm poliklinleri getir
        Polyclinic GetById(Guid id);
        Polyclinic GetByName(string name);
        void Add(Polyclinic polyclinic);
        void Update(Polyclinic polyclinic);
        void Delete(Polyclinic polyclinic);
    }
}
