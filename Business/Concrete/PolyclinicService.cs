using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.ResModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    [CacheAspect]
    public class PolyclinicService : IPolyclinicService
    {

        IPolyclinicRepository _polyclinicRepository;
        public PolyclinicService(IPolyclinicRepository polyclinicRepository)
        {
            _polyclinicRepository = polyclinicRepository;
        }
     
        public async Task<IResult> AddAsync(PolyclinicDto polyclinicDto)
        {
            try
            {
                var polyclinic = new Polyclinic
                {
                    Name = polyclinicDto.Name,
                    Description = polyclinicDto.Description,
                    CreateTime = DateTime.Now,
                    Deleted = false
                };

                // Asenkron şekilde Polyclinic ekliyoruz
                await _polyclinicRepository.AddAsync(polyclinic);

                return new SuccessResult(Messages.AddPolyclinic);
            }
            catch (Exception ex)
            {

                return new ErrorResult($"Error occurred while adding polyclinic: {ex.Message}");
            }
           
        }

        public async Task<bool> IsExist(string name)
        {
            // Polikliniklerin listesini alıyoruz, silinmemiş olanlar ile filtreliyoruz.
            var polyclinics = await _polyclinicRepository.GetAllAsync(p => p.Name == name && !p.Deleted);

            // Eğer liste boş değilse, zaten var demektir
            return polyclinics.Any();
        }

        public async Task<IDataResult<Polyclinic>> Delete(Polyclinic polyclinic)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<Polyclinic>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<Polyclinic>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<Polyclinic>> Update(Polyclinic polyclinic)
        {
            throw new NotImplementedException();
        }

        [CacheAspect]
        public IResult Add(PolyclinicDto polyclinicDto)
        {
            // 1. Name alanının boş olup olmadığını kontrol et
            if (string.IsNullOrWhiteSpace(polyclinicDto.Name))
            {
                return new ErrorResult(Messages.IsNullOrWhiteSpace);
            }

            // 2. Aynı isimde bir poliklinik var mı kontrol et
            var existingPolyclinic = _polyclinicRepository.GetAll(p => p.Name == polyclinicDto.Name && !p.Deleted).FirstOrDefault();
            if (existingPolyclinic != null)
            {
                return new ErrorResult(Messages.PolyclinicExists);
            }

            // 3. Yeni poliklinik nesnesi oluştur
            var polyclinic = new Polyclinic
            {
                Name = polyclinicDto.Name,
                Description = polyclinicDto.Description,
                CreateTime = DateTime.Now,
                Deleted = false
            };

            // 4. Veritabanına ekle
            _polyclinicRepository.Add(polyclinic);

            return new SuccessResult(Messages.AddPolyclinic);
        }


    }
}
