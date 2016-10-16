using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Salvis.Entities;
using Dapper;

namespace Salvis.DataLayer.Repositories
{
    public class DebtRepository : RepositoryBase<Debt>, IDebtRepository
    {

        private readonly IGoalRepository _goalRepository;

        public DebtRepository(IGoalRepository goalRepository, IDbConnection connection)
            : base(connection)
        {
            _goalRepository = goalRepository;
        }

        public new Debt Add(Debt item)
        {
            if (item == null) throw new ArgumentNullException("item");
            
            base.Add(item);
            item.Goal.ParentId = item.Id;
            item.Goal.ParentTypeId = GoalEntityType.Debt;
            _goalRepository.Add(item.Goal);
            return item;
        }

        public bool DeleteByCode(string code)
        {
            const string sql = "SPC_DELETE_GOAL";
            return Connection.Execute(sql, new { code, type = GoalEntityType.Debt }, commandType: CommandType.StoredProcedure) > 0;
        }

        new public Debt Get(long id)
        {
            var entity =
                Connection.Query<Debt>(String.Format("SELECT * FROM {0} where Id = @id", EntityTableSchema),
                                         new { id }).SingleOrDefault();
            if (entity == null)
                return null;

            entity.Goal = _goalRepository.Get(entity.Id, GoalEntityType.Debt);
            return entity;
        }

        public Debt GetByCode(string code)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE Code = @code", EntityTableSchema);
            var entity = Connection.Query<Debt>(sql, new { code }).SingleOrDefault();

            if (entity == null)
                return null;
            entity.Goal = _goalRepository.Get(entity.Id, GoalEntityType.Debt);
            return entity;
        }

        public IEnumerable<Debt> GetByUserId(string userId)
        {
            var results =
                Connection.Query<Debt>(
                    String.Format("SELECT * FROM {0} where UserId = @userId", EntityTableSchema),
                    new { userId });

            var entities = results as List<Debt> ?? results.ToList();

            if (!entities.Any())
                return null;

            entities.ForEach(i =>
            {
                i.Goal = _goalRepository.Get(i.Id, GoalEntityType.Debt);
            });

            return entities;
        }

    }
}
