using System.Linq;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using System;
using System.Collections.Generic;
using Salvis.Framework.Engine;

namespace Salvis.Framework.Services
{
    public class DebtService : GoalService<Debt>, IDebtService
    {

        private readonly IDebtRepository _debtRepository;

        private readonly INotificationService _notificationService;

        public DebtService(IDebtRepository debtRepository,
                            INotificationService notificationService)
        {
            _debtRepository = debtRepository;
            _notificationService = notificationService;
        }

        public override void Dispose()
        {
            _notificationService.Dispose();
            _debtRepository.Dispose();
        }

        public override Debt Add(Debt item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");
            //if (item.Goal.OperationDetails == null) throw new ArgumentNullException("item", "Property item.Goal.Operation.OperationDetails can't be Null.");

            item.Code = base.CreateCode(item.Goal);
            _debtRepository.Add(item);

            //if (item.Goal.Notifications != null && item.Goal.Notifications.Any())
            //    _notificationService.Add(item.Goal);

            return item;
        }

        public override Debt Add(Debt item, TimeInterval timeInterval)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");

            //  this should be here, since its the BL...
            item.Goal.OperationDetails = GoalEngine.Create(item.Goal, timeInterval);

            return Add(item);
        }

        public override void Delete(Debt item)
        {
            _debtRepository.Delete(item);
        }

        public override void Update(Debt item)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteByCode(String code)
        {
            return _debtRepository.DeleteByCode(code);
        }

        public override Debt Get(int id)
        {
            return Get((long)id);
        }

        public override Debt Get(long id)
        {
            return _debtRepository.Get(id);
        }

        public override IEnumerable<Debt> Get()
        {
            return _debtRepository.Get();
        }

        public override Debt GetByCode(String code)
        {
            return _debtRepository.GetByCode(code);
        }

        public override IEnumerable<Debt> GetByUserId(string userId)
        {
            return _debtRepository.GetByUserId(userId);
        }

    }
}
