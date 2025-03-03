using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, HospitalManagerContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new HospitalManagerContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
        public List<UserWithRolesDto> GetAllUsers()
        {
            using (HospitalManagerContext context = new HospitalManagerContext())
            {
                
                    var users = context.UserOperationClaims
                        .Join(context.Users, uoc => uoc.UserId, u => u.Id, (uoc, u) => new { uoc, u }) // UserId ile User tablosu arasında join yapıyoruz
                        .Join(context.OperationClaims, uocWithUser => uocWithUser.uoc.OperationClaimId, oc => oc.Id, (uocWithUser, oc) => new { uocWithUser, oc }) // OperationClaimId ile OperationClaims tablosu arasında join yapıyoruz
                        .ToList();

                    var result = users.GroupBy(u => u.uocWithUser.u.Id) // Kullanıcıyı UserId'ye göre grupla
                        .Select(g => new UserWithRolesDto
                        {
                            UserId = g.Key,
                            UserName = g.First().uocWithUser.u.FirstName + g.First().uocWithUser.u.LastName, // Kullanıcı adı
                            Email = g.First().uocWithUser.u.Email, // Kullanıcı e-posta
                            Roles = g.Select(u => u.oc.Name).ToList() // Kullanıcının sahip olduğu roller (OperationClaimName)
                        }).ToList();

                    return result;
                }

            
        }
    
    
    }
}
