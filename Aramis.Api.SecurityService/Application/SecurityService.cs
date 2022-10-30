using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Helpers;
using Aramis.Api.SecurityService.Interfaces;
using Aramis.Api.SecurityService.ModelsDto;
using Microsoft.Extensions.Options;

namespace Aramis.Api.SecurityService.Application
{
    public class SecurityService : ISecurityService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleRepository _rolesRepository;
        private readonly AppSettings _appSettings;
        public SecurityService(IUsersRepository usersRepository, IOptions<AppSettings> appSettings, IRoleRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _appSettings = appSettings.Value;
        }

        #region USERS
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
                    Token = ExtensionMethods.GetToken(user, _appSettings.Secret!)
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
                user.Role = _rolesRepository.GetByName("User").Id;
                _usersRepository.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
        public void DeleteUser(string id)=>_usersRepository.Delete(GetUserById(id));
        public IEnumerable<SecUser> GetAllUsers() => _usersRepository.GetAll();
        public SecUser GetUserById(string id) => _usersRepository.GetById(id);
        public void UpdateUser(SecUser user)=>_usersRepository.Update(user); 

        #endregion USERS

        #region ROLES
        public void DeleteRole(string id) => _rolesRepository.Delete(GetRoleById(id));
        public IEnumerable<SecRole> GetAllRoles() => _rolesRepository.GetAll();
        public SecRole GetRoleById(string id) =>_rolesRepository.GetById(id); 
        public SecRole GetRoleByName(string name)=>_rolesRepository.GetByName(name);     
        public void UpdateRole(SecRole role) => _rolesRepository.Update(role);
        public void CreateRole(SecRole role)=>_rolesRepository.Add(role);
        #endregion ROLES
    }
}
