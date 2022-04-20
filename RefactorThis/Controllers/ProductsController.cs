using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using refactor_me.Filters;
using refactor_me.Models;
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
        public async Task<IHttpActionResult> Create([FromBody]CreateProductDto dto)
        {
            //this can be simpilfied with auto mapper
            Product product = MapProductDtoToProduct(dto);
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
        private static Product MapProductDtoToProduct(CreateProductDto dto)
        {
            return new Product
            {
                DeliveryPrice = dto.DeliveryPrice,
                Description = dto.Description,
                Name = dto.Name,
                Price = dto.Price
            };
        }

        #endregion

        #region Prodcut Options

        [Route("{productId}/options")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOptions(Guid productId)
        {
            if (!_productService.ProductExist(productId))
                return NotFound();

            return Ok(await _productionOptionService.GetProductOptions(productId));
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOption(Guid productId, Guid id)
        {
            if (!_productService.ProductExist(productId))
                return NotFound();

            var option = await _productionOptionService.GetProductOption(id);
            if (option == null)
                return NotFound();

            return Ok(option);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOption(Guid productId, CreateProductOptionDto dto)
        {

            if (!_productService.ProductExist(productId))
                return NotFound();

            //this can be simplified with automapper
            var option = new ProductOption
            {
                Name = dto.Name,
                Description = dto.Description,
                ProductId = productId,
            };

            await _productionOptionService.Create(option);

            return Created($"/products/{productId}/options", option);

        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateOption(Guid id, ProductOption option)
        {

            var productOption = await _productionOptionService.GetProductOption(id);

            if (productOption == null)
                return Conflict();

            await _productionOptionService.Update(option);
            return Ok();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public async Task DeleteOption(Guid id)
        {
            await _productionOptionService.Delete(id);
        }
        
        #endregion
    }
}
