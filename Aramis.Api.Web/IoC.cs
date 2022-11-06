﻿using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Application.Customers;
using Aramis.Api.Repository.Application.Operaciones;
using Aramis.Api.Repository.Application.Security;
using Aramis.Api.Repository.Application.Stock;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Interfaces.Stock;
using Aramis.Api.SecurityService.Interfaces;
using Aramis.Api.StockService.Interfaces;

namespace Aramis.Api.Web
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services)
        {

            #region Services

            services.AddScoped<ISecurityService, SecurityService.Application.SecurityService>();
            services.AddScoped<ICustomersService, CustomersService.Application.CustomersService>();
            services.AddScoped<IStockService, StockService.Application.StockService>();
            services.AddScoped<IOperacionesService, OperacionesService.Application.OperacionesService>();
            services.AddScoped<ICuentasService, FlowService.Application.CuentasService>();
            services.AddScoped<ITipoPagoService, FlowService.Application.TipoPagoService>();
            #endregion 

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomersAttributesRepository, CustomersAttributesRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IOperacionesRepository, OperacionesRepository>();
            #endregion Repositories
        }
    }
}
