using System;
using System.Linq;
using System.Web.Mvc;
using Salvis.Entities;
using Salvis.Framework.Services;
using Salvis.App.Web.Filters;
using Salvis.App.Web.Helpers;
using Salvis.App.Web.Models;

using Salvis.Resources;
using Resources;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Salvis.App.Web.Controllers
{

    /// <summary>
    /// Class managing the basis flow of Goals inherit models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Authorize]
    public abstract class GoalBaseController<T> : SalvisBaseController where T : class
    {

        private readonly IGoalService<T> _goalService;

        protected GoalBaseController(IGoalService<T> goalService)
        {
            _goalService = goalService;
        }

        [HttpPost]
        [SalvisAntiForgeryToken]
        public virtual ActionResult AddGoal(GoalModel model)
        {
            JsonResultData response;
            try
            {
                if (!model.IsValid())
                {
                    ModelState.AddModelError("", Texts.Error_InputInvalid);
                }
                else
                {
                    var result = _goalService.Validate(model.StartDate, model.EndDate,
                        0f, model.Amount, model.TimeType);

                    if (result.Errors.Any())
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item);
                        }
                    }
                    else
                    {
                        var entity = CreateEntity(model, result.Result as Goal);
                        entity = _goalService.Add(entity);
                        response = OnAfterCreation(entity);
                        return Json(response);
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", new ValidationUIException(ex.Message).Message);
            }
            LoadPartialAdd();
            return View("_AddPartial", model);
        }

        [HttpGet]
        public virtual ActionResult CallNotificationRow(int rownum)
        {
            var timeIntevals = EnumHelper.ToList(typeof(TimeInterval));
            ViewBag.Interval = new SelectList(timeIntevals, "Key", "Value");
            return PartialView("_GoalsNotificationFieldsPartial", rownum);
        }

        public abstract T CreateEntity(GoalModel model, Goal goal);

        /// <summary>
        ///  Delete action.
        ///  </summary><param name="parentId"></param>
        ///  <returns></returns>
        [HttpPost]
        [SalvisAntiForgeryToken]
        public virtual JsonResult DeleteGoal(String parentId)
        {
            JsonResultData response;
            try
            {
                var result = _goalService.DeleteByCode(parentId);
                response = new JsonResultData
                {
                    Code = HttpStatusCode.OK,
                    Message = new MessageBox
                    {
                        Message = Texts.Goal_SuccessDelete,
                        Title = Strings.AppName,
                        Type = MessageBoxType.success.ToString()
                    }
                };
            }
            catch (Exception)
            {
                response = new JsonResultData
                {
                    Code = HttpStatusCode.BadRequest,
                    Message = new MessageBox
                    {
                        Message = Texts.ErrorInternal,
                        Title = "Error",
                        Type = MessageBoxType.danger.ToString()
                    }
                };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Converts the Anonymous entity to a GoalModel.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract GoalModel EntityToModel(T entity);

        [HttpGet]
        public abstract JsonResult FillGoalsGraphic(string parentId);

        /// <summary>
        ///  Gets a valid list of goals for the logged user.
        ///  </summary>
        ///  <returns></returns>
        [HttpGet]
        public virtual ActionResult ListGoals()
        {
            var entities = _goalService.GetByUserId(User.Identity.GetUserId());
            if (entities == null || !entities.Any())
                entities = new T[0];
            return PartialView("_GoalsListPartial", entities.Select(i => EntityToModel(i)));
        }

        [HttpGet]
        public virtual ActionResult LoadPartialAdd()
        {
            var timeInterval = EnumHelper.ToList(typeof(TimeInterval));
            ViewBag.Interval = new SelectList(timeInterval, "Key", "Value");
            return PartialView("_AddPartial");
        }

        [HttpGet]
        public virtual ActionResult LoadDeleteConfirmation()
        {
            return PartialView("_GoalsDeleteConfirmation");
        }

        public abstract JsonResultData OnAfterCreation(T entity);

    }
}