using refactor_me.Repository;
using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace refactor_me.ProductOptionService
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly ProductOptionRepository _repo;

        public ProductOptionService(ProductOptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            try
            {
                string query = $"where productid = '{productId}'";
                return await _repo.GetAllAsync(query);
            }
            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }
        }
        public async Task<ProductOption> GetProductOption(Guid Id)
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
                throw new Exception("Something bad happer while fetching the product option");
            }
        }

        public async Task Create(ProductOption option)
        {
            try
            {
                await _repo.InsertAsync(option);
            }

            catch (Exception ex)
            {
                //log the exception to file or db
                Debug.WriteLine($"message: {ex.Message}\nstack-trace:{ex.StackTrace}");
                throw new Exception("Something bad happer while fetching the product");
            }

        }

        public async Task Update(ProductOption option)
        {
            try
            {
                await _repo.UpdateAsync(option);
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