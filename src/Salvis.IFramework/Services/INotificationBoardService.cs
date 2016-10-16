using System;
using Salvis.Entities;
using Salvis.Entities.Notifications;

namespace Salvis.Framework.Services
{
    public interface INotificationBoardService : IService
    {

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
