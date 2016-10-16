using Autofac;
using Ploeh.AutoFixture;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Salvis.Tests.Framework.OnlineServices.UnitTests
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
                var fix = new Fixture().Customize(new Ploeh.AutoFixture.AutoMoq.AutoMoqCustomization());
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
                var connString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                builder.RegisterType<SqlConnection>() // this sends the connString to the SqlConnection.ctor
                    .WithParameter("connectionString", connString).As<IDbConnection>();

                LoadComponents(ref builder, typeof(Salvis.Framework.OnlineServices.IOnlineService).Assembly); //Contract & Implemenation
                LoadComponents(ref builder, typeof(Salvis.Framework.Services.ISavingService).Assembly); //Contract
                LoadComponents(ref builder, typeof(Salvis.Framework.Services.SavingService).Assembly); //Contract

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
