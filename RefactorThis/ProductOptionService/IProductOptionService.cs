using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace refactor_me.ProductOptionService
{
    public interface IProductOptionService
    {
        Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);
        Task<ProductOption> GetProductOption(Guid Id);
        Task Create(ProductOption option);
        Task Update(ProductOption option);
        Task Delete(Guid id);
    }
}
