using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Security
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IGenericRepository<SecModule> _repository;
        private readonly AramisbdContext _context;
        public ModuleRepository(IGenericRepository<SecModule> repository, AramisbdContext context)
        {
            _repository = repository;
            _context = context;
        }
        public bool Add(SecModule data) => _repository.Add(data);
        public bool Delete(SecModule data) => _repository.Delete(data.Id.ToString());
        public SecModule GetByName(string name) => _context.SecModules!.SingleOrDefault(x => x.Name.Equals(name))!;
        public SecModule GetById(string id) => _repository.Get(id);
        public bool Update(SecModule data) => _repository.Update(data);
        public List<SecModule> GetAll() => (List<SecModule>)_repository.Get();
    }
}
