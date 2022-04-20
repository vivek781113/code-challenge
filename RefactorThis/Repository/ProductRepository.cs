using Dapper;
using refactor_this.Models;
using System;
using System.Data;

namespace refactor_me.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly IDbConnection _connection;
        const string TABLE_NAME = "Product";

        public ProductRepository(string tableName = TABLE_NAME) : base(tableName)
        {
            _connection = base.CreateConnection();
        }

        public bool ProductExist(Guid id)
        {
            return _connection.ExecuteScalar<bool>($"select count(1) from {TABLE_NAME} where Id=@id", new { id });
        }
    }
}