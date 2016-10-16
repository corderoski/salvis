using System;
using Salvis.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Salvis.DataLayer.Model
{

    internal class DataModelContainer : DbContext
    {
        
        public DataModelContainer(String nameOrConnectionString = null)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<DataModelContainer>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            
            base.OnModelCreating(modelBuilder);
        }

        public IDbConnection GetInstance()
        {
            return Database.Connection;
        }

        /// <summary>
        /// Implements a Secured changes saving throwing try-catch exceptions errors.
        /// </summary>
        public void SecureSaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var inError in error.ValidationErrors)
                    {
                        throw new Exception(inError.ErrorMessage);
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                foreach (var item in ex.Entries)
                {
                    throw new Exception(ex.Message + Environment.NewLine + item.Entity, ex.InnerException);
                }
                throw new Exception(ex.Message + Environment.NewLine + ex.InnerException.Message);
            }
            catch
            {
                throw;
            }
        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupDetail> GroupDetails { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Planification> Planifications { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConfiguration> UserConfigurations { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Recurrent> Recurrents { get; set; }
        public DbSet<Tip> Tips { get; set; }

    }
}
