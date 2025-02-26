using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;
        private IPatientService _patientService;
        private IPolyclinicService _polyclinicService;
        private IUserOperationClaimService _userOperationCalimService;
        private IDoctorService _doctorService;

        public AuthController(IAuthService authService, 
            IUserService userService, 
            IPatientService patientservice, 
            IUserOperationClaimService userOperationClaimService, 
            IDoctorService doctorService,
            IPolyclinicService polyclinicService)
        {
            _doctorService = doctorService;
            _authService = authService;
            _userService = userService;
            _patientService = patientservice;
            _userOperationCalimService = userOperationClaimService;
            _polyclinicService = polyclinicService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register/patient")]
        public ActionResult RegisterPatient(UserForPatientRegisterDto userForPatientRegisterDto)
        {
            // Kimlik numarasına göre kullanıcı kontrolü yapılıyor
            var userExists = _authService.UserExistsIdentityPatient(userForPatientRegisterDto.IdentityNumber);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message); // Kimlik numarası ile patient kullanıcısı var mı kontrolü
            }
            var registerResult = _authService.RegisterPatient(userForPatientRegisterDto, userForPatientRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

            //// Kullanıcı kaydını oluşturuyoruz
            //byte[] passwordHash, passwordSalt;
            //HashingHelper.CreatePasswordHash(userForPatientRegisterDto.Password, out passwordHash, out passwordSalt);
            //var user = new User
            //{
            //    Email = userForPatientRegisterDto.Email,
            //    FirstName = userForPatientRegisterDto.FirstName,
            //    LastName = userForPatientRegisterDto.LastName,
            //    PasswordHash = passwordHash,
            //    PasswordSalt = passwordSalt,
            //    Status = true
            //};
            //_userService.Add(user);  // Kullanıcıyı ekliyoruz


            //// Hasta kaydını oluşturuyoruz
            //var patient = new Patient
            //{
            //    UserId = user.Id,  // UserId ile ilişkilendiriyoruz
            //    PatientName = user.FirstName + user.LastName,
            //    IdentityNumber = userForPatientRegisterDto.IdentityNumber
            //};

            //_patientService.Add(patient);  // Hastayı ekliyoruz


            //// Patient rolünü ekliyoruz
            //var operationClaimId = 3; // Patient rolü
            //var userOperationClaim = new UserOperationClaim
            //{
            //    UserId = user.Id,
            //    OperationClaimId = operationClaimId
            //};

            //// UserOperationClaim'i ekliyoruz
            //_userOperationCalimService.Add(userOperationClaim);


            // Token oluşturma işlemi
            //var result = _authService.CreateAccessToken(user);
            //if (result.Success)
            //{
            //    return Ok(result.Data);  // Token başarıyla oluşturulursa döndürülüyor
            //}

            //return BadRequest(result.Message);  // Token oluşturulamazsa hata mesajı döndürülüyor
        }

        [HttpPost("register/doctor")]
        public ActionResult RegisterDoctor(UserForDoctorRegisterDto userForDoctorRegisterDto)
        {
            // Kimlik numarasına göre kullanıcı kontrolü yapılıyor
            var userExists = _authService.UserExistsIdentityDoctor(userForDoctorRegisterDto.IdentityNumber);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message); // Kimlik numarası ile kullanıcı kontrolü
            }
            //var registerResult = _authService.RegisterDoctor(userForDoctorRegisterDto, userForDoctorRegisterDto.Password);
            //var result = _authService.CreateAccessToken(registerResult.Data);
            //if (result.Success)
            //{
            //    return Ok(result.Data);
            //}

            //return BadRequest(result.Message);
            // Kullanıcı kaydını oluşturuyoruz

            #region Kötü_Kod

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForDoctorRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForDoctorRegisterDto.Email,
                FirstName = userForDoctorRegisterDto.FirstName,
                LastName = userForDoctorRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);  // Kullanıcıyı ekliyoruz

            // Poliklinik adını kullanarak PolyclinicId'yi alıyoruz
            var polyclinic = _polyclinicService.GetByName(userForDoctorRegisterDto.PolyclinicName);

            if (polyclinic == null)
            {
                return BadRequest("Seçilen poliklinik bulunamadı.");
            }

            var doctor = new Doctor
            {
                UserId = user.Id,
                DoctorFirstName = userForDoctorRegisterDto.FirstName,
                DoctorLastName = userForDoctorRegisterDto.LastName,
                IdentityNumber = userForDoctorRegisterDto.IdentityNumber,
                DoctorSpecialty = userForDoctorRegisterDto.DoctorSpecialty,
                PolyclinicId = polyclinic.Id // Burada ID'yi set ediyoruz
            };

            _doctorService.Add(doctor);  // Doktoru ekliyoruz

            // Doctor rolünü ekliyoruz
            var operationClaimId = 2; // Doctor rolü
            var userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = operationClaimId
            };

            // UserOperationClaim'i ekliyoruz
            _userOperationCalimService.Add(userOperationClaim);

            //Token oluşturma işlemi
            var result = _authService.CreateAccessToken(user);
            if (result.Success)
            {
                return Ok(result.Data);  // Token başarıyla oluşturulursa döndürülüyor
            }

            return BadRequest(result.Message);  // Token oluşturulamazsa hata mesajı döndürülüyor
            #endregion

        }


    }
}
