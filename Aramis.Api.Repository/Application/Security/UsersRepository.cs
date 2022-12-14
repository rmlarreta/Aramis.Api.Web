using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Security
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

        public bool Add(SecUser user)
        {
            _repository.Add(user);
            return _repository.Save();
        }

        public bool Delete(SecUser secUser)
        {
            _repository.Delete(secUser.Id);
            return _repository.Save();
        }

        public SecUser GetByName(string name)
        {
            return _context.SecUsers!.Include(x => x.RoleNavigation).SingleOrDefault(x => x.UserName.Equals(name))!;
        }

        public SecUser GetById(string id)
        {
            return _context.SecUsers
             .AsNoTracking()
             .Include(x => x.RoleNavigation)
             .Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault()!;
        }

        public bool Update(SecUser secUser)
        {
            _repository.Update(secUser);
            return _repository.Save();
        }

        public List<SecUser> GetAll()
        {
            return _context.SecUsers
            .Include(x => x.RoleNavigation)
            .ToList();
        }
    }
}
