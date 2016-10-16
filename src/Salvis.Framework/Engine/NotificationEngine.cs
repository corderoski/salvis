using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Salvis.Entities;
using Salvis.Entities.Notifications;
using Salvis.Framework.Helpers;

namespace Salvis.Framework.Engine
{
    /// <summary>
    /// Class that represents a group of operation with notifications based on behavior.
    /// </summary>
    internal class NotificationEngine : IEngine
    {

        public static IEnumerable<Notification> Create(Goal goal, TimeInterval timeInterval)
        {
            //  validations

            //  instances
            var plazos = DateHelper.GetIntervalsInTime(goal.StartDate, goal.EndDate, (int)timeInterval);
            var notifications = new Collection<Notification>();
            //var details = new Collection<Noti>();
            //  construct
            for (int i = 0; i < plazos; i++)
            {
                var item = new Notification()
                    {
                        
                    };
                notifications.Add(item);
            }
            //  joining
            //notification.OperationDetails = operations;
            return null;
        }

        public static void Create(Goal goal, ref IEnumerable<Notification> items, IEnumerable<TimeInterval> timeIntervals)
        {
            /*
             * 1.- Determinar cada intervalo
             * 2.- Para cada intervalo, crear items.Count (n) cantidad de notificaciones
             * 3.- ReleaseDate = Start + intervalo
             */

            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);
                item.ParentId = goal.ParentId;
                item.ParentTypeId = (int)goal.ParentTypeId;
                //item.UserId = 0;
            }

        }


    }
}
