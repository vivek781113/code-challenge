using refactor_this.Models;

namespace refactor_me.Repository
{
    public class ProductOptionRepository : GenericRepository<ProductOption>
    {
        const string TABLE_NAME = "ProductOption";
        public ProductOptionRepository(string tableName = TABLE_NAME) : base(tableName)
        {
        }
    }
}