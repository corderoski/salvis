namespace Salvis.App.Web.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Salvis.App.Web.Models.ApplicationDbContext";
        }

        /// <summary>
        /// This method will be called after migrating to the latest version.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {

            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Normal" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Premiun" });

#if DEBUG
            context.Users.AddOrUpdate(
                         new ApplicationUser
                         {
                             Id = "1649023b-0288-4f43-a3e9-d6c39c801c68",
                             Email = "jcordero@corderoski.com",
                             UserName = "jcordero@corderoski.com",
                             Name = "J Corderoski",
                             EmailConfirmed = true,
                             PasswordHash = "ACpvQ+Yp0Zkcr13ftm0pZlu/oJ5Dqu2fz0JVWCh7vJfindCthds34ICIiFzLMzfwLQ==",
                             SecurityStamp = "34509e9d-28d2-4fa7-96bb-55e94d7060fd",
                             PhoneNumber = "809-000-0000",
                             PhoneNumberConfirmed = false,
                             TwoFactorEnabled = false,
                             LockoutEnabled = false,
                             AccessFailedCount = 0,
                             Enable = true
                         }
                       );
#endif

        }
    }
}
