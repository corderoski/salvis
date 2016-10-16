using System.Linq;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using System;
using System.Collections.Generic;
using Salvis.Framework.Engine;

namespace Salvis.Framework.Services
{
    public class RecurrentService : GoalService<Recurrent>, IRecurrentService
    {

        private readonly IRecurrentRepository _repository;

        public RecurrentService(IRecurrentRepository recurrentRepository)
        {
            _repository = recurrentRepository;
        }

        public override void Dispose()
        {
            _repository.Dispose();
        }

        public override Recurrent Add(Recurrent item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");

            item.Code = base.CreateCode(item.Goal);
            _repository.Add(item);

            //if (item.Goal.Notifications != null && item.Goal.Notifications.Any())
            //    _notificationService.Add(item.Goal);

            return item;
        }

        public override Recurrent Add(Recurrent item, TimeInterval timeInterval)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Goal == null) throw new ArgumentNullException("item", "Property item.Goal can't be Null.");

            //  this should be here, since its the BL...
            item.Goal.OperationDetails = GoalEngine.Create(item.Goal, timeInterval);

            return Add(item);
        }

        public override void Delete(Recurrent item)
        {
            _repository.Delete(item);
        }

        public override void Update(Recurrent item)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteByCode(String code)
        {
            return _repository.DeleteByCode(code);
        }

        public override Recurrent Get(int id)
        {
            return Get((long)id);
        }

        public override Recurrent Get(long id)
        {
            return _repository.Get(id);
        }

        public override IEnumerable<Recurrent> Get()
        {
            return _repository.Get();
        }

        public override Recurrent GetByCode(String code)
        {
            return _repository.GetByCode(code);
        }

        public override IEnumerable<Recurrent> GetByUserId(string userId)
        {
            return _repository.GetByUserId(userId);
        }

        public IServiceResult Validate(DateTime startDate, DateTime? endDate, double amount, TimeInterval timeInterval)
        {
            if(!endDate.HasValue)
            endDate = startDate.AddYears(1);

            double? auxAmount = amount; //this can o cannot be used.
            var result = FieldValidations(startDate, endDate, 0f, ref auxAmount, timeInterval);

            if (result.Errors.Any())
                return new ServiceResult(null, result.Errors);

            //var resultEntity = result.Result as Goal;
            var entity = new Goal
            {
                StartDate = startDate,
                EndDate = endDate.Value,
                Amount = amount
            };
            entity.OperationDetails = GoalEngine.CreateRecurrent(startDate, endDate.Value, amount, timeInterval);

            return new ServiceResult(entity, result.Errors);
        }


    }
}
