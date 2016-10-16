using Salvis.Entities;

namespace Salvis.DataLayer.Repositories
{
    public interface IGoalRepository : IRepository<Goal>
    {
        Goal Add(Goal item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId">The parent's ID</param>
        /// <param name="parentTypeId">The Parent's Type or source.</param>
        /// <returns>Matched item.</returns>
        Goal Get(long parentId, GoalEntityType parentTypeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId">The parent's ID</param>
        /// <param name="parentTypeId">The Parent's Type or source.</param>
        void Delete(long parentId, GoalEntityType parentTypeId);

        /// <summary>
        /// Gets the count of steps/shares for the indicated goal.
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="p"></param>
        /// <param name="parentTypeId"></param>
        /// <returns>Quantity</returns>
        int GetShares(long parentId, int parentTypeId);



    }
}
