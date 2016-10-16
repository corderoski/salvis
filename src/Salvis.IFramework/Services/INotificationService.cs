using System;
using System.Collections.Generic;
using Salvis.Entities;
using Salvis.Entities.Notifications;

namespace Salvis.Framework.Services
{
    public interface INotificationService : IService
    {

        Notification Add(Notification notification);

        IEnumerable<Notification> Add(Goal goal, IEnumerable<Notification> items, IEnumerable<TimeInterval> timeIntervals);

        void Delete(Notification notification);

        void Delete(IEnumerable<Notification> items);

        IEnumerable<Notification> GetByTimeIntervals(DateTime init, DateTime final);

        /// <summary>
        /// Request all the programmed notifications for a given Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<Notification> GetByDate(DateTime date);

    }
}
