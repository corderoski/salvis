using System.Collections.Generic;
using Salvis.Entities;
using System;
using Salvis.Entities.Notifications;

namespace Salvis.DataLayer.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification Add(Notification notification);

        IEnumerable<Notification> Add(IEnumerable<Notification> items);

        void Delete(Notification notification);

        void Delete(IEnumerable<Notification> items);

        IEnumerable<Notification> GetByTimeIntervals(DateTime init, DateTime final);

        /// <summary>
        /// Gets the last registered and completed execution.
        /// </summary>
        /// <returns></returns>
        NotificationBoard GetLastExecution();

        /// <summary>
        /// Gets a new registered execution without it started.
        /// </summary>
        /// <param name="startDate">Indicates the starting date for the new execution. Can be Null.</param>
        /// <returns>A created object representing the execution.</returns>
        NotificationBoard GetNewExecution(DateTime? startDate);

        /// <summary>
        /// Closes an execution and mark it as completed.
        /// </summary>
        /// <param name="item">An execution to close.</param>
        bool CloseExecution(NotificationBoard item);

        

    }
}
