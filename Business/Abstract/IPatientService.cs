using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPatientService
    {
        //List<OperationClaim> GetClaims(User user);
        void Add(Patient patient);
        Patient GetByIdentityNumber(string identityNumber);

    }
}
