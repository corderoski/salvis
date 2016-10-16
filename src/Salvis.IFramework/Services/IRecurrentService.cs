using System;
using Salvis.Entities;

namespace Salvis.Framework.Services
{
    public interface IRecurrentService : IGoalService<Recurrent>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="amount"></param>
        /// <param name="timeInterval"></param>
        /// <returns>An IServiceResult containing a Goal object, otherwise, a Null object and error messages.</returns>
        IServiceResult Validate(DateTime startDate, DateTime? endDate, double amount, TimeInterval timeInterval);

        [Obsolete("NotImplemented, instead use Validate with 4 parameters contract.", true)]
        new IServiceResult Validate(DateTime? startDate, DateTime? endDate,
                              Double? partAmount, Double? fullAmount, TimeInterval timeInterval = TimeInterval.Monthly);

        [Obsolete("NotImplemented, instead use Validate with 4 parameters contract.", true)]
        new IServiceResult Validate(DateTime startDate, DateTime? endDate, double? partAmount, double? fullAmount, TimeInterval timeInterval);
        
    }
}
