using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.FiscalService.Interfaces;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.ReportService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Application.Customers;
using Aramis.Api.Repository.Application.Operaciones;
using Aramis.Api.Repository.Application.Pagos;
using Aramis.Api.Repository.Application.Recibos;
using Aramis.Api.Repository.Application.Reports;
using Aramis.Api.Repository.Application.Security;
using Aramis.Api.Repository.Application.Stock;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Interfaces.Reports;
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
            services.AddScoped<IRecibosService, FlowService.Application.RecibosService>();
            services.AddScoped<IPaymentsMp, FlowService.Application.PaymentsMP>();
            services.AddScoped<IPagosService, FlowService.Application.PagosService>();
            services.AddScoped<IFiscalService, FiscalService.Application.FiscalService>();
            services.AddScoped<IOperacionsReports, ReportService.Application.OperacionsReports>();
            services.AddScoped<IReportsService,ReportService.Application.ReportsService>();
            #endregion 

            #region Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomersAttributesRepository, CustomersAttributesRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IOperacionesRepository, OperacionesRepository>();
            services.AddScoped<IRecibosRepository, RecibosRepository>();
            services.AddScoped<IPagosRepository, PagosRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();
            #endregion Repositories
        }
    }
}
