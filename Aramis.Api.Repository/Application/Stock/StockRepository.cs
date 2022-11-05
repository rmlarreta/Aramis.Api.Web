using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Stock;
using Aramis.Api.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Application.Stock
{
    public class StockRepository : IStockRepository
    {
        private readonly IGenericRepository<StockProduct> _repository;
        private readonly IGenericRepository<StockRubro> _rubrosRepository;
        private readonly IGenericRepository<StockIva> _ivaRepository;
        private readonly AramisbdContext _context;
        public StockRepository(IGenericRepository<StockProduct> repository, IGenericRepository<StockRubro> rubrosRepository, IGenericRepository<StockIva> ivaRepository, AramisbdContext context)
        {
            _repository = repository;
            _rubrosRepository = rubrosRepository;
            _ivaRepository = ivaRepository;
            _context = context;
        }

        public bool Insert(StockProduct data)
        {
            return _repository.Add(data);
        }

        public bool Delete(string id)
        {
            return _repository.Delete(Guid.Parse(id));
        }

        public bool Update(StockProduct data)
        {
            return _repository.Update(data);
        }

        public IEnumerable<StockProduct> GetProducts()
        {
            return _context.StockProducts.Include(x => x.IvaNavigation)
                 .Include(x => x.RubroNavigation)
                 .ToList();
        }

        public StockProduct GetProduct(string id)
        {
            return _context.StockProducts.Include(x => x.IvaNavigation)
               .Include(x => x.RubroNavigation)
               .Where(x => x.Id.Equals(id))
               .SingleOrDefault()!;
        }

        #region Rubros
        public StockRubro GetRubro(string id)
        {
            return _rubrosRepository.Get(Guid.Parse(id));
        }

        public IEnumerable<StockRubro> GetRubroList()
        {
            return _rubrosRepository.Get();
        }

        public bool InsertRubro(StockRubro rubro)
        {
            return _rubrosRepository.Add(rubro);
        }

        public bool UpdateRubro(StockRubro rubro)
        {
            return _rubrosRepository.Update(rubro);
        }

        public bool DeleteRubro(string id)
        {
            return _rubrosRepository.Delete(Guid.Parse(id));
        }
        #endregion Rubros
        #region #iva
        public StockIva GetIva(string id)
        {
            return _ivaRepository.Get(Guid.Parse(id));
        }

        public IEnumerable<StockIva> GetIvas()
        {
            return _ivaRepository.Get();
        }
        #endregion#iva




    }
}
