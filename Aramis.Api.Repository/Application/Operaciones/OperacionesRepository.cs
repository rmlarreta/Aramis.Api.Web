using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Operaciones
{
    public class OperacionesRepository : IOperacionesRepository
    {
        private readonly IGenericRepository<BusOperacion> _operacionRespository;
        private readonly AramisbdContext _context;
        public OperacionesRepository(IGenericRepository<BusOperacion> operacionRespository, AramisbdContext context)
        {
            _operacionRespository = operacionRespository;
            _context = context;
        }

        public bool DeleteDetalles(List<BusOperacionDetalle> detalles)
        {
            _context.BusOperacionDetalles.RemoveRange(detalles);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteOperacion(string operacion)
        {
            _operacionRespository.Delete(Guid.Parse(operacion));
            return _operacionRespository.Save();
        }

        public BusOperacion Get(string id)
        {
            return _context.BusOperacions
                 .Include(x => x.Cliente)
                 .Include(x => x.Cliente.RespNavigation)
                 .Include(x => x.TipoDoc)
                 .Include(x => x.Estado)
                 .Include(x => x.BusOperacionDetalles)
                 .Include(x => x.BusOperacionObservacions)
                 .Where(x => x.Id.Equals(Guid.Parse(id)))
                 .FirstOrDefault()!;
        }

        public bool Insert(BusOperacion entity)
        {
            _operacionRespository.Add(entity);
            return _operacionRespository.Save();
        }

        public bool Update(BusOperacion entity)
        {
            _operacionRespository.Update(entity);
            return _operacionRespository.Save();
        }
    }
}
