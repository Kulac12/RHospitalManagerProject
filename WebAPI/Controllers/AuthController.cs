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
        private IUserOperationClaimService _userOperationCalimService;

        public AuthController(IAuthService authService, IUserService userService, IPatientService patientservice, IUserOperationClaimService userOperationClaimService)
        {
            _authService = authService;
            _userService = userService;
            _patientService = patientservice;
            _userOperationCalimService = userOperationClaimService;
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
            var userExists = _authService.UserExistsIdentity(userForPatientRegisterDto.IdentityNumber);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message); // Kimlik numarası ile kullanıcı kontrolü
            }

            // Kullanıcı kaydını oluşturuyoruz
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForPatientRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForPatientRegisterDto.Email,
                FirstName = userForPatientRegisterDto.FirstName,
                LastName = userForPatientRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);  // Kullanıcıyı ekliyoruz

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
            _userOperationCalimService.Add(userOperationClaim);

            // Token oluşturma işlemi
            var result = _authService.CreateAccessToken(user);
            if (result.Success)
            {
                return Ok(result.Data);  // Token başarıyla oluşturulursa döndürülüyor
            }

            return BadRequest(result.Message);  // Token oluşturulamazsa hata mesajı döndürülüyor
        }



    }
}
