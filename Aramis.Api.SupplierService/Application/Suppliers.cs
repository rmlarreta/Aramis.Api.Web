using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.Commons.ModelsDto.Suppliers;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.OperacionesService.Interfaces;
using Aramis.Api.Repository.Interfaces.Proveedores;
using Aramis.Api.Repository.Models;
using Aramis.Api.SupplierService.Interfaces;
using AutoMapper;

namespace Aramis.Api.SupplierService.Application
{
    public class Suppliers : ISuppliers
    {
        private readonly ISuppliersRepository _repository;
        private readonly IOperacionesService _operaciones;
        private readonly ICuentasService _cuentasService;
        private readonly IMapper _mapper;

        public Suppliers(ISuppliersRepository repository, IOperacionesService operaciones, ICuentasService cuentasService, IMapper mapper)
        {
            _repository = repository;
            _operaciones = operaciones;
            _cuentasService = cuentasService;
            _mapper = mapper;
        }

        public OpDocumentoProveedorDto GetById(string id)
        {
            return _mapper.Map<OpDocumentoProveedorDto>(_repository.GetAll().Where(x => x.Id.Equals(id)).FirstOrDefault());
        }

        public List<OpDocumentoProveedorDto> GetByProveedor(string id)
        {
            return _mapper.Map<List<OpDocumentoProveedorDto>>(_repository.GetAll().Where(x => x.ProveedorId.Equals(id)).ToList());
        }

        public List<OpDocumentoProveedorDto> GetByState(string state)
        {
            return _mapper.Map<List<OpDocumentoProveedorDto>>(_repository.GetAll().Where(x => x.EstadoId.Equals(Guid.Parse(state))).ToList());
        }

        public bool InsertDocument(OpDocumentoProveedorDto documento)
        {
            documento.Id = Guid.NewGuid();
            documento.EstadoId = _operaciones.Estados().Where(x => x.Name.Equals("ABIERTO")).FirstOrDefault()!.Id;
            _repository.Add(_mapper.Map<OpDocumentoProveedor>(documento));
            return _repository.Save();
        }

        public bool PagarDocumento(OpDocumentProveedorPago documentProveedorPago)
        {
            documentProveedorPago.Documento!.EstadoId = _operaciones.Estados().Where(x => x.Name.Equals("PAGADO")).FirstOrDefault()!.Id;
            if (!_cuentasService.DebitarPago(documentProveedorPago)) throw new Exception("No ha podido Debitarse");
            _cuentasService.Insert(new CobCuentaMovimientoDto()
            {
                Computa = false,
                Cuenta = documentProveedorPago.Cuenta!.ToString(),
                Debito=true,
                Detalle="Pago Documento Proveedor "+documentProveedorPago.Documento.Numero.ToString()+ " "+documentProveedorPago.Documento.Razon,
                Fecha=DateTime.Now,
                Monto=documentProveedorPago.Documento.Monto,
                Operador=documentProveedorPago.Operador,
                Id=Guid.NewGuid().ToString()
            });
            _repository.Update(_mapper.Map<OpDocumentoProveedor>(documentProveedorPago.Documento));
           return _repository.Save(); 
        } 
         
        public bool UpdateDocument(OpDocumentoProveedorDto documento)
        {
            _repository.Update(_mapper.Map<OpDocumentoProveedor>(documento));
            return _repository.Save();
        }
         
    }
}

