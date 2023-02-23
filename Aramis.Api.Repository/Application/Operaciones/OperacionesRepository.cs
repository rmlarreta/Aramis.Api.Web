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
            return _context.BusOperacions
                 .Include(x => x.Cliente)
                 .Include(x => x.Cliente.RespNavigation)
                 .Include(x => x.TipoDoc)
                 .Include(x => x.Estado)
                 .Include(x => x.BusOperacionDetalles)
                 .Include(x => x.BusOperacionObservacions)
                 .Include(x => x.BusOperacionPagos)
                 .ThenInclude(x => x.Recibo)
                 .ThenInclude(x => x.CobReciboDetalles)
                 .ThenInclude(x => x.TipoNavigation).AsNoTracking()
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
                 .Include(x => x.BusOperacionPagos)
                 .ThenInclude(x => x.Recibo)
                 .ThenInclude(x => x.CobReciboDetalles)
                 .ThenInclude(x => x.TipoNavigation)
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

        public List<BusOperacion> GetImpagasByClienteId(string clienteId)
        {
            var query = from op in _context.BusOperacions
                        join pagos in _context.BusOperacionPagos on op.Id equals pagos.OperacionId
                        join recibos in _context.CobRecibos.AsNoTrackingWithIdentityResolution() on pagos.ReciboId equals recibos.Id
                        join detalles in _context.CobReciboDetalles.AsNoTrackingWithIdentityResolution() on recibos.Id equals detalles.ReciboId
                        join tipospago in _context.CobTipoPagos on detalles.Tipo equals tipospago.Id
                        where op.ClienteId.ToString() == clienteId && tipospago.Name == "CUENTA CORRIENTE" && detalles.Cancelado == false
                        select op.Id.ToString();
             
            List<BusOperacion> operaciones = this.Get(query.Distinct().ToList());

            return operaciones;
        }

        public List<BusOperacion> Get(List<string> ops)
        {

            List<BusOperacion> operaciones = new();
            foreach (var op in ops)
            {
                var data = _context.BusOperacions.AsNoTracking()
                    .Include(x => x.BusOperacionDetalles)
                    .Where(x => x.Id.ToString() == op)
                    .FirstOrDefault();
                if (data != null)
                {
                    operaciones.Add(data);
                }
            }
            return operaciones;
        }

        public void PagarOperaciones(List<string> ops)
        {
            Guid estado = _context.BusEstados.Where(x => x.Name.Equals("PAGADO")).SingleOrDefault()!.Id;
            foreach (var op in ops)
            {
                BusOperacion? optoupdate = _context.BusOperacions.Where(x=>x.Id==Guid.Parse(op)).FirstOrDefault();
                optoupdate!.EstadoId = estado;
                _context.BusOperacions.Update(optoupdate);
            }
        }
    }
}
