using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimService:IUserOperationClaimService
    {
        private IUserOperationClaimRepository _userOperationClaimRepository;


        public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }
        public void Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimRepository.Add(userOperationClaim);
        }

        public List<UserOperationClaim> GetByUserId(int userId)
        {
            return _userOperationClaimRepository.GetAll(u => u.UserId == userId);
        }
    }
}
