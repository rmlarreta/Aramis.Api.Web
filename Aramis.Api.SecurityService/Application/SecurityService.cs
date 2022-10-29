using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Helpers;
using Aramis.Api.SecurityService.Interfaces;
using Aramis.Api.SecurityService.ModelsDto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aramis.Api.SecurityService.Application
{
    public class SecurityService : ISecurityService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly AppSettings _appSettings;
        public SecurityService(IUsersRepository usersRepository, IOptions<AppSettings> appSettings, ISecurityRepository securityRepository)
        {
            _usersRepository = usersRepository;
            _securityRepository = securityRepository;
            _appSettings = appSettings.Value;
        }
        public UserAuth Authenticate(string username, string password)
        {
            try
            {

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return null!;
                }

                SecUser user = _usersRepository.GetByName(username);

                // check if username exists
                if (user == null || user.Active == false)
                {
                    return null!;
                }

                // check if password is correct
                if (!ExtensionMethods.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null!;
                }

                if (user.EndOfLife < DateTime.Now.AddHours(-3))
                {
                    throw new ApplicationException("Debes Renovar tus Datos");
                }

                // authentication successful
                UserAuth userAuth = new()
                {
                    Role = user.Role!,
                    UserName = user.RealName,
                    Token = GetToken(user)
                };

                return userAuth;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ChangePassword(string username, string password, string npassword)
        {
            try
            {
                SecUser? secUser = new();
                UserAuth? user = Authenticate(username, password);
                if (user != null) { secUser = _usersRepository.GetByName(username); }

                if (!string.IsNullOrWhiteSpace(npassword))
                {
                    ExtensionMethods.CreatePasswordHash(npassword, out byte[] passwordHash, out byte[] passwordSalt);

                    secUser.PasswordHash = passwordHash;
                    secUser.PasswordSalt = passwordSalt;
                    secUser.EndOfLife = DateTime.Today.AddMonths(1);
                    _usersRepository.Update(secUser);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateUser(SecUser user, string password)
        {
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Falta la contraseña");
                }

                if (_usersRepository.GetByName(user.UserName) != null)
                {
                    throw new Exception("El usuario\"" + user.UserName + "\" ya está en uso");
                }

                ExtensionMethods.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.EndOfLife = DateTime.Today.AddDays(-1);
                user.Active = false;
                user.Role = _securityRepository.GetRoleByName("User").Id;
                _usersRepository.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(string id)
        {
            _usersRepository.Delete(GetUserById(id));
        }

        public IEnumerable<SecUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public SecUser GetUserById(string id)
        {
            return _usersRepository.GetById(id);
        }

        public void UpdateUser(SecUser user)
        {
            _usersRepository.Update(user);
        }

        private string GetToken(SecUser user)
        {
            JwtSecurityTokenHandler? tokenHandler = new();
            byte[]? key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
            SecurityTokenDescriptor? tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                     new Claim("UserName", user.UserName.ToString()),
                          new Claim("UserRealName", user.RealName.ToString()),
                       new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            string? tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
