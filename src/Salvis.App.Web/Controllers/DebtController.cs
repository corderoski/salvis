using System;
using System.Linq;
using Salvis.Resources;
using System.Web.Mvc;
using Salvis.Entities;
using Salvis.Entities.Notifications;
using Salvis.Framework.Services;
using Salvis.App.Web.Models;

using System.Net;
using Microsoft.AspNet.Identity;

namespace Salvis.App.Web.Controllers
{

    [Authorize]
    public class DebtController : GoalBaseController<Debt>
    {

        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
            : base(debtService)
        {
            _debtService = debtService;
        }

        //
        // GET: /Debts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var timeInterval = Salvis.App.Web.Helpers.EnumHelper.ToList(typeof(TimeInterval));
            ViewBag.Interval = new SelectList(timeInterval, "Key", "Value");
            return View();
        }

        #region Override functions

        public override GoalModel EntityToModel(Debt entity)
        {
            return new GoalModel
                {
                Id = entity.Code,
                Type = GoalEntityType.Debt,
                Amount = entity.Goal.Amount,
                Name = entity.Goal.Name,
                Description = entity.Goal.Description,
                StartDate = entity.Goal.StartDate,
                EndDate = entity.Goal.EndDate
            };
        }

        [HttpGet]
        public override JsonResult FillGoalsGraphic(string parentId)
        {
            var item = _debtService.GetByCode(parentId);
            var goals = item.Goal;

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public override JsonResultData OnAfterCreation(Debt entity)
        {
            MessageBox messageBox;
            if (entity.Id > 0 && entity.Goal.ParentId > 0)
            {
                messageBox = new MessageBox
                {
                    Message = Texts.Goal_DebtCreated,
                    Type = MessageBoxType.success.ToString()
                };
            }
            else
            {
                messageBox = new MessageBox
                {
                    Message = Texts.ErrorInternal,
                    Type = MessageBoxType.danger.ToString()
                };
            }
            return new JsonResultData
            {
                Code = HttpStatusCode.PartialContent,
                Data = Url.Action("Index", "Debt"),
                Message = messageBox
            };
        }

        public override Debt CreateEntity(GoalModel model, Goal goal)
        {
            var entity = new Debt()
            {
                ReasonTypeId = model.ReasonId,
                UserId = User.Identity.GetUserId(),
                Goal = goal,
            };
            entity.Goal.Name = model.Name;
            if (String.IsNullOrEmpty(model.Description))
                model.Description = String.Empty;
            entity.Goal.Description = model.Description;
            if (model.Notifications != null && model.Notifications.Any())
            {
                var notifications = model.Notifications.Select(i =>
                                    new Notification
                                    {
                                        IsEmailEnabled = i.Email,
                                        IsSmsEnabled = i.Sms,
                                        ReleaseDate = Convert.ToDateTime(i.Hour),
                                        UserId = User.Identity.GetUserId()
                                    });
                entity.Goal.Notifications = notifications;
            }
            return entity;
        }

        #endregion
      
    }
}
