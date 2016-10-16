using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Salvis.Entities;

namespace Salvis.DataLayer.Repositories
{
    public class SavingRepository : RepositoryBase<Saving>, ISavingRepository
    {

        private readonly String _sqlQueryJoinGoal = String.Format("SELECT * FROM dbo.[{0}] S INNER JOIN dbo.[{1}] G ON G.GoalId = S.Id ", typeof(Saving).Name, typeof(Goal).Name);

        private readonly IGoalRepository _goalRepository;
        private readonly ICatalogRepository _catalogRepository;

        public SavingRepository(IGoalRepository goalRepository, ICatalogRepository catalogRepository, IDbConnection connection)
            : base(connection)
        {
            if (catalogRepository == null) throw new ArgumentNullException("catalogRepository");
            _goalRepository = goalRepository;
            _catalogRepository = catalogRepository;
        }

        public new Saving Add(Saving item)
        {
            if (item == null) throw new ArgumentNullException("item");

            base.Add(item);
            item.Goal.ParentId = item.Id;
            item.Goal.ParentTypeId = GoalEntityType.Saving;
            _goalRepository.Add(item.Goal);
            return item;
        }

        new public Saving Get(long id)
        {
            var entity =
                Connection.Query<Saving>(String.Format("SELECT * FROM {0} where Id = @id",
                EntityTableSchema), new { id }).SingleOrDefault();
            if (entity == null)
                return null;

            entity.Goal = _goalRepository.Get(entity.Id, GoalEntityType.Saving);
            //var reason = _catalogRepository.Get(Catalogs.SAVING_TYPE, entity.ReasonTypeId);
            return entity;
        }

        public Saving GetByCode(string code)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE Code = @code", EntityTableSchema);
            var entity = Connection.Query<Saving>(sql, new { code }).SingleOrDefault();

            if (entity == null)
                return null;
            entity.Goal = _goalRepository.Get(entity.Id, GoalEntityType.Saving);
            return entity;
        }

        public IEnumerable<Saving> GetByUserId(string userId)
        {
            var entities =
                Connection.Query<Saving>(
                    String.Format("SELECT * FROM {0} where UserId = @userId", EntityTableSchema),
                    new { userId });

            var savings = entities.ToList();

            /*
            savings.ForEach(i =>
                {
                    i.Goals = _goalRepository.Get(i.Id, GoalEntityType.Savings);
                    var reason = _catalogRepository.Get(Catalogs.SAVING_TYPE, i.ReasonTypeId);
                    i.Reason = (reason != null) ? reason.Value : String.Empty;
                });
             * */

            var reasons = _catalogRepository.Get(Catalog.SAVING_TYPE);
            savings.ForEach(i =>
            {
                i.Goal = _goalRepository.Get(i.Id, GoalEntityType.Saving);
               // i.Reason = reasons.First(cat => cat.SubCategoryId == i.ReasonTypeId).Value;
            });

            return savings;
        }

    }
}
