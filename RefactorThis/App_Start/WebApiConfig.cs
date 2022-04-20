using refactor_me.Filters;
using refactor_me.Handlers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace refactor_this
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Filters.Add(new ProductsExceptionFilter());

            /*
             * any error is raised in the following area then exception filter will not work.
             * Error inside the exception filter.
               Exception related to routing.
               Error inside the Message Handlers class.
               Error in Controller Constructor.
             */

            //registering exception handlers
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());


            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
