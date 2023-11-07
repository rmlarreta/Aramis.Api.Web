using Aramis.Api.Repository.Models;

namespace Aramis.Api.OperacionesService.Interfaces
{
    public interface ISystemService
    {
        SystemIndex GetIndex(); 
        Task UpdateIndex(SystemIndex index);

    }
}
