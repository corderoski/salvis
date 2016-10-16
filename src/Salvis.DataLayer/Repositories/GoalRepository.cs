using Salvis.Entities;
using System;
using System.Data;
using System.Linq;
using Dapper;

namespace Salvis.DataLayer.Repositories
{
    public class GoalRepository : RepositoryBase<Goal>, IGoalRepository
    {

        private readonly IOperationRepository _operationRepository;

        public GoalRepository(IOperationRepository operationRepository,
                            IDbConnection connection)
            : base(connection)
        {
            _operationRepository = operationRepository;
        }

        public new Goal Add(Goal item)
        {
            if (item == null) throw new ArgumentNullException("item");

            foreach (var operation in item.OperationDetails)
            {
                operation.GoalId = item.ParentId;
                operation.GoalTypeId = item.ParentTypeId;
            }

            base.Add(item);
            _operationRepository.Add(item.OperationDetails);
           
            return item;
        }

        public void Delete(long parentId, GoalEntityType parentTypeId)
        {
            var sql = String.Format("DELETE FROM {0} WHERE ParentId = @id AND ParentTypeId = @typeId", EntityTableSchema);
            Connection.Execute(sql, new { id = parentId, typeId = (int)parentTypeId });

        }

        public Goal Get(long parentId, GoalEntityType parentTypeId)
        {
            var sql = String.Format("SELECT * FROM {0} WHERE ParentId = @id AND ParentTypeId = @typeId", EntityTableSchema);
            var entity = Connection.Query<Goal>(sql, new { id = parentId, typeId = (int)parentTypeId }).SingleOrDefault();

            if (entity != null)
                entity.OperationDetails = _operationRepository.Get(entity.ParentId, entity.ParentTypeId);

            return entity;
        }

        public int GetShares(long parentId, int parentTypeId)
        {
            throw new NotImplementedException("This function is pending of re-implementation.");

            /*
            var sql = String.Format("SELECT  COUNT(*) FROM [dbo].[{0}] op " +
            " LEFT JOIN [dbo].[{1}] opd ON op.GoalId = opd.OperationId AND op.GoalTypeId = opd.OperationTypeId" +
            " WHERE op.GoalId = @parentId AND op.GoalTypeId = @GoalTypeId; ", typeof(Operation).Name, typeof(OperationDetail).Name);

            var result = Connection.ExecuteScalar(sql, new {parentId, parentTypeId});
            return Convert.ToInt32(result);*/
        }


    }
}
