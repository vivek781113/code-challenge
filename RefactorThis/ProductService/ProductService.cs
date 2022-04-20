using refactor_me.Repository;
using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<Product> GetProductById(Guid Id)
        {
            try
            {
                return await _repo.GetAsync(Id);

            }
            catch (KeyNotFoundException keyNFEx)
            {
                //log this in file or db
                Debug.WriteLine($"{keyNFEx.Data}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }

        public async Task Create(Product product)
        {
           await _repo.InsertAsync(product);
        }

        public bool ProductExist(Guid id)
        {
            return _repo.ProductExist(id);
        }

        public async Task Update(Product product)
        {
            await _repo.UpdateAsync(product);
        }

        public async Task Delete(Guid id)
        {
            await _repo.DeleteRowAsync(id);
        }
    }
}