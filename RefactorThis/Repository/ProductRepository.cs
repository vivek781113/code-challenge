using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(string tableName = "Product") : base(tableName)
        {
        }
    }
}