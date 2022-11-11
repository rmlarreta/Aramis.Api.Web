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
                    .Where(x => x.Id.Equals(Guid.Parse(id)))
                    .FirstOrDefault()!;
        }

        public IEnumerable<CobRecibo> GetAll()
        {
            return _context.CobRecibos
                   .Include(x => x.CobReciboDetalles)
                   .ToList()!;
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
