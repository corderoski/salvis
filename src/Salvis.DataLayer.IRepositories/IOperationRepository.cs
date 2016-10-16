using Salvis.Entities;
using System;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface IOperationRepository : IRepositoryBaseEnumerableOperation<Operation>
    {

        [Obsolete("Use Get(goalId, goalTypeId)", true)]
        new Operation Get(int id);

        [Obsolete("Use Get(goalId, goalTypeId))", true)]
        new Operation Get(long id);

        IEnumerable<Operation> Get(long goalId, GoalEntityType goalTypeId);

        int OperationsCount(long goalId, GoalEntityType goalTypeId);

        int OperationsPendingCount(long goalId, GoalEntityType goalTypeId);

        [Obsolete("", true)]
        new void Update(IEnumerable<Operation> items);

    }


}
