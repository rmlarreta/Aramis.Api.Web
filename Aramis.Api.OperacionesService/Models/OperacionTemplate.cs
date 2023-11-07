using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.OperacionesService.Models
{
    public abstract class OperacionTemplate : Service<BusOperacion>, IOperacionesService
    {
        protected OperacionTemplate(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public abstract Task<int> DeleteOperacion(Guid id);

        public abstract Task<BusOperacionesDto> GetOperacion(Guid id);

        public abstract Task<BusOperacionesDto> NuevaOperacion(BusOperacionBaseDto? busoperacionesinsert, string operador);

        public abstract Task<BusOperacionesDto> UpdateOperacion(BusOperacionBaseDto busoperacionesinsert);
    }


}
