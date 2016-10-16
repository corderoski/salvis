using System;
using System.Web.Mvc;
using Salvis.Framework.Helpers;
using Salvis.App.Web.Models;
using Salvis.App.Web.Filters;
using Salvis.Resources;
using System.Net;

namespace Salvis.App.Web.Controllers
{
    [AllowAnonymous]
    public class AppsController : SalvisBaseController
    {


        public AppsController()
        {

        }
 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Asesor()
        {
            return View();
        }

         
        #region MonthlyViewer

        public ActionResult MonthlyViewer()
        {
            return View();
        }


        [HttpGet]
        public virtual ActionResult MonthlyViewerRow(int rownum)
        {
            return PartialView("_MonthlyViewerItem", rownum);
        }

        [HttpPost]
        [SalvisAntiForgeryToken]
        public virtual ActionResult AddMonthlyReport(MonthlyReportModel model)
        {
            JsonResultData response = null;
            try
            {
                if (!model.IsValid())
                {
                    response = new JsonResultData
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = new MessageBox
                        {
                            Message = Texts.Error_InputInvalid,
                            Title = Texts.ErrorInValidation,
                            Type = MessageBoxType.danger.ToString()
                        }
                    };
                    return Json((object)response);
                }

                //var result = _goalService.Validate(model.StartDate, model.EndDate,
                //    0f, model.Amount, model.TimeType);

                //if (result.Errors.Any())
                //{
                //    response = new JsonResultData
                //    {
                //        Code = JsonResultDataCode.Error,
                //        Messages = new MessageBox
                //        {
                //            Messages = String.Join(" ", result.Errors),
                //            Title = "Error",
                //            Type = MessageBoxType.danger.ToString()
                //        }
                //    };
                //}
                //else
                //{
                //    response = null;
                //}
            }
            catch (Exception ex)
            {
                response = new JsonResultData
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = new ValidationUIException(ex.Message).MessageBox
                };
            }
            return Json((object)response);
        }

        #endregion

         #region Planners

        public ActionResult PlannerBudget()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResultData PlannerBudget(FormCollection formCollection)
        {
            float income = 0f;
            float outcome = 0f;
            float saving = 0f;
            float debts = 0f;

            //  step1
            {
                float income1 = 0f;
                float income2 = 0f;

                if (!String.IsNullOrEmpty(formCollection["s1_txtIn1"]))
                    income1 = Convert.ToSingle(formCollection["s1_txtIn1"]);

                if (!String.IsNullOrEmpty(formCollection["s1_txtIn2"]))
                    income2 = Convert.ToSingle(formCollection["s1_txtIn2"]);

                income = income1 + income2;
            }

            //  step2
            {
                float outcome1 = 0f, outcome2 = 0f, outcome3 = 0f, outcome4 = 0f;
                if (!String.IsNullOrEmpty(formCollection["s2_txtOut1"]))
                    outcome1 = Convert.ToSingle(formCollection["s2_txtOut1"]);
                if (!String.IsNullOrEmpty(formCollection["s2_txtOut2"]))
                    outcome2 = Convert.ToSingle(formCollection["s2_txtOut2"]);
                if (!String.IsNullOrEmpty(formCollection["s2_txtOut3"]))
                    outcome3 = Convert.ToSingle(formCollection["s2_txtOut3"]);
                if (!String.IsNullOrEmpty(formCollection["s2_txtOut4"]))
                    outcome4 = Convert.ToSingle(formCollection["s2_txtOut4"]);

                outcome = outcome1 + outcome2 + outcome3 + outcome4;
            }

            //  step3
            {
                float sav1 = 0f, sav2 = 0f;
                if (!String.IsNullOrEmpty(formCollection["s3_sav1"]))
                    sav1 = Convert.ToSingle(formCollection["s3_sav1"]);
                if (!String.IsNullOrEmpty(formCollection["s3_sav2"]))
                    sav2 = Convert.ToSingle(formCollection["s3_sav2"]);

                saving = sav1 + sav2;
            }

            //  step4
            {
                float debt1 = 0f, debt2 = 0f, debt3 = 0f;

                if (!String.IsNullOrEmpty(formCollection["s4_debt1"]))
                    debt1 = Convert.ToSingle(formCollection["s4_debt1"]);
                if (!String.IsNullOrEmpty(formCollection["s4_debt2"]))
                    debt2 = Convert.ToSingle(formCollection["s4_debt2"]);
                if (!String.IsNullOrEmpty(formCollection["s4_debt3"]))
                    debt3 = Convert.ToSingle(formCollection["s4_debt3"]);

                debts = debt1 + debt2 + debt3;
            }

            //indicates that budget was prepared.
            ViewBag.Budget = true;

            ViewBag.realIncome = FormatHelper.GetCurrency(income);

            ViewBag.expOutcome = FormatHelper.GetCurrency(income * 0.60f) + " (60%)";
            ViewBag.realOutcome = FormatHelper.GetCurrency(outcome + debts) + " (" + Math.Round((outcome * 100) / income, 1) + "%)";

            ViewBag.expSaving = FormatHelper.GetCurrency(income * 0.30f) + " (30%)";
            ViewBag.realSaving = FormatHelper.GetCurrency(saving) + " (" + Math.Round((saving * 100) / income, 1) + "%)";


            var personal = 0f;
            if (!String.IsNullOrEmpty(formCollection["s2_txtOut4"]))
                personal = Convert.ToSingle(formCollection["s2_txtOut4"]);
            ViewBag.expPersonal = FormatHelper.GetCurrency(income * 0.10f) + " (10%)";
            ViewBag.realPersonal = FormatHelper.GetCurrency(personal) + " (" + Math.Round((personal * 100) / income, 1) + "%)";
  
            var response = new JsonResultData
            {
                Code = HttpStatusCode.Redirect,
                Data = Url.Action("PlannerResult")
            };
            return response;
        }

        public ActionResult PlannerResult()
        {
            /*
            var docName = Guid.NewGuid().ToString() + ".png";
            PrintPage(docName);

            var fileName = String.Format("{0}{1}", ImgPath, docName);
            var stream = new FileStream(Server.MapPath(fileName), FileMode.Open);
            var fileResult = File(stream, "image/png", docName);
            //TODO: add full document
            return fileResult;
            */
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlannerResult(FormCollection formCollection)
        {
            var path = System.IO.Path.Combine(@"~/Images", "document.pdf");

            return File(path, "application/pdf");
        }

        #endregion

        public ActionResult SmartAssistant()
        {
            return View();
        }



    }
}
