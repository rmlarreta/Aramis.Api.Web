using Aramis.Api.Commons.ModelsDto.Customers;
using Aramis.Api.CustomersService.Extensions;
using Aramis.Api.CustomersService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aramis.Api.CustomersService.Application
{
    public class CustomersService : Service<OpCliente>, ICustomersService
    {

        private readonly IMapper _mapper;
        private readonly ICustomersAttributes _attributes;
        public CustomersService(IUnitOfWork unitOfWork, IMapper mapper, ICustomersAttributes attributes) : base(unitOfWork)
        {
            _mapper = mapper;
            _attributes = attributes;
        }

        public async Task<List<OpClienteDto>> GetAllClientes()
        {
            List<OpCliente>? list = await base.GetAll().ToListAsync();
            return _mapper.Map<List<OpClienteDto>>(list);
        }

        public async Task<OpClienteDto> GetById(Guid id)
        {
            Expression<Func<OpCliente, object>>[] includeProperties = new Expression<Func<OpCliente, object>>[]
        {
            c => c.RespNavigation,
            c => c.GenderNavigation,
            c => c.PaisNavigation
        };
            OpCliente cliente = await base.Get(id, includeProperties);
            return _mapper.Map<OpClienteDto>(cliente);
        }

        public async Task<OpClienteDto> GetByCui(string cui)
        {
            Expression<Func<OpCliente, bool>> expression = c => c.Cui == cui;
            Expression<Func<OpCliente, object>>[] includeProperties = new Expression<Func<OpCliente, object>>[]
            {
            c => c.RespNavigation,
            c => c.GenderNavigation,
            c => c.PaisNavigation
           };
            OpCliente cliente = await base.Get(expression, includeProperties);
            return _mapper.Map<OpClienteDto>(cliente);
        }

        public async Task<int> DeleteCliente(Guid id)
        {
            OpCliente cliente = await base.Get(id);
            if (cliente.Cui == "0") throw new ApplicationException("Este cliente no es eliminable");
            return await base.Delete(cliente);
        }

        public async Task Update(OpClienteBase entity)
        {
            if (await CheckIfMostradorAsync(entity)) throw new ApplicationException("Este cliente no es editable");
            var gender = _attributes.GetGender(entity.Gender).Name;
            entity.Cui = ExtensionMethods.ConformaCui(entity, gender);
            await base.Update(_mapper.Map<OpCliente>(entity));
        } 
        public async Task Insert(OpClienteBase entity)
        {
            OpCliente cliente = _mapper.Map<OpCliente>(entity);
            await base.Add(cliente);
        } 
        private async Task<bool> CheckIfMostradorAsync(OpClienteBase entity)
        {
            OpCliente cliente = await base.Get(entity.Id);
            return cliente.Cui == "0";
        }
    }
}
