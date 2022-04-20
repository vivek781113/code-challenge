using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using refactor_me.Filters;
using refactor_me.ProductOptionService;
using refactor_me.ProductService;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("api/products")]
    [ProductsExceptionFilter]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productionOptionService;

        /// <summary>
        /// Constructore of the controller
        /// </summary>
        /// <param name="productService">product service instacne, unity will take care of resolving & registering the serivce</param>
        /// <param name="productOptionService">productOptionService, unity will take care of resolving & registering the serivce</param>
        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            ////test the globle exception handler execution
            //_productService = _productService ?? new ProductService();
            _productService = productService;
            _productionOptionService = productOptionService;
        }


        #region Product Actions

        [Route]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productService.GetProducts();
        }

        [HttpGet]
        [Route("searchByName/{name}")]
        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            return await _productService.GetProductsByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> Create(Product product)
        {

            await _productService.Create(product);
            return Created($"/products/{product.Id}", product);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, Product product)
        {
            if (!_productService.ProductExist(id))
                return Conflict();

            await _productService.Update(product);
            return Ok();

        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await _productService.Delete(id);
        }
        
        #endregion

        
        [Route("{productId}/options")]
        [HttpGet]
        public async Task<IEnumerable<ProductOption>> GetOptions(Guid productId)
        {
            return await _productionOptionService.GetProductOptions(productId);
            //return new ProductOptions(productId);
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
