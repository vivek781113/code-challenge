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
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            ////test the globle exception handler execution
            //throw new Exception("exeption in products controller");
            //_productService = _productService ?? new ProductService();
            _productService = productService;
        }

        [Route]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productService.GetProducts();

            //old code
            //return new Products();
        }

        //http://localhost:58123/api/products/SearchByName/galaxy
        [HttpGet]
        [Route("searchByName/{name}")]
        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            return await _productService.GetProductsByName(name);

            //old code
            //return new Products(name);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);

            ///old code
            //var product = new Product(id);
            //if (product.IsNew)
            //    throw new HttpResponseException(HttpStatusCode.NotFound);

            //return product;
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> Create(Product product)
        {

            await _productService.Create(product);
            return Created($"/products/{product.Id}", product);
            //product.Save();
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, Product product)
        {
            if (!_productService.ProductExist(id))
                return Conflict();

            await _productService.Update(product);
            return Ok();

            //var orig = new Product(id)
            //{
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    DeliveryPrice = product.DeliveryPrice
            //};

            //if (!orig.IsNew)
            //    orig.Save();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await _productService.Delete(id);
            //var product = new Product(id);
            //product.Delete();
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
