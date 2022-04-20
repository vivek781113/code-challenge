using refactor_this.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByName(string name);
    }
}
