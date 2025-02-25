using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    //bana bir tane entity bir tane context ver. Ben buna göre çalışacağım demek
    /// <summary>
    /// Yeni bir tablo oluşturduğunda ekleme, silme, güncelleme işlemi yapmayacağız demek.Bir kere yazıcaz her zaman kullanıcaz demek
    /// </summary>
    /// //çözmek demek ampulden ilgili nesneyi eklemek demek 
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
       where TEntity : class, IEntity, new()    // bu bir veri tabanı tablosu olacak ama IEntity direk yazamasın diye new() yazıyoruz. 
       where TContext : DbContext, new()  //Bu bir DbContext sınıfı olacak ama direk kendi adını yazamasın diye yine onu da new ledik.
    {
        public void Add(TEntity entity)
        {
            //Idisposabe pattern implementation of c#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();

            }
        }

        public void Delete(TEntity entity)
        {
            //Idisposabe pattern implementation of c#
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }


        }

        public void Update(TEntity entity)
        {
            //Idisposabe pattern implementation of c#
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    
    
    }
}
