using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.OperacionesService.Application
{
    public class SystemService : Service<SystemIndex>, ISystemService
    {
        public SystemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public SystemIndex GetIndex()
        {
            return base.GetAll().First();
        }

        public async Task UpdateIndex(SystemIndex index)
        {
            await base.Update(index);
        }
    }
}
