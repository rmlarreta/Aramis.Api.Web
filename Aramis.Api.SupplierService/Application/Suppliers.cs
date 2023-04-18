using Aramis.Api.Commons.ModelsDto.Suppliers;
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
        private readonly IMapper _mapper;

        public Suppliers(ISuppliersRepository repository, IOperacionesService operaciones, IMapper mapper)
        {
            _repository = repository;
            _operaciones = operaciones;
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
            return _mapper.Map<List<OpDocumentoProveedorDto>>(_repository.GetAll().Where(x => x.EstadoId.Equals(state)).ToList());
        }

        public bool InsertDocument(OpDocumentoProveedorDto documento)
        {
            documento.Id = Guid.NewGuid();
            documento.EstadoId = _operaciones.Estados().Where(x => x.Name.Equals("ABIERTO")).FirstOrDefault()!.Id;
            _repository.Add(_mapper.Map<OpDocumentoProveedor>(documento));
            return _repository.Save();
        }
    }
}

