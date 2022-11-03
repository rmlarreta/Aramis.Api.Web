using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Helpers;
using Aramis.Api.SecurityService.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Aramis.Api.SecurityService.Application
{
    public class SecurityService : ISecurityService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleRepository _rolesRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public SecurityService(IUsersRepository usersRepository, IOptions<AppSettings> appSettings, IRoleRepository rolesRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
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
                    Id = user.Id.ToString(),
                    RealName = user.RealName,
                    Role = user.RoleNavigation.Name!,
                    UserName = user.UserName
                };
                var token = ExtensionMethods.GetToken(userAuth, _appSettings.Secret!);
                userAuth.Token = token;

                return userAuth;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserAuth ChangePassword(string username, string password, string npassword)
        {
            try
            {
                SecUser? secUser = new();
                UserAuth? user = Authenticate(username, password);
                if (user != null) { secUser = _usersRepository.GetByName(username); }

                if (string.IsNullOrWhiteSpace(npassword))
                {
                    return null!;
                }

                ExtensionMethods.CreatePasswordHash(npassword, out byte[] passwordHash, out byte[] passwordSalt);

                secUser.PasswordHash = passwordHash;
                secUser.PasswordSalt = passwordSalt;
                secUser.EndOfLife = DateTime.Today.AddMonths(1);
                _usersRepository.Update(secUser);
                return Authenticate(username, npassword);
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
        public void DeleteUser(string id)
        {
            _usersRepository.Delete(_usersRepository.GetById(id));
        }
        public IEnumerable<UserDto> GetAllUsers()
        {
            List<SecUser>? users = _usersRepository.GetAll();
            return _mapper.Map<List<SecUser>, List<UserDto>>(users);
        }
        public UserDto GetUserById(string id)
        {
            SecUser? user = _usersRepository.GetById(id);
            return _mapper.Map<SecUser, UserDto>(user);
        }
        public UserDto GetUserByName(string name)
        {
            SecUser? user = _usersRepository.GetByName(name);
            return _mapper.Map<SecUser, UserDto>(user);
        }
        public void UpdateUser(UserDto userdto)
        {
            var user = _mapper.Map<UserDto, SecUser>(userdto);
            var userpass = _usersRepository.GetById(userdto.Id.ToString());
            user.PasswordHash = userpass.PasswordHash;
            user.PasswordSalt = userpass.PasswordSalt;
            user.RoleNavigation = null!;
            _usersRepository.Update(user);
        }

        #endregion USERS

        #region ROLES
        public void DeleteRole(string id)
        {
            _rolesRepository.Delete(_rolesRepository.GetById(Guid.Parse(id)));
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _rolesRepository.GetAll();
            return _mapper.Map<List<SecRole>, List<RoleDto>>(roles);
        }

        public RoleDto GetRoleById(string id)
        {
            var role = _rolesRepository.GetById(Guid.Parse(id));
            return _mapper.Map<SecRole, RoleDto>(role);
        }

        public RoleDto GetRoleByName(string name)
        {
            var role = _rolesRepository.GetByName(name);
            return _mapper.Map<SecRole, RoleDto>(role);
        }

        public void UpdateRole(RoleDto roledto)
        {
            var role = _mapper.Map<RoleDto, SecRole>(roledto);
            _rolesRepository.Update(role);
        }

        public void CreateRole(RoleDto roledto)
        {
            var role = _mapper.Map<RoleDto, SecRole>(roledto);
            _rolesRepository.Add(role);
        }
        #endregion ROLES
    }
}
