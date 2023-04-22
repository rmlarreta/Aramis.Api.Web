using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Commons.ModelsDto.Suppliers;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class CuentasService : ICuentasService
    {
        private readonly IGenericRepository<CobCuentum> _cuentas;
        private readonly IGenericRepository<CobCuentaMovimiento> _cuentasMovimientos;
        private readonly IMapper _mapper;

        public CuentasService(IGenericRepository<CobCuentum> cuentas, IGenericRepository<CobCuentaMovimiento> cuentasMovimientos, IMapper mapper)
        {
            _cuentas = cuentas;
            _cuentasMovimientos = cuentasMovimientos;
            _mapper = mapper;
        }

        public bool DebitarPago(OpDocumentProveedorPago documentProveedorPago)
        {
            var cuenta = _cuentas.Get(Guid.Parse(documentProveedorPago.Cuenta!));
            cuenta.Saldo -= documentProveedorPago.Documento!.Monto;
            _cuentas.Update(cuenta);
            return _cuentas.Save();
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

        public CobCuentDto Insert(CobCuentaMovimientoDto cobCuentaMovimiento)
        {
            cobCuentaMovimiento.Id = Guid.NewGuid().ToString();
            _cuentasMovimientos.Add(_mapper.Map<CobCuentaMovimiento>(cobCuentaMovimiento)); 
            CobCuentum cuenta = _cuentas.Get(Guid.Parse(cobCuentaMovimiento.Cuenta!));
            cuenta.Saldo = cobCuentaMovimiento.Debito ? cuenta.Saldo - cobCuentaMovimiento.Monto : cuenta.Saldo + cobCuentaMovimiento.Monto;
            _cuentas.Update(cuenta);
            _cuentas.Save();
            return _mapper.Map<CobCuentDto>(cuenta);
        }

        public CobCuentDto Update(CobCuentDto cobCuentum)
        {
            _cuentas.Update(_mapper.Map<CobCuentDto, CobCuentum>(cobCuentum));
            _cuentas.Save();
            return cobCuentum;
        }
    }
}
