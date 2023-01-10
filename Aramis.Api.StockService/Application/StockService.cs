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
            return _stockRepository.Delete(id);
        }

        public StockProductDto GetById(string id)
        {
            StockProduct? product = _stockRepository.GetProduct(id);
            return _mapper.Map<StockProduct, StockProductDto>(product);
        }

        public StockIvaDto GetIva(string id)
        {
            StockIva? iva = _stockRepository.GetIva(id);
            return _mapper.Map<StockIva, StockIvaDto>(iva);
        }

        public List<StockIvaDto> GetIvaList()
        {
            List<StockIva>? ivas = _stockRepository.GetIvas().ToList();
            return _mapper.Map<List<StockIva>, List<StockIvaDto>>(ivas);
        }

        public List<StockProductDto> GetList()
        {
            List<StockProduct>? products = _stockRepository.GetProducts().ToList();
            return _mapper.Map<List<StockProduct>, List<StockProductDto>>(products);
        }

        public StockRubroDto GetRubro(string id)
        {
            StockRubro? rubro = _stockRepository.GetRubro(id);
            return _mapper.Map<StockRubro, StockRubroDto>(rubro);
        }

        public List<StockRubroDto> GetRubroList()
        {
            List<StockRubro>? rubros = _stockRepository.GetRubroList().ToList();
            return _mapper.Map<List<StockRubro>, List<StockRubroDto>>(rubros);

        }

        public StockProductDto Insert(StockProductInsert product)
        {
            product.Id = Guid.NewGuid();
            _stockRepository.Insert(_mapper.Map<StockProductInsert, StockProduct>(product));
            return GetById(product.Id.ToString()!);
        }

        public bool InsertRubro(StockRubroDto rubro)
        {
            return _stockRepository.InsertRubro(_mapper.Map<StockRubroDto, StockRubro>(rubro));
        }

        public StockProductDto Update(StockProductInsert product)
        {
            _stockRepository.Update(_mapper.Map<StockProductInsert, StockProduct>(product));
            return GetById(product.Id.ToString()!);
        }

        public bool UpdateRubro(StockRubroDto rubro)
        {
            return _stockRepository.UpdateRubro(_mapper.Map<StockRubroDto, StockRubro>(rubro));
        }
    }
}
