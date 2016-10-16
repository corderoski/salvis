using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Salvis.Entities;
using Salvis.Entities.Notifications;

namespace Salvis.DataLayer.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {

        private readonly String _sqlDelete = String.Format("DELETE FROM dbo.[{0}] WHERE GoalId = @parentId AND GoalTypeId = @parentTypeId",
                                                           typeof(Notification).Name);

        private readonly String _sqlInsertNotifBoard =
            String.Format(
                "INSERT INTO dbo.[{0}] (StartDate, EndDate, Duration, ItemsCount) values (@StartDate, @EndDate, @Duration, @ItemsCount);",
                typeof(NotificationBoard).Name);

        public NotificationRepository(IDbConnection connection)
            : base(connection)
        {
        }

        #region Notification

        public new Notification Add(Notification item)
        {
            Connection.Insert(item);
            return item;
        }

        public new IEnumerable<Notification> Add(IEnumerable<Notification> items)
        {
            foreach (var notification in items)
            {
                Connection.Insert(notification);
            }
            return items;
        }

        new public void Delete(Notification notification)
        {
            var result = Connection.Execute(_sqlDelete, new { notification.ParentId, notification.ParentTypeId });
        }

        new public void Delete(IEnumerable<Notification> items)
        {
            foreach (var notification in items)
                Delete(notification);
        }

        public IEnumerable<Notification> GetByTimeIntervals(DateTime init, DateTime final)
        {
            var sqlSelect = String.Format("SELECT * FROM dbo.[{0}] WHERE ReleaseDate BETWEEN @init AND @final", typeof(Notification).Name);
            return Connection.Query<Notification>(sqlSelect, new { init, final });
        }

        #endregion

        #region NotificationBoard

        public NotificationBoard GetLastExecution()
        {
            const string sqlLastItem = "SELECT MAX([Id]) FROM [dbo].[NotificationBoard]";
            var maxId = Connection.ExecuteScalar<int>(sqlLastItem);

            var sqlSelect = String.Format("SELECT * FROM dbo.[{0}] WHERE Id = @id", typeof(NotificationBoard).Name);
            var item = Connection.Query<NotificationBoard>(sqlSelect, new { id = maxId }).SingleOrDefault() ??
                       GetNewExecution(null);

            return item;
        }

        public NotificationBoard GetNewExecution(DateTime? startDate)
        {
            var run = new NotificationBoard();
            run.StartDate = startDate.HasValue ? startDate.Value : DateTimeOffset.Now.DateTime;
            //  Disable previous results, insert, look for new ID
            var id = Connection.ExecuteScalar<long>("SET NOCOUNT ON; " + _sqlInsertNotifBoard + " SELECT @@IDENTITY id",
                 new { run.StartDate, run.EndDate, run.Duration, run.ItemsCount });
            run.Id = id;
            return run;
        }

        public bool CloseExecution(NotificationBoard item)
        {
            item.EndDate = DateTimeOffset.Now.DateTime;
            return Connection.Update(item) > 0;
        }

        #endregion

    }
}
