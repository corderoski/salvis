using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Salvis.App.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<Salvis.DataLayer.DataModelContainer>(null);

            //System.Web.Helpers.AntiForgeryConfig.RequireSsl = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = CreateContainer();

            // MVC Dependency
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);

            // WebAPI Dependency
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["disable"]))
            {
                const string homeDefault = "Home/Default";
                if (!Request.Url.ToString().Contains(homeDefault))
                {
                    Response.Redirect("~/" + homeDefault);
                }
            }
            //TODO: Develop a mechanism for auth the user without request credentials, this, for email usages.
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Get the error details
            HttpException lastErrorWrapper = Server.GetLastError() as HttpException;
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            //  SqlConnection / IDbConnection / ConnectionString
            var connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            builder.RegisterType<System.Data.SqlClient.SqlConnection>() // this sends the connString to the SqlConnection.ctor
                .WithParameter("connectionString", connString).As<System.Data.IDbConnection>();

            //REPOSITORIES
            LoadBuilderComponents(ref builder, typeof(DataLayer.Repositories.IUserRepository).Assembly); //Contract
            LoadBuilderComponents(ref builder, typeof(DataLayer.Repositories.UserRepository).Assembly); //Implementation

            //SERVICES
            LoadBuilderComponents(ref builder, typeof(Framework.OnlineServices.IOnlineService).Assembly); //Contract & Implementation
            LoadBuilderComponents(ref builder, typeof(Framework.Services.IUserService).Assembly); //Contract
            LoadBuilderComponents(ref builder, typeof(Framework.Services.UserService).Assembly); //Implementation

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }

        private static void LoadBuilderComponents(ref ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Adapter"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Factory"))
                   .AsImplementedInterfaces();
        }

    }
}
