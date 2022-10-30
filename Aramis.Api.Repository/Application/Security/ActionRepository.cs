using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Security;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Security
{
    public class ActionRepository : IActionRepository
    {
        private readonly IGenericRepository<SecAction> _repository;
        private readonly AramisbdContext _context;
        public ActionRepository(IGenericRepository<SecAction> repository, AramisbdContext context)
        {
            _repository = repository;
            _context = context;
        }
        public bool Add(SecAction data) => _repository.Add(data);
        public bool Delete(SecAction data) => _repository.Delete(data.Id.ToString());
        public SecAction GetByName(string name) => _context.SecActions!.SingleOrDefault(x => x.Name.Equals(name))!;
        public SecAction GetById(string id) => _repository.Get(id);
        public bool Update(SecAction data) => _repository.Update(data);
        public List<SecAction> GetAll() => (List<SecAction>)_repository.Get();
    }
}
