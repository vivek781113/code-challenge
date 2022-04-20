using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<Product> GetProductById(Guid Id);
        Task Create(Product product);
        bool ProductExist(Guid id);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
