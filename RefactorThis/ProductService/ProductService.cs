using refactor_me.Repository;
using refactor_this.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repo;

        public ProductService()
        {
            _repo = _repo ?? new ProductRepository();
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            string query = $"where lower(name) like '%{name.ToLower()}%'";
            return await _repo.GetAllAsync(query);
        }
    }
}