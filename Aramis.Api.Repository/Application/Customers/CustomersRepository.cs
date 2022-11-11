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
            _repository.Add(opCliente);
            return _repository.Save();
        }

        public bool Delete(OpCliente opCliente)
        {
            _repository.Delete(opCliente.Id);
            return _repository.Save();
        }
        public OpCliente Get(string id)
        {
            return _context.OpClientes
                    .AsNoTracking()
                    .Include(x => x.GenderNavigation)
                    .Include(x => x.RespNavigation)
                    .Include(x => x.PaisNavigation)
                    .Where(x => x.Id.Equals(Guid.Parse(id))).SingleOrDefault()!;
        }
        public List<OpCliente> GetAll()
        {
            return _context.OpClientes
                   .Include(x => x.GenderNavigation)
                   .Include(x => x.RespNavigation)
                   .Include(x => x.PaisNavigation)
                   .OrderBy(x => x.Cui)
                   .ToList()!;
        }
        public bool Update(OpCliente opCliente)
        {
            _repository.Update(opCliente);
            return _repository.Save();
        }
    }
}
