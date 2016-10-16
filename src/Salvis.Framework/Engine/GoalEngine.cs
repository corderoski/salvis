using System;
using System.Linq;
using System.Collections.ObjectModel;
using Salvis.Entities;
using Salvis.Framework.Helpers;
using System.Collections.Generic;

namespace Salvis.Framework.Engine
{
    /// <summary>
    /// Class that represents a group of operations based on behavior.
    /// </summary>
    internal class GoalEngine : IEngine
    {

        public static IEnumerable<Operation> CreateRecurrent(DateTime startDate, DateTime endDate, Double amount, TimeInterval timeInterval)
        {
            if (timeInterval < TimeInterval.Dialy)
            {
                timeInterval = TimeInterval.Monthly;
            }

            var initDate = startDate.AddDays(0);          // ¿substract one day? Yes: 1-15, No: 1-16

            var shares = DateHelper.GetIntervalsInTime(startDate, endDate, (int)timeInterval);

            //  instances
            var operations = new Collection<Operation>();

            //assignment
            for (var i = 0; i <= shares; i++)
            {
                var item = new Operation()
                {
                    IntervalTypeId = timeInterval,
                    ExpValue = amount,
                    InputDate = initDate,
                    RealValue = null,
                    EffectedDate = null
                };
                operations.Add(item);
                initDate = DateHelper.AggregateValidDate(timeInterval, initDate);
            }

            return operations;
        }

        public static IEnumerable<Operation> Create(Goal goal, TimeInterval timeInterval)
        {
            if (timeInterval < TimeInterval.Dialy)
            {
                timeInterval = TimeInterval.Monthly;
            }

            var shares = DateHelper.GetIntervalsInTime(goal.StartDate, goal.EndDate,
                (int)timeInterval);

            //  instances
            var operations = new Collection<Operation>();

            var partAmount = goal.Amount / shares;
            var initDate = goal.StartDate.AddDays(-1);          // ¿substract one day? Yes: 1-15, No: 1-16
            //  construct
            for (var i = 0; i < shares; i++)
            {
                initDate = DateHelper.AggregateValidDate(timeInterval, initDate);
                var item = new Operation()
                    {
                        IntervalTypeId = timeInterval,
                        ExpValue = partAmount,
                        InputDate = initDate,
                        RealValue = null,
                        EffectedDate = null
                    };
                operations.Add(item);
            }

            //  joining
            return operations;
        }

        public static OperationExpected GetOperationExpected(Goal goal)
        {
            var entity = new OperationExpected
                {
                    RealAmount = goal.OperationDetails.First().ExpValue
                };

            //  Pending trans: looks for ineffected operations with no amount specified.
            entity.ExpAmount = goal.OperationDetails.Where(LatestNotEffectedOperationsPredicate).Sum(p => p.ExpValue);

            //  Date = Coming operation / step
            var nextDate = goal.OperationDetails.OrderBy(opd => opd.InputDate)
                                .FirstOrDefault(NextComingOperationPredicate);
            if (nextDate != null) entity.NextDate = nextDate.InputDate;

            return entity;
        }

        #region Private functions

        private static bool LatestNotEffectedOperationsPredicate(Operation opd)
        {
            return !opd.EffectedDate.HasValue && !opd.RealValue.HasValue;
        }

        private static bool NextComingOperationPredicate(Operation opd)
        {
            return !opd.EffectedDate.HasValue && opd.InputDate > DateTimeOffset.Now.DateTime.Date;
        }

        #endregion

    }
}
