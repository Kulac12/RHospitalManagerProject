using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //Kullanıcı sisteme girdiğinde eğer doğru ise buraya gelecek ve bu kullanıcının Claim lerini bulacak ve orada bir tane Json web Token oluşturacak içerisinde bu bilgileri barındıran ve buraya verecek. 
        //User user: bu kullanıcı için
        //List<OperationClaim> oeprationClaims: 
        AccessToken CreateToken(User user, List<OperationClaim> oeprationClaims);

    }
}
