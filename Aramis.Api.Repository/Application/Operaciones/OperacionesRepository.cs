using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Operaciones
{
    public class OperacionesRepository : IOperacionesRepository
    { 
        private readonly AramisbdContext _context;
        public OperacionesRepository(AramisbdContext context)
        {
            _context = context;
        }

        public void DeleteDetalles(List<BusOperacionDetalle> detalles)
        {
            _context.BusOperacionDetalles.RemoveRange(detalles); 
        }

        public void DeleteOperacion(string operacion)
        {
            var op = _context.BusOperacions.FirstOrDefault(x=>x.Id.Equals(operacion));
            _context.BusOperacions.Remove(op!); 
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

        public void Insert(BusOperacion entity)
        {
            _context.BusOperacions.Add(entity); 
        }

        public void Update(BusOperacion entity)
        {
            _context.BusOperacions.Update(entity); 
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
