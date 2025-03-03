using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
        private IDoctorService _doctorService;
        private IPolyclinicService _polyclinicService;
        private IUserOperationClaimService _userOperationClaimService;

        IUserDal _userDal;
        public AuthManager(
            IUserDal _userDal,
            IUserService userService, 
            ITokenHelper tokenHelper,
            IPatientService patientService,
            IUserOperationClaimService userOperationClaimService,
            IDoctorService doctorService,
            IPolyclinicService polyclinicService)
        {
            _userDal = _userDal;
            _userService = userService;
            _tokenHelper = tokenHelper;
            _patientService = patientService;
            _userOperationClaimService = userOperationClaimService;
            _doctorService = doctorService;
            _polyclinicService = polyclinicService;
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



        
        public IDataResult<User> RegisterPatient(UserForPatientRegisterDto userForPatientRegisterDto, string password)
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

            // Hasta kaydını oluşturuyoruz
            var patient = new Patient
            {
                UserId = user.Id,  // UserId ile ilişkilendiriyoruz
                PatientName = user.FirstName + user.LastName,
                IdentityNumber = userForPatientRegisterDto.IdentityNumber
            };

            _patientService.Add(patient);  // Hastayı ekliyoruz


            // Patient rolünü ekliyoruz
            var operationClaimId = 3; // Patient rolü
            var userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = operationClaimId
            };

            // UserOperationClaim'i ekliyoruz
            _userOperationClaimService.Add(userOperationClaim);

             return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }




        public IDataResult<User> RegisterDoctor(UserForDoctorRegisterDto userForDoctorRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // User nesnesini oluşturuyoruz
            var user = new User
            {
                Email = userForDoctorRegisterDto.Email,
                FirstName = userForDoctorRegisterDto.FirstName,
                LastName = userForDoctorRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            
            var polyclinic = _polyclinicService.GetByName(userForDoctorRegisterDto.PolyclinicName);

            if (polyclinic == null)
            {
                return new ErrorDataResult<User>(Messages.PolyclinikNotFound);
            }

            var doctor = new Doctor
            {
                UserId = user.Id,
                DoctorFirstName = user.FirstName,
                DoctorLastName = user.LastName,
                IdentityNumber = userForDoctorRegisterDto.IdentityNumber,
                DoctorSpecialty = userForDoctorRegisterDto.DoctorSpecialty,
                PolyclinicId = polyclinic.Id // Burada ID'yi set ediyoruz
            };

         

            // Doctor rolünü ekliyoruz
            var operationClaimId = 2; // Doctor rolü
            var userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = operationClaimId
            };

            // UserOperationClaim'i ekliyoruz
           
            _userService.Add(user);
            _doctorService.Add(doctor);
            _userOperationClaimService.Add(userOperationClaim);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
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

        public IResult UserExistsIdentityPatient(string identityNumber)
        {
            var user = _patientService.GetByIdentityNumber(identityNumber);

            if (user != null)
            {
                // Kullanıcı bulundu, bu kullanıcının rolünü kontrol ediyoruz
                var userOperationClaims = _userOperationClaimService.GetByUserId(user.UserId);

                // Kullanıcının rolü 3 (Patient) ise
                var hasPatientRole = userOperationClaims.Any(u => u.OperationClaimId == 3);

                if (hasPatientRole)
                {
                    // Kullanıcı zaten "Patient" rolüne sahip
                    return new ErrorResult(Messages.UserAlreadyExists);
                }
            }

            // Eğer kullanıcı bulunamazsa veya rolü "Patient" değilse, işlem başarılı
            return new SuccessResult();
        }
        public IResult UserExistsIdentityDoctor(string identityNumber)
        {
            var user = _doctorService.GetByIdentityNumber(identityNumber);

            if (user != null)
            {
                // Kullanıcı bulundu, bu kullanıcının rolünü kontrol ediyoruz
                var userOperationClaims = _userOperationClaimService.GetByUserId(user.UserId);

                // Kullanıcının rolü 3 (Patient) ise
                var hasDoctorRole = userOperationClaims.Any(u => u.OperationClaimId == 2);

                if (hasDoctorRole)
                {
                    // Kullanıcı zaten "Patient" rolüne sahip
                    return new ErrorResult(Messages.UserAlreadyExists);
                }
            }

            // Eğer kullanıcı bulunamazsa veya rolü "Patient" değilse, işlem başarılı
            return new SuccessResult();
        }

        public List<UserWithRolesDto> GetAllUsers()
        {
            return _userDal.GetAllUsers();
        }
    }
}
