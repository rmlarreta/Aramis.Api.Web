using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AramisbdContext _context;
        private readonly IGenericRepository<SecUser> _repository;
        public UsersRepository(AramisbdContext context, IGenericRepository<SecUser> repository)
        {
            _context = context;
            _repository = repository;
        }

        public bool Add(SecUser user) => _repository.Add(user);
        public bool Delete(SecUser secUser) => _repository.Delete(secUser.Id);
        public SecUser GetByName(string name) => _context.SecUsers!.Include(x => x.RoleNavigation).SingleOrDefault(x => x.UserName.Equals(name))!;
        public SecUser GetById(string id) => _context.SecUsers
                                             .AsNoTracking()
                                             .Include(x => x.RoleNavigation)
                                             .Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault()!;
        public bool Update(SecUser secUser) => _repository.Update(secUser);
        public List<SecUser> GetAll() => _context.SecUsers
                                        .Include(x => x.RoleNavigation)
                                        .ToList();
    }
}
