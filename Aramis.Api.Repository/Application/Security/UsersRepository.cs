using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;

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

        public bool Add(SecUser user)=>_repository.Add(user); 
        public bool Delete(SecUser secUser)=> _repository.Delete(secUser.Id.ToString()); 
        public SecUser GetByName(string name)=> _context.SecUsers!.SingleOrDefault(x => x.UserName.Equals(name))!; 
        public SecUser GetById(string id)=> _repository.Get(id); 
        public bool Update(SecUser secUser)=> _repository.Update(secUser); 
        public List<SecUser> GetAll()=> (List<SecUser>)_repository.Get(); 
    }
}
