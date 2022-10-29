using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
namespace Aramis.Api.Repository.Application
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AramisbdContext _context;
        private readonly IRepository<SecUser> _repository;
        public UsersRepository(AramisbdContext context, IRepository<SecUser> repository)
        {
            _context = context;
            _repository = repository;
        }

        public bool Add(SecUser user)
        {
            return _repository.Add(user);
        }

        public bool Delete(SecUser secUser)
        {
            return _repository.Delete(secUser.Id.ToString());
        }

        public SecUser GetByName(string name)
        {
            return _context.SecUsers!.SingleOrDefault(x => x.UserName.Equals(name))!;
        }

        public SecUser GetById(string id)
        {
            return _context.SecUsers!.SingleOrDefault(x => x.Id.Equals(id.ToString()))!;
        }

        public bool Update(SecUser secUser)
        {
            return _repository.Update(secUser);
        }
    }
}
