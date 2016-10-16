using Salvis.Entities;
using Salvis.Entities.Notifications;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;
using Salvis.Resources;
using Salvis.App.Web.Filters;
using Salvis.App.Web.Helpers;
using Salvis.App.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Salvis.App.Web.Controllers
{
    [Authorize]
    [SalvisAntiForgeryToken]
    public class SavingController : GoalBaseController<Saving>
    {

        private readonly ISavingService _savingService;

        //  Unused, for now.
        //private readonly INotificationService _notificationService;

        public SavingController(ISavingService savingService)
            : base(savingService)
        {
            _savingService = savingService;
        }

        //
        // GET: /Savings/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var timeInterval = EnumHelper.ToList(typeof(TimeInterval));
            ViewBag.Interval = new SelectList(timeInterval, "Key", "Value");
            return View();
        }

        #region Override & Partial function

        public override GoalModel EntityToModel(Saving entity)
        {
            return new GoalModel()
            {
                Id = entity.Code,
                Type = GoalEntityType.Saving,
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
            var saving = _savingService.GetByCode(parentId);
            var goals = saving.Goal;
            
            var line1 = _savingService.BuilderGraphic(goals, true);
            var line2 = _savingService.BuilderGraphic(goals);
            var graphic = new[]
            {
                new Line {data = line1.Line, label = "Real"},
                new Line {data = line2.Line, label = "Exp"}
            };
            var operationDetails =
                goals.OperationDetails.Select(
                    p =>
                    new[]
                        {
                            p.InputDate.ToString(FormatHelper.DATE_FORMAT),
                            FormatHelper.GetCurrency(p.ExpValue),
                            (p.RealValue.HasValue) ? FormatHelper.GetCurrency(p.RealValue.Value) : string.Empty
                        }).ToList();

            var graphicData = new GraphicData
            {
                Line = graphic,
                YaxisSub = line1.YaxisSub,
                OperationDetails = operationDetails,
                OperationExpectedNextDate = line2.NextOperation.NextDate.ToString(FormatHelper.DATE_FORMAT),
                OperationExpectedRealAmount = FormatHelper.GetCurrency(line2.NextOperation.RealAmount),
                OperationExpectedExpAmount = FormatHelper.GetCurrency(line2.NextOperation.ExpAmount)
            };
            return Json(graphicData, JsonRequestBehavior.AllowGet);
        }

        public override JsonResultData OnAfterCreation(Saving entity)
        {
            if (entity.Id > 0 && entity.Goal.ParentId > 0)
            {
                return new JsonResultData
                {
                    Code = HttpStatusCode.PartialContent,
                    Data = Url.Action("Index", "Saving"),
                    Message = new MessageBox
                    {
                        Message = Texts.Goal_SavingCreated,
                        Type = MessageBoxType.success.ToString()
                    }
                };
            }
            // if reached, something failed
            return new JsonResultData
            {
                Code = HttpStatusCode.InternalServerError,
                Message = new MessageBox
                {
                    Message = Texts.ErrorInternal,
                    Type = MessageBoxType.danger.ToString()
                }
            };
        }

        public override Saving CreateEntity(GoalModel model, Goal goal)
        {
            goal.Name = model.Name;
            goal.Description = String.IsNullOrEmpty(model.Description) ? string.Empty : model.Description;
            var entity = new Saving()
            {
                ReasonTypeId = model.ReasonId,
                UserId = User.Identity.GetUserId(),
                Goal = goal
            };
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