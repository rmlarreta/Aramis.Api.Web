using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class TipoPagoService : ITipoPagoService
    {
        private readonly IService<CobTipoPago> _repository;
        private readonly IService<CobPo> _points;
        private readonly IMapper _mapper;
        public TipoPagoService(IService<CobTipoPago> repository, IService<CobPo> points, IMapper mapper)
        {
            _repository = repository;
            _points = points;
            _mapper = mapper;
        }

        public bool Delete(string id)
        {
            _repository.Delete(Guid.Parse(id));
            return _repository.Save();
        }

        public List<CobTipoPagoDto> GetAll()
        {
            return _mapper.Map<List<CobTipoPago>, List<CobTipoPagoDto>>(_repository.Get().OrderBy(x => x.Cuenta).ToList());
        }

        public CobTipoPagoDto GetById(string id)
        {
            return _mapper.Map<CobTipoPago, CobTipoPagoDto>(_repository.Get(Guid.Parse(id)));
        }

        public CobTipoPagoDto Insert(CobTipoPagoDto cobTipoPago)
        {
            cobTipoPago.Id = Guid.NewGuid();
            _repository.Add(_mapper.Map<CobTipoPagoDto, CobTipoPago>(cobTipoPago));
            return cobTipoPago;
        }

        public CobTipoPagoDto Update(CobTipoPagoDto cobTipoPago)
        {
            _repository.Update(_mapper.Map<CobTipoPagoDto, CobTipoPago>(cobTipoPago));
            return cobTipoPago;
        } 
        public List<CobPosDto> GetPost()
        {
            return _mapper.Map<List<CobPo>, List<CobPosDto>>(_points.Get().OrderBy(x => x.DeviceId).ToList());
        }
    }
}
