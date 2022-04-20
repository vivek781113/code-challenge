using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using refactor_me.Filters;
using refactor_me.ProductService;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("api/products")]
    [ProductsExceptionFilter]
    public class ProductsController : ApiController
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            ////test the gloable exception handler execution
            //throw new Exception("exeption in products controller");
            _productService = _productService ?? new ProductService();
        }

        [Route]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            //return new Products();
            return await _productService.GetProducts();
        }

        //http://localhost:58123/api/products/SearchByName/galaxy
        [HttpGet]
        [Route("searchByName/{name}")]
        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            //return new Products(name);
            return await _productService.GetProductsByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = new Product(id);
            if (product.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            product.Save();
            return Created("", product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            var orig = new Product(id)
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            var product = new Product(id);
            product.Delete();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return new ProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = new ProductOption(id);
            if (option.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = new ProductOption(id)
            {
                Name = option.Name,
                Description = option.Description
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var opt = new ProductOption(id);
            opt.Delete();
        }
    }
}
