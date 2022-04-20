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
    }
}
