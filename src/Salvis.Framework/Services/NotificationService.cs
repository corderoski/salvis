using System;
using System.Collections.Generic;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using Salvis.Entities.Notifications;
using Salvis.Framework.Engine;
using Salvis.Framework.Helpers;

namespace Salvis.Framework.Services
{

    public class NotificationService : INotificationService, INotificationBoardService
    {

        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        #region Notification

        public Notification Add(Notification notification)
        {
            return _notificationRepository.Add(notification);
        }

        public IEnumerable<Notification> Add(Goal goal, IEnumerable<Notification> items, IEnumerable<TimeInterval> timeIntervals)
        {
            NotificationEngine.Create(goal, ref items, timeIntervals);

            return _notificationRepository.Add(items);
        }

        public void Delete(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public void Delete(IEnumerable<Notification> items)
        {
            _notificationRepository.Delete(items);
        }

        public IEnumerable<Notification> GetByTimeIntervals(DateTime init, DateTime final)
        {
            //init = DateHelper.CleanHour(init);
            //final = DateHelper.GetNextHour(final);
            return _notificationRepository.GetByTimeIntervals(init, final);
        }

        public IEnumerable<Notification> GetByDate(DateTime date)
        {
            var init = new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);
            var final = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            return _notificationRepository.GetByTimeIntervals(init, final);
        }

        #endregion

        #region NotificationBoard

        public NotificationBoard GetLastExecution()
        {
            return _notificationRepository.GetLastExecution();
        }

        public NotificationBoard GetNewExecution(DateTime? startDate)
        {
            return _notificationRepository.GetNewExecution(startDate);
        }

        public bool CloseExecution(NotificationBoard item)
        {
            return _notificationRepository.CloseExecution(item);
        }

        #endregion

        public void Dispose()
        {
            _notificationRepository.Dispose();
        }

    }
}
