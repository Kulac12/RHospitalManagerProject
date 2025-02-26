using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IPatientService _patientService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IPatientService patientService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _patientService = patientService;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        //AuthManagerda eklemeleri yapmalıyız. Contorllera yazdıklarımızı buraya alıcaz.

        public IDataResult<Patient> RegisterPatient(UserForPatientRegisterDto userForPatientRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // User nesnesini oluşturuyoruz
            var user = new User
            {
                Email = userForPatientRegisterDto.Email,
                FirstName = userForPatientRegisterDto.FirstName,
                LastName = userForPatientRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            // User'ı veritabanına kaydediyoruz
            _userService.Add(user);

            // User'ı GetByMail metodu ile sorgulayıp Id'sini alıyoruz
            var userFromDb = _userService.GetByMail(user.Email); // User döner

            // Eğer User bulunamazsa hata döndürüyoruz
            if (userFromDb == null)
            {
                return new ErrorDataResult<Patient>("User could not be found after registration.");
            }

            // Hasta nesnesini oluşturuyoruz
            var patient = new Patient
            {
                UserId = userFromDb.Id,  // userFromDb.Id kullanarak ilişkilendiriyoruz
                PatientName = user.FirstName + " " + user.LastName,
                IdentityNumber = userForPatientRegisterDto.IdentityNumber,
            };

            // Hastayı veritabanına ekliyoruz
            _patientService.Add(patient);

            // Başarıyla kaydedilen hastayı döndürüyoruz
            return new SuccessDataResult<Patient>(patient, Messages.UserRegistered);
        }



        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IResult UserExistsIdentity(string identityNumber)
        {
            if (_patientService.GetByIdentityNumber(identityNumber) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

     
    }
}
