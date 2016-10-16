using System;
using Salvis.Entities;

namespace Salvis.Framework.Helpers
{
    public class DateHelper
    {


        /// <summary>
        /// Cleans and reach a DateTime to his lowest hour.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime CleanHour(DateTime date)
        {
            return date.AddMinutes(-date.Minute).AddSeconds(-date.Second).AddMilliseconds(-date.Millisecond);
        }

        public static int GetIntervalsInTime(DateTime startDate, DateTime endDate, int timeInterval)
        {
            var round = Math.Round((endDate - startDate).TotalDays / timeInterval);
            return (int)round;
        }

        /// <summary>
        /// Gets the next valid hour in a clean (without minutes or seconds) format.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="hours">Hours to up for, default 1.</param>
        /// <returns></returns>
        public static DateTime GetNextHour(DateTime start, int hours = 1)
        {
            return CleanHour(start.AddHours(hours));
        }

        /// <summary>
        /// Aggregate a range of Time based on a TimeInterval.
        /// </summary>
        /// <param name="timeInterval">Indicates the interval of time to aggregate for.</param>
        /// <param name="initDate"></param>
        /// <returns>DateTime with the aggregated interval.</returns>
        public static DateTime AggregateValidDate(TimeInterval timeInterval, DateTime initDate)
        {
            switch (timeInterval)
            {
                case TimeInterval.Dialy:
                case TimeInterval.Weekly:
                case TimeInterval.BiWeekly:
                    initDate = initDate.AddDays((int)timeInterval);
                    break;
                case TimeInterval.Monthly:
                    initDate = initDate.AddMonths(1);
                    break;
                case TimeInterval.Quarterly:
                    initDate = initDate.AddMonths(3);
                    break;
                default:
                    initDate = initDate.AddDays((int)timeInterval);
                    break;
            }
            return initDate;
        }

    }
}
