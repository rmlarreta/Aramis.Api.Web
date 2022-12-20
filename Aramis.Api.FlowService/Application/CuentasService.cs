using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class CuentasService : ICuentasService
    {
        private readonly IGenericRepository<CobCuentum> _cuentas;
        private readonly IMapper _mapper;
        public CuentasService(IGenericRepository<CobCuentum> cuentas, IMapper mapper)
        {
            _cuentas = cuentas;
            _mapper = mapper;
        }
        public bool Delete(string id)
        {
            _cuentas.Delete(Guid.Parse(id));
            return _cuentas.Save();
        }

        public List<CobCuentDto> GetAll()
        {
            return _mapper.Map<List<CobCuentum>, List<CobCuentDto>>(_cuentas.Get().OrderBy(x=>x.Name).ToList());
        }

        public CobCuentDto GetById(string id)
        {
            return _mapper.Map<CobCuentum, CobCuentDto>(_cuentas.Get(Guid.Parse(id)));
        }

        public CobCuentDto Insert(CobCuentDto cobCuentum)
        {
            cobCuentum.Id = Guid.NewGuid();
            _cuentas.Add(_mapper.Map<CobCuentDto, CobCuentum>(cobCuentum));
            return cobCuentum;
        }

        public CobCuentDto Update(CobCuentDto cobCuentum)
        {
            _cuentas.Update(_mapper.Map<CobCuentDto, CobCuentum>(cobCuentum));
            return cobCuentum;
        }
    }
}
