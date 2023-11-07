using Aramis.Api.Commons.ModelsDto.Security;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using Aramis.Api.SecurityService.Extensions;
using Aramis.Api.SecurityService.Helpers;
using Aramis.Api.SecurityService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Aramis.Api.SecurityService.Application
{
    public class SecurityService : Service<SecUser>, ISecurityService
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly HttpContext _hcontext;
        private readonly ClaimsPrincipal _cp;

        public SecurityService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IMapper mapper, IHttpContextAccessor haccess) : base(unitOfWork)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _hcontext = haccess.HttpContext!;
            _cp = _hcontext.User;
        }

        #region USERS
        public async Task<UserAuth> Authenticate(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return null!;
                }


                SecUser user = await GetUserBaseByName(username);

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
                UserAuth userAuth = _mapper.Map<UserAuth>(user);

                string? token = ExtensionMethods.GetToken(userAuth, _appSettings.Secret!);
                userAuth.Token = token;

                return userAuth;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserAuth> ChangePassword(string username, string password, string npassword)
        {
            try
            {
                SecUser? secUser = new();
                UserAuth? user = await Authenticate(username, password);

                if (user != null) { secUser = await GetUserBaseByName(username); }

                if (string.IsNullOrWhiteSpace(npassword))
                {
                    return null!;
                }

                ExtensionMethods.CreatePasswordHash(npassword, out byte[] passwordHash, out byte[] passwordSalt);

                secUser.PasswordHash = passwordHash;
                secUser.PasswordSalt = passwordSalt;
                secUser.EndOfLife = DateTime.Today.AddMonths(1);
                await Update(secUser);
                return await Authenticate(username, npassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserAuth> CreateUser(UserInsertDto userInsert)
        {
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(userInsert.PassWord))
                {
                    throw new Exception("Falta la contraseña");
                }

                if (GetUserByName(userInsert.UserName!) != null)
                {
                    throw new Exception("El usuario\"" + userInsert.UserName + "\" ya está en uso");
                }

                ExtensionMethods.CreatePasswordHash(userInsert.PassWord, out byte[] passwordHash, out byte[] passwordSalt);
                SecUser user = new();
                user.UserName = userInsert.UserName!;
                user.RealName = userInsert.RealName!;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.EndOfLife = DateTime.Today.AddDays(-1);
                user.Active = false;
                user.Role = user.Role;
                await Add(user);
                return await GetUserById(user.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteUser(Guid id)
        {
            SecUser user = await base.Get(id);
            await base.Delete(user);
        }
        public IEnumerable<UserAuth> GetAllUsers()
        {
            List<SecUser>? users = GetAll().ToList();
            return _mapper.Map<List<UserAuth>>(users);
        }
        public async Task<UserAuth> GetUserById(Guid id)
        {
            SecUser? user = await Get(id);
            return _mapper.Map<UserAuth>(user);
        }
        public async Task<UserAuth> GetUserByName(string name)
        {
            return _mapper.Map<UserAuth>(await GetUserBaseByName(name));
        }
        public async Task UpdateUser(UserBaseDto user)
        {
            await base.Update(_mapper.Map<SecUser>(user));
        }
        #endregion USERS

        #region Claims
        public string GetUserAuthenticated()
        {
            string? user = ExtensionMethods.GetUserName(_cp);
            if (user == null)
            {
                return null!;
            }

            return user;
        }
        public string GetPerfilAuthenticated()
        {
            string? perfil = ExtensionMethods.GetUserPerfil(_cp);
            return perfil;
        }
        #endregion Claims

        #region PRIVATE
        private async Task<SecUser> GetUserBaseByName(string name)
        {
            Expression<Func<SecUser, bool>> expression = c => c.UserName == name;
            Expression<Func<SecUser, object>>[] includeProperties = new Expression<Func<SecUser, object>>[]
            {
                    u=>u.RoleNavigation
            };
            return await Get(expression, includeProperties);
        }
        #endregion
    }
}
