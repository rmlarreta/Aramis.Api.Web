using Aramis.Api.Repository.Interfaces.Recibos;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Recibos
{
    public class RecibosRepository : IRecibosRepository
    {
        private readonly AramisbdContext _context;
        public RecibosRepository(AramisbdContext context)
        {
            _context = context;
        }
        public void Add(CobRecibo recibo)
        {
            _context.CobRecibos.Add(recibo);
        }

        public void Delete(string id)
        {
            CobRecibo? data = _context.CobRecibos.FirstOrDefault(x => x.Id == Guid.Parse(id));
            _context.CobRecibos.Remove(data!);
        }

        public CobRecibo Get(string id)
        {
            return _context.CobRecibos
                    .Include(x => x.CobReciboDetalles)
                    .Include("CobReciboDetalles.TipoNavigation")
                    .Include("CobReciboDetalles.TipoNavigation.Cuenta")
                    .Include(x => x.Cliente)
                    .Include(x => x.Cliente.RespNavigation)
                    .Where(x => x.Id.Equals(Guid.Parse(id)))
                    .FirstOrDefault()!;
        }

        public List<CobRecibo> GetAll()
        {
            return _context.CobRecibos
                    .Include(x => x.CobReciboDetalles)
                    .Include("CobReciboDetalles.TipoNavigation")
                    .Include("CobReciboDetalles.TipoNavigation.Cuenta")
                    .Include(x => x.Cliente)
                    .Include(x => x.Cliente.RespNavigation)
                   .ToList()!;
        }

        public List<CobRecibo> GetSinImputarByCLiente(string clienteId)
        {
            var query = from r in _context.CobRecibos
                          where r.ClienteId.ToString() == clienteId
                          && !(from op in _context.BusOperacionPagos
                               select op.ReciboId).Contains(r.Id)
                          select r.Id.ToString();
            List<CobRecibo> recibos = new();
            foreach (var op in query)
            {
                var data = Get(op);
                if (data != null) recibos.Add(data);
            }
            return recibos.ToList();
        }

        public List<CobReciboDetalle> GetCuentaCorrientesByCliente(string clienteId)
        {
            var dets = from detalles in _context.CobReciboDetalles
                       join recibos in _context.CobRecibos on detalles.ReciboId equals recibos.Id
                       join tipos in _context.CobTipoPagos on detalles.Tipo equals tipos.Id
                       where recibos.ClienteId.ToString() == clienteId && tipos.Name == "CUENTA CORRIENTE"
                       select detalles;
            return dets.ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(CobRecibo recibo)
        {
            _context.CobRecibos.Update(recibo);
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
