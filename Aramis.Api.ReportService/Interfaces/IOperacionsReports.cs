using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.ReportService.Interfaces
{
    public interface IOperacionsReports
    {
        FileStreamResult FacturaReport(string id);
        //reporte remitos
        //reporte ordenes
        //reporte recibos
    }
}
