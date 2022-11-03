using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;
namespace Aramis.Api.Repository.Application.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AramisbdContext _context;
        private readonly IGenericRepository<OpCliente> _repository;
        public CustomersRepository(AramisbdContext context, IGenericRepository<OpCliente> repository)
        {
            _context = context;
            _repository = repository;
        }
        public bool Add(OpCliente opCliente)
        {
            return _repository.Add(opCliente);
        }

        public bool Delete(OpCliente opCliente)
        {
            return _repository.Delete(opCliente.Id);
        }
        public OpCliente Get(string id)
        {
            return _context.OpClientes
                    .AsNoTracking()
                    .Include(x => x.GenderNavigation)
                    .Include(x => x.RespNavigation)
                    .Include(x=> x.PaisNavigation)
                    .Where(x => x.Id.Equals(Guid.Parse(id))).SingleOrDefault()!;
        }
        public List<OpCliente> GetAll()
        {
            return _context.OpClientes
                   .Include(x => x.GenderNavigation)
                   .Include(x => x.RespNavigation)
                   .Include(x => x.PaisNavigation)
                   .OrderBy(x=>x.Cui)
                   .ToList()!;
        }
        public bool Update(OpCliente opCliente)
        {
            return _repository.Update(opCliente);
        }
    }
}
