using Microsoft.AspNet.Identity;
using Salvis.App.Web.Models;
using Salvis.Entities;
using Salvis.Entities.Notifications;
using Salvis.Framework.Services;
using Salvis.Resources;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salvis.App.Web.Controllers
{

    [Authorize]
    public class RecurrentController : GoalBaseController<Recurrent>
    {

        private readonly IRecurrentService _service;

        public RecurrentController(IRecurrentService recurrentService)
            : base(recurrentService)
        {
            _service = recurrentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var timeInterval = Helpers.EnumHelper.ToList(typeof(TimeInterval));
            ViewBag.Interval = new SelectList(timeInterval, "Key", "Value");
            return View();
        }

        #region Override functions

        public override GoalModel EntityToModel(Recurrent entity)
        {
            return new GoalModel
                {
                    Id = entity.Code,
                    Type = GoalEntityType.Recurrent,
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
            var item = _service.GetByCode(parentId);
            var goals = item.Goal;


            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public override JsonResultData OnAfterCreation(Recurrent entity)
        {
            MessageBox messageBox;
            if (entity.Id > 0 && entity.Goal.ParentId > 0)
            {
                messageBox = new MessageBox
                {
                    Message = Texts.Goal_RecurrentCreated,
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
                Data = Url.Action("Index", "Recurrent"),
                Message = messageBox
            };
        }

        public override Recurrent CreateEntity(GoalModel model, Goal goal)
        {
            var entity = new Recurrent()
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
