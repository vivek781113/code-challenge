using refactor_me.ProductOptionService;
using refactor_me.ProductService;
using refactor_me.Repository;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace refactor_me
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here

            container.RegisterType<ProductRepository, ProductRepository>();
            container.RegisterType<ProductOptionRepository, ProductOptionRepository>();

            container.RegisterType<IProductService, ProductService.ProductService>();
            container.RegisterType<IProductOptionService, ProductOptionService.ProductOptionService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}