using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Interfaces.Reports
{
    public interface IReportsRepository
    {
        IOperacionesRepository Operacions { get; }
        IRecibosRepository Cobranzas { get; }
        IService<SystemEmpresa> Empresa { get; }
    }
}
