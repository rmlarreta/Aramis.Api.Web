using Aramis.Api.Commons.ModelsDto.Pagos;

namespace Aramis.Api.FlowService.Interfaces
{
    public interface IMovimientosCuentasService
    {

        Task Insert(CobCuentaMovimientoDto movimiento);
    }
}
