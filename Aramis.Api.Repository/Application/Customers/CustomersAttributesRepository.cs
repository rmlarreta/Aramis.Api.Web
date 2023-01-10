using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.Repository.Application.Customers
{
    public class CustomersAttributesRepository : ICustomersAttributesRepository
    {
        private readonly IGenericRepository<OpGender> _genderRepository;
        private readonly IGenericRepository<OpPai> _paiRepository;
        private readonly IGenericRepository<OpResp> _respRepository;

        public CustomersAttributesRepository(IGenericRepository<OpGender> genderRepository, IGenericRepository<OpPai> paiRepository, IGenericRepository<OpResp> respRepository)
        {
            _genderRepository = genderRepository;
            _respRepository = respRepository;
            _paiRepository = paiRepository;
        }  
        public OpGender GetGender(string id)
        {
            return _genderRepository.Get(Guid.Parse(id));
        }

        public List<OpGender> GetGenderList()
        {
            return _genderRepository.Get().ToList();
        }

        public OpPai GetPais(string id)
        {
            return _paiRepository.Get(Guid.Parse(id));
        }

        public List<OpPai> GetPaisList()
        {
            return _paiRepository.Get().ToList();
        }

        public OpResp GetResp(string id)
        {
            return _respRepository.Get(Guid.Parse(id));
        }

        public List<OpResp> GetRespList()
        {
            return _respRepository.Get().ToList();
        }
    }
}
