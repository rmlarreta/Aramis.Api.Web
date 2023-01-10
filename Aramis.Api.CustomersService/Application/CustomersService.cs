using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Extensions;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Application.Customers;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Customers;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.CustomersService.Application
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private ICustomersAttributesRepository _attributesRepositor;
        private readonly IGenericRepository<OpGender> _genderRepository;
        private readonly IGenericRepository<OpPai> _paiRepository;
        private readonly IGenericRepository<OpResp> _respRepository;
        private readonly IMapper _mapper;

        public CustomersService(
            IGenericRepository<OpGender> genderRepository,
            IGenericRepository<OpPai> paiRepository,
            IGenericRepository<OpResp> respRepository,
            ICustomersRepository customersRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _customersRepository = customersRepository;
            _genderRepository = genderRepository;
            _respRepository = respRepository;
            _paiRepository = paiRepository;
        }

        public bool Delete(string id)
        {
            OpCliente? customer = _customersRepository.Get(id);
            if (customer.Cui == "0") throw new ApplicationException("Este cliente no es eliminable");
            return _customersRepository.Delete(customer);
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

        public OpClienteDto GetByCui(string cui)
        {
            OpCliente? entity = _customersRepository.GetAll().Where(x => x.Cui == cui).FirstOrDefault()!;
            return _mapper.Map<OpCliente, OpClienteDto>(entity);
        }

        public OpClienteDto Insert(OpClienteInsert entity)
        {
            var gender = _genderRepository.Get(Guid.Parse(entity.Gender)).Name;
            entity.Cui = ExtensionMethods.ConformaCui(entity, gender);
            entity.Id = Guid.NewGuid().ToString();
            OpCliente? cliente = _mapper.Map<OpClienteInsert, OpCliente>(entity);
            _customersRepository.Add(cliente);
            return GetById(entity.Id);
        }

        public OpClienteDto Update(OpClienteInsert entity)
        {
            var gender = _genderRepository.Get(Guid.Parse(entity.Gender)).Name;
            entity.Cui = ExtensionMethods.ConformaCui(entity, gender);
            OpCliente? cliente = _mapper.Map<OpClienteInsert, OpCliente>(entity);
            _customersRepository.Update(cliente);
            return GetById(entity.Id!);
        }
        #region Atributos
        public ICustomersAttributesRepository Attributes => _attributesRepositor ??= new CustomersAttributesRepository(_genderRepository, _paiRepository, _respRepository);

        #endregion Atributos
    }
}
