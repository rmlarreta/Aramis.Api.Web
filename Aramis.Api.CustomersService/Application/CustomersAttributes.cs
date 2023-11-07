using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.CustomersService.Application
{
    public class CustomersAttributes : ICustomersAttributes
    {
        private readonly IRepository<OpGender> _genders;
        private readonly IRepository<OpPai> _paises;
        private readonly IRepository<OpResp> _resps;
        private readonly IMapper _mapper;

        public CustomersAttributes(IRepository<OpGender> genders, IRepository<OpPai> paises, IRepository<OpResp> resps, IMapper mapper)
        {
            _genders = genders;
            _paises = paises;
            _resps = resps;
            _mapper = mapper;
        }

        public OpGenderDto GetGender(Guid id)
        {
           return _mapper.Map<OpGenderDto>(_genders.Get(id));
        }

        public List<OpGenderDto> GetGenderList()
        {
            return _mapper.Map<List<OpGenderDto>>(_genders.GetAll());
        }

        public OpPaiDto GetPais(Guid id)
        {
            return _mapper.Map<OpPaiDto>(_paises.Get(id));
        }

        public List<OpPaiDto> GetPaisList()
        {
            return _mapper.Map<List<OpPaiDto>>(_paises.GetAll());
        }

        public OpRespDto GetResp(Guid id)
        {
            return _mapper.Map<OpRespDto>(_resps.Get(id));
        }

        public List<OpRespDto> GetRespList()
        {
            return _mapper.Map<List<OpRespDto>>(_resps.GetAll());
        }
    }
}
