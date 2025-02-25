using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    { 
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //securityKey: Hangi anahtarı kullandığımız
            //SecurityAlgorithms.HmacSha512Signature : Hangi algoritmanın kullanılacağı
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
