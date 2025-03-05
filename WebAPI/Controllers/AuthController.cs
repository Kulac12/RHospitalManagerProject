using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Models.EnumModels;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            // Kullanıcı zaten var mı kontrol et
            var userExists = _authService.UserExists(userForRegisterDto.IdentityNumber);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            // Kullanıcı türü geçerli mi kontrol et
            if (!Enum.IsDefined(typeof(UserType), userForRegisterDto.Type))
            {
                return BadRequest("Geçersiz kullanıcı tipi.");
            }

            // Doktor ise PoliklinikId zorunlu olmalı
            if (userForRegisterDto.Type == UserType.Doctor && !userForRegisterDto.PoliklinikId.HasValue)
            {
                return BadRequest("Doktor kaydı için PoliklinikId belirtilmelidir.");
            }

            // Kullanıcıyı kaydet
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            // Access Token oluştur
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }

}

