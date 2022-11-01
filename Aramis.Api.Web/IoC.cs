using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Application.Security;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security; 
using Aramis.Api.SecurityService.Interfaces; 

namespace Aramis.Api.Web
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services)
        {

            #region Services

            services.AddScoped<ISecurityService, SecurityService.Application.SecurityService>();
            #endregion 

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>(); 
            #endregion Repositories
        }
    }
}
