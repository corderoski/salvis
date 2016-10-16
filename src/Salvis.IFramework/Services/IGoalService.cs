using System;
using System.Collections.Generic;
using Salvis.Entities;

namespace Salvis.Framework.Services
{
    public interface IGoalService<T> : IServiceBaseOperation<T> where T : class
    {

        T Add(T item, TimeInterval timeInterval);

        bool DeleteByCode(String code);

        /// <summary>
        /// Request an item for his public id or code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>The matched element</returns>
        T GetByCode(String code);

        IEnumerable<T> GetByUserId(string userId);

        OperationExpected NextExpectedOperation(Goal goal);

        /// <summary>
        /// Validates the basic values for a goal asumming a vast range of Nullables.
        ///  <seealso cref="Validate(System.DateTime,System.Nullable{System.DateTime},System.Nullable{double},System.Nullable{double},Salvis.Entities.TimeInterval)"/>
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="timeInterval"></param>
        /// <param name="partAmount"></param>
        /// <param name="fullAmount"></param>
        /// <returns>An IServiceResult containing messages and a Goal object.</returns>
        IServiceResult Validate(DateTime? startDate, DateTime? endDate,
            Double? partAmount, Double? fullAmount, TimeInterval timeInterval = TimeInterval.Monthly);

        /// <summary>
        /// Validates the basic values for a goal.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="partAmount"></param>
        /// <param name="fullAmount"></param>
        /// <param name="timeInterval"></param>
        /// <returns>An IServiceResult containing messages and a Goal object.</returns>
        IServiceResult Validate(DateTime startDate, DateTime? endDate,
            Double? partAmount, Double? fullAmount, TimeInterval timeInterval);

        /// <summary>
        /// Indicates the total of shares / operations for a goal.
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        //int GetShares(Int64 parentId, GoalEntityType parentType);

        /// <summary>
        /// Create a line graph based on OperationDetails.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="realValue">Kind of value (Amount real OR Amount Expected.</param>
        /// <param name="margin"></param>
        /// <param name="max"></param>
        /// <returns>A list of point of the line graph.</returns>
        dynamic BuilderGraphic(Goal goal, bool realValue = false, int margin = 4, int max = 9);

    }
}
