using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.ResModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPolyclinicService
    {

        Task<IResult> AddAsync(PolyclinicDto polyclinicDto);
        Task<bool> IsExist(string name);

        Task<IDataResult<Polyclinic>> GetById(int id);

        Task<IDataResult<Polyclinic>> GetByName(string name);

        Task<IDataResult<Polyclinic>> Update(Polyclinic polyclinic);
        Task<IDataResult<Polyclinic>>  Delete(Polyclinic polyclinic);

        //Polyclinic GetById(int id);
        //Polyclinic GetByName(string name);
        IResult Add(PolyclinicDto polyclinicDto);
        //void Update(Polyclinic polyclinic);
        //void Delete(Polyclinic polyclinic);
    }
}