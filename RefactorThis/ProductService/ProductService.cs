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

        public ProductService(ProductRepository repo)
        {
            //_repo = _repo ?? new ProductRepository();
            _repo = repo;
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
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                await _repo.InsertAsync(product);

            }
            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }

        public bool ProductExist(Guid id)
        {
            try
            {

                return _repo.ProductExist(id);
            }
            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                await _repo.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _repo.DeleteRowAsync(id);
            }
            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }

        }
    }
}