using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.FiscalService.Interfaces;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.ReportService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Application.Commons;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.SecurityService.Interfaces;
using Aramis.Api.StockService.Interfaces;
using Aramis.Api.SupplierService.Application;
using Aramis.Api.SupplierService.Interfaces;

namespace Aramis.Api.Web
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services)
        {

            #region Services
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<ICustomersService, CustomersService.Application.CustomersService>();
            services.AddScoped<IOperacionesService, OperacionesService.Application.OperacionesService>();
            services.AddScoped<ISecurityService, SecurityService.Application.SecurityService>();


            services.AddScoped<IStockService, StockService.Application.StockService>();
            services.AddScoped<ICuentasService, FlowService.Application.CuentasService>();
            services.AddScoped<ITipoPagoService, FlowService.Application.TipoPagoService>();
            services.AddScoped<IRecibosService, FlowService.Application.RecibosService>();
            services.AddScoped<IPaymentsMp, FlowService.Application.PaymentsMP>();
            services.AddScoped<IPagosService, FlowService.Application.PagosService>();
            services.AddScoped<IFiscalService, FiscalService.Application.FiscalService>();
            services.AddScoped<IOperacionsReports, ReportService.Application.OperacionsReports>();
            services.AddScoped<IReportsService, ReportService.Application.ReportsService>();
            services.AddScoped<ISuppliers, Suppliers>();
            #endregion

            #region Repositories
            services.AddScoped<IEntity, Entity>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion Repositories
        }
    }
}
