using Autofac;
using Ploeh.AutoFixture;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Salvis.DataLayer.Repositories;
using Salvis.Framework.Services;

namespace Salvis.Tests
{
    internal abstract class CompositionRoot
    {

        /*
        public void DefaultTestMethod()
        {
            using (var trans = new TransactionScope())
            {
                var fixture = CompositionRoot.FixtureInstance;
                using (var scope = CompositionRoot.GetBuilder.BeginLifetimeScope())
                {
                }
                trans.Complete();
            }
        }
         */

        internal static IFixture FixtureInstance
        {
            get
            {
                var fix = new Fixture();
                fix.Behaviors.Remove(new ThrowingRecursionBehavior());
                fix.Behaviors.Add(new OmitOnRecursionBehavior());
                return fix;
            }
        }

        internal static IContainer GetBuilder
        {
            get
            {
                var builder = new ContainerBuilder();
                //  SqlConnection / IDbConnection / ConnectionString
                var connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                builder.RegisterType<SqlConnection>() // this sends the connString to the SqlConnection.ctor
                    .WithParameter("connectionString", connString).As<IDbConnection>();

                LoadComponents(ref builder, typeof(IUserRepository).Assembly); //Contract
                LoadComponents(ref builder, typeof(UserRepository).Assembly); //Implementation

                LoadComponents(ref builder, typeof(IUserService).Assembly); //Contract
                LoadComponents(ref builder, typeof(UserService).Assembly); //Contract

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
