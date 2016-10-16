using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Autofac;

namespace Salvis.App.NotificationManager
{
    class CompositionRoot
    {

        static CompositionRoot()
        {
        }

        public static IContainer GetBuilder
        {
            get
            {
                var builder = new ContainerBuilder();
                //  SqlConnection / IDbConnection / ConnectionString
                var connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                builder.RegisterType<SqlConnection>() // this sends the connString to the SqlConnection.ctor
                       .WithParameter("connectionString", connString).As<IDbConnection>();

                LoadComponents(ref builder, typeof(Salvis.DataLayer.Repositories.IUserRepository).Assembly); //Contract
                LoadComponents(ref builder, typeof(Salvis.DataLayer.Repositories.UserRepository).Assembly); //Implementation
                LoadComponents(ref builder, typeof(Salvis.Framework.Services.ISavingService).Assembly); //Contract
                LoadComponents(ref builder, typeof(Salvis.Framework.Services.SavingService).Assembly); //Implementation

                return builder.Build();
            }
        }

        private static void LoadComponents(ref ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Adapter"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Factory"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();
        }
    }
}