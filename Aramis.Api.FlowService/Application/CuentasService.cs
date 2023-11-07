using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Commons.ModelsDto.Suppliers;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Application;
using Aramis.Api.Repository.Interfaces.Commons;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class CuentasService : Service<CobCuentum>, ICuentasService
    {

        private readonly IRepository<CobCuentaMovimiento> _cuentasMovimientos;
        private readonly IMapper _mapper;

        public CuentasService(IUnitOfWork unitOfWork, IRepository<CobCuentaMovimiento> cuentasMovimientos, IMapper mapper) : base(unitOfWork)
        {
            _cuentasMovimientos = cuentasMovimientos;
            _mapper = mapper;
        }

        public async Task DebitarPago(OpDocumentProveedorPago documentProveedorPago)
        {
            CobCuentum cuenta = await Get(documentProveedorPago.Cuenta);
            cuenta.Saldo -= documentProveedorPago.Documento!.Monto;

            CobCuentaMovimientoDto movimiento = new()
            {
                Computa = false,
                Debito = true,
                Detalle = $"Pago a Proveedor {documentProveedorPago.Documento.Razon} ({documentProveedorPago.Documento.Numero})",
                Cuenta = documentProveedorPago.Cuenta,
                Fecha = DateTime.Now,
                Monto = documentProveedorPago.Documento.Monto,
                Operador = documentProveedorPago.Operador
            };
            InsertMovimiento(movimiento);
            await Update(cuenta);
        }

        public async Task Delete(string id)
        {
            await Delete(Guid.Parse(id));
        }

        public List<CobCuentDto> GetAllCuentas()
        {
            List<CobCuentum>? cuentas = base.GetAll().OrderBy(x => x.Name).ToList();
            return (_mapper.Map<List<CobCuentDto>>(cuentas));
        }

        public async Task<CobCuentDto> GetById(string id)
        {
            CobCuentum? cuenta = await Get(Guid.Parse(id));
            return _mapper.Map<CobCuentDto>(cuenta);
        }

        public async Task<CobCuentDto> Insert(CobCuentDto cobCuentum)
        {
            cobCuentum.Id = Guid.NewGuid();
            await Add(_mapper.Map<CobCuentum>(cobCuentum));
            return cobCuentum;
        }

        private void InsertMovimiento(CobCuentaMovimientoDto cobCuentaMovimiento)
        {
            cobCuentaMovimiento.Id = Guid.NewGuid();
            _cuentasMovimientos.Add(_mapper.Map<CobCuentaMovimiento>(cobCuentaMovimiento));
        }

        public async Task<CobCuentDto> Update(CobCuentDto cobCuentum)
        {
            await Update(_mapper.Map<CobCuentum>(cobCuentum));
            return cobCuentum;
        }
    }
}
