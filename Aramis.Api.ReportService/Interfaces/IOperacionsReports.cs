using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.ReportService.Interfaces
{
    public interface IOperacionsReports
    {
        FileStreamResult FacturaReport(string id);
        FileStreamResult RemitoReport(string id);
        FileStreamResult PresupuestoReport(string id);
        FileStreamResult TicketOrdenReport(string id);
        
        //reporte recibos
    }
}
