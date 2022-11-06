using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Models;

namespace Aramis.Api.FlowService.Application
{
    public class TipoPagoService : ITipoPagoService
    {
        private readonly IGenericRepository<CobTipoPago> _repository;
        public TipoPagoService(IGenericRepository<CobTipoPago> repository)
        {
            _repository = repository;
        }

        public bool Delete(string id)
        {
           _repository.Delete(Guid.Parse(id));
            return _repository.Save();
        }

        public List<CobTipoPago> GetAll()
        {
            return _repository.Get().ToList();
        }

        public CobTipoPago GetById(string id)
        {
            return _repository.Get(Guid.Parse(id));
        }

        public CobTipoPago Insert(CobTipoPago cobTipoPago)
        {
            cobTipoPago.Id = Guid.NewGuid();
            _repository.Add(cobTipoPago);
            return cobTipoPago;
        }

        public CobTipoPago Update(CobTipoPago cobTipoPago)
        {
            _repository.Update(cobTipoPago);
            return cobTipoPago;
        }
    }
}
