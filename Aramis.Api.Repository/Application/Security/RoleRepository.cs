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
        public bool Add(SecRole data) => _repository.Add(data);
        public bool Delete(SecRole data) => _repository.Delete(data.Id.ToString());
        public SecRole GetByName(string name) => _context.SecRoles!.SingleOrDefault(x => x.Name.Equals(name))!;
        public SecRole GetById(string id) => _repository.Get(id);
        public bool Update(SecRole data) => _repository.Update(data);
        public List<SecRole> GetAll() => (List<SecRole>)_repository.Get();
    }
}
