using Aramis.Api.Commons.ModelsDto.Stock;
using Aramis.Api.Repository.Interfaces.Stock;
using Aramis.Api.Repository.Models;
using Aramis.Api.StockService.Interfaces;
using AutoMapper;

namespace Aramis.Api.StockService.Application
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        public bool Delete(string id)
        {
        return  _stockRepository.Delete(id);
        }

        public StockProductDto GetById(string id)
        { 
          var product= _stockRepository.GetProduct(id);
            return _mapper.Map<StockProduct,StockProductDto>(product);
        }

        public StockIva GetIva(string id)
        {
            return _stockRepository.GetIva(id);
        }

        public List<StockIva> GetIvaList()
        {
            return _stockRepository.GetIvas().ToList();
        }

        public List<StockProductDto> GetList()
        {
            var products = _stockRepository.GetProducts().ToList();
            return _mapper.Map<List<StockProduct>, List<StockProductDto>>(products);
        }

        public StockRubro GetRubro(string id)
        {
            return _stockRepository.GetRubro(id);
        }

        public List<StockRubro> GetRubroList()
        {
            return _stockRepository.GetRubroList().ToList();
        }

        public bool Insert(StockProductInsert product)
        {
          return _stockRepository.Insert(_mapper.Map<StockProductInsert, StockProduct>(product));
        }

        public bool InsertRubro(StockRubro rubro)
        {
            return _stockRepository.InsertRubro(rubro);
        }

        public bool Update(StockProductInsert product)
        {
          return _stockRepository.Update(_mapper.Map<StockProductInsert, StockProduct>(product));
        }

        public bool UpdateRubro(StockRubro rubro)
        {
            return UpdateRubro(rubro);
        }
    }
}
