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

        public void InsertDetalles(List<BusOperacionDetalle> detalles)
        {
            _context.BusOperacionDetalles.AddRange(detalles);
        }

        public void UpdateDetalles(List<BusOperacionDetalle> detalles)
        {
            _context.BusOperacionDetalles.UpdateRange(detalles);
        }

        public void DeleteDetalles(List<BusOperacionDetalle> detalles)
        {
            _context.BusOperacionDetalles.RemoveRange(detalles);
        }

        public void DeleteOperacion(string operacion)
        {
            BusOperacion? op = _context.BusOperacions.FirstOrDefault(x => x.Id.ToString() == operacion);
            _context.BusOperacions.Remove(op!);
        }

        public BusOperacion Get(string id)
        {
            return _context.BusOperacions.AsNoTracking()
                 .Include(x => x.Cliente)
                 .Include(x => x.Cliente.RespNavigation)
                 .Include(x => x.TipoDoc)
                 .Include(x => x.Estado).AsNoTracking()
                 .Include(x => x.BusOperacionDetalles)
                 .Include(x => x.BusOperacionObservacions)
                 .Where(x => x.Id.Equals(Guid.Parse(id)))
                 .FirstOrDefault()!;
        }

        public List<BusOperacion> Get()
        {
            return _context.BusOperacions
                 .OrderBy(x => x.Cliente.Razon)
                 .Include(x => x.Cliente)
                 .Include(x => x.Cliente.RespNavigation)
                 .Include(x => x.TipoDoc)
                 .Include(x => x.Estado)
                 .Include(x => x.BusOperacionDetalles)
                 .Include(x => x.BusOperacionObservacions)
                 .ToList()!;
        }

        public void Insert(BusOperacion entity)
        {
            _context.BusOperacions.Add(entity);
        }

        public void Update(BusOperacion entity)
        {
            _context.BusOperacions.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void InsertObservaciones(List<BusOperacionObservacion> observaciones)
        {
            _context.BusOperacionObservacions.AddRange(observaciones);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public List<BusEstado> GetEstados()
        {
            return _context.BusEstados.OrderBy(x => x.Name).ToList();
        }

        public List<BusOperacionTipo> GetTipos()
        {
            return _context.BusOperacionTipos.OrderBy(x => x.Name).ToList();
        }

        public List<StockProduct> GetProducts()
        {
            return _context.StockProducts.OrderBy(x => x.Descripcion).ToList();
        }

        public void UpdateProducts(List<StockProduct> products)
        {
            _context.UpdateRange(products);
        }

        public SystemIndex GetIndexs()
        {
            return _context.SystemIndices.First();
        }

        public void UpdateIndexs(SystemIndex indexs)
        {
            _context.SystemIndices.Update(indexs);
        }
    }
}
