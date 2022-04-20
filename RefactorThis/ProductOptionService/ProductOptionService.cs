using refactor_me.Repository;
using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.ProductOptionService
{
    public class ProductOptionService : IProductOptionService
    {
        private ProductOptionRepository _repo;

        public ProductOptionService(ProductOptionRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            string query = $"where productid = '{productId}'";
            return await _repo.GetAllAsync(query);
        }
    }
}