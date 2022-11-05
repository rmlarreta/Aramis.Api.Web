using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Security
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IGenericRepository<SecRole> _repository;
        private readonly AramisbdContext _context;
        public RoleRepository(IGenericRepository<SecRole> repository, AramisbdContext context)
        {
            _repository = repository;
            _context = context;
        }
        public bool Add(SecRole data)
        {
            return _repository.Add(data);
        }

        public bool Delete(SecRole data)
        {
            return _repository.Delete(data.Id);
        }

        public SecRole GetByName(string name)
        {
            return _context.SecRoles!.SingleOrDefault(x => x.Name.Equals(name))!;
        }

        public SecRole GetById(Guid id)
        {
            return _repository.Get(id);
        }

        public bool Update(SecRole data)
        {
            return _repository.Update(data);
        }

        public List<SecRole> GetAll()
        {
            return (List<SecRole>)_repository.Get();
        }
    }
}
