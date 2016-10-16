using Salvis.Entities;
using System;
using System.Data;
using System.Linq;
using Dapper;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public class OperationRepository : RepositoryBase<Operation>, IOperationRepository
    {

        public OperationRepository(IDbConnection connection)
            : base(connection)
        {
        }

        new public IEnumerable<Operation> Add(IEnumerable<Operation> items)
        {
            {   //Validations
                if (!items.Any()) throw new TypeNotAsExpectedException("The passed Operations must not be empty.");
                var first = items.FirstOrDefault();
                if (first == null || items.All(op => op.GoalId != first.GoalId && op.GoalTypeId != first.GoalTypeId))
                    throw new TypeNotAsExpectedException("The passed Operations doesn't reference the same Primary Values.");
            }
            base.Add(items);
            return items;
        }

        public IEnumerable<Operation> Get(long goalId, GoalEntityType goalTypeId)
        {
            var sqlSelect = String.Format("SELECT * FROM {0} WHERE [GoalId] = @goalId AND [GoalTypeId] = @goalTypeId", EntityTableSchema);
            var entities = Connection.Query<Operation>(sqlSelect, new { goalId, goalTypeId });
            return entities;
        }

        public int OperationsCount(long goalId, GoalEntityType goalTypeId)
        {
            throw new NotImplementedException();
        }

        public int OperationsPendingCount(long goalId, GoalEntityType goalTypeId)
        {
            throw new NotImplementedException();
        }


    }


}
