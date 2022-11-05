using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Extensions;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.CustomersService.Application
{
    public class CustomersService : ICustomersService, ICustomersAttributesRepository
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersAttributesRepository _attributesRepository;
        private readonly IMapper _mapper;
        public CustomersService(ICustomersRepository customersRepository, ICustomersAttributesRepository attributesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _customersRepository = customersRepository;
            _attributesRepository = attributesRepository;
        }
        public bool Delete(string id)
        {
            return _customersRepository.Delete(_customersRepository.Get(id));
        }

        public List<OpClienteDto> GetAll()
        {
            List<OpCliente>? list = _customersRepository.GetAll();
            return _mapper.Map<List<OpCliente>, List<OpClienteDto>>(list);
        }

        public OpClienteDto GetById(string id)
        {
            OpCliente? entity = _customersRepository.Get(id);
            return _mapper.Map<OpCliente, OpClienteDto>(entity);
        }

        public OpClienteDto Insert(OpClienteInsert entity)
        {
            entity.Cui = ExtensionMethods.ConformaCui(entity, GetGender(entity.Gender).Name);
            entity.Id = Guid.NewGuid().ToString();
            OpCliente? cliente = _mapper.Map<OpClienteInsert, OpCliente>(entity);
            _customersRepository.Add(cliente);
            return GetById(entity.Id);
        }

        public OpClienteDto Update(OpClienteInsert entity)
        {
            entity.Cui = ExtensionMethods.ConformaCui(entity, GetGender(entity.Gender).Name);
            OpCliente? cliente = _mapper.Map<OpClienteInsert, OpCliente>(entity);
            _customersRepository.Update(cliente);
            return GetById(entity.Id!);
        }
        #region Atributos
        public OpGender GetGender(string id)
        {
            return _attributesRepository.GetGender(id);
        }

        public List<OpGender> GetGenderList()
        {
            return _attributesRepository.GetGenderList();
        }

        public OpPai GetPais(string id)
        {
            return _attributesRepository.GetPais(id);
        }

        public List<OpPai> GetPaisList()
        {
            return _attributesRepository.GetPaisList();
        }

        public OpResp GetResp(string id)
        {
            return _attributesRepository.GetResp(id);
        }

        public List<OpResp> GetRespList()
        {
            return _attributesRepository.GetRespList();
        }
        #endregion Atributos
    }
}
