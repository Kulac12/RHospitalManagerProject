using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //biz bir password vereceğiz ve dışarıya bu iki yapıyı(passwordHash,passwordSalt) çıkaracak yapıyı tasarlayacağız. 
        public static void CreatePasswordHash(
            string password, 
            out byte[] passwordHash, 
            out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //kullandığımız algoritmanın o anki key değeridir. Bu her kullanıcı için farklı Key oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //Password hashini doğrula demek
        public static bool VerifyPasswordHash(string password,
             byte[] passwordHash,
             byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}



