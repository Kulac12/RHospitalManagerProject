using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> RegisterPatient(UserForPatientRegisterDto userForPatientRegisterDto, string password);
        IDataResult<User> RegisterDoctor(UserForDoctorRegisterDto userForDoctorRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IResult UserExistsIdentity(string identityNumber);
        IResult UserExistsIdentityPatient(string identityNumber);
        IResult UserExistsIdentityDoctor(string identityNumber);
        IDataResult<AccessToken> CreateAccessToken(User user);

        List<UserWithRolesDto> GetAllUsers();
    }
}
 