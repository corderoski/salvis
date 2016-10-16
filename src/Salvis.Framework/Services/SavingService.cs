using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Salvis.Framework.Services
{
    public class SavingService : GoalService<Saving>, ISavingService
    {

        private readonly ISavingRepository _savingRepository;

        public SavingService(ISavingRepository savingRepository)
        {
            _savingRepository = savingRepository;
        }

        public override void Dispose()
        {
            _savingRepository.Dispose();
        }

        public override Saving Add(Saving item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");

            item.Code = base.CreateCode(item.Goal);
            _savingRepository.Add(item);

            //if (item.Goal.Notifications != null && item.Goal.Notifications.Any())
            //    _notificationService.Add(item.Goal);

            return item;
        }

        // Al guardar, debe crear un nuevo ID, ReasonTypeId es el motivo
        public override Saving Add(Saving item, TimeInterval timeInterval)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");

            item.Code = base.CreateCode(item.Goal);

            _savingRepository.Add(item);
            return item;
        }

        public override Saving GetByCode(string code)
        {
            return _savingRepository.GetByCode(code);
        }

        public override bool DeleteByCode(string code)
        {
            throw new NotImplementedException();
        }

        public override void Update(Saving item)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Saving item)
        {
            _savingRepository.Delete(item);
        }

        public override Saving Get(int id)
        {
            return _savingRepository.Get(id);
        }

        public override Saving Get(long id)
        {
            return _savingRepository.Get(id);
        }

        public override IEnumerable<Saving> Get()
        {
            return _savingRepository.Get();
        }

        public override IEnumerable<Saving> GetByUserId(string userId)
        {
            return _savingRepository.GetByUserId(userId);
        }

    }
}
