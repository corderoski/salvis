using System;
using System.Linq;
using System.Web.Mvc;
using Salvis.Resources;
using Salvis.Entities;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;
using Salvis.App.Web.Filters;
using Salvis.App.Web.Models;
using WebGrease.Css.Extensions;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Salvis.App.Web.Controllers
{

    [Authorize]
    public class MessageController : SalvisBaseController
    {

        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
            : base(LayoutStyleEnum.MailLayout)
        {
            _messageService = messageService;
            _userService = userService;
        }

        //
        // GET: /Mail/
        public ActionResult Index(int page = 1)
        {
            if (page == 0) page = 1;

            var toSkip = (page - 1) * MAX_MESSAGES_SHOWING;


            var messageSkiped = _messageService.GetByUserId(User.Identity.GetUserId())
                                               .OrderByDescending(p => p.InputDate)
                                               .Skip(toSkip).ToList();

            ViewBag.NextPage = (messageSkiped.Count > MAX_MESSAGES_SHOWING) ? page + 1 : 0;
            ViewBag.BeforePage = page > 1 ? page - 1 : 0;

            var message = messageSkiped.Take(MAX_MESSAGES_SHOWING);
            var messagesModel = message.Select(p =>
                {
                    var model = new MessageModel(p);
                    if (!string.IsNullOrEmpty( model.FromUserId))
                    {
                        model.FromUserName = _userService.GetName(model.FromUserId);
                    }
                    return model;
                });
            return View(messagesModel);
        }

        public ActionResult Details(string id)
        {
            var message = _messageService.Get(0);
            var model = new MessageModel(message);

            if (!model.UserId.Equals(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }
            if (message.State == (int)MessageState.Unread)
            {
                MarkAsRead(message);
            }
            if (!string.IsNullOrEmpty(model.FromUserId))
            {
                model.FromUserName = _userService.GetName(model.FromUserId);
            }
            return View(model);
        }
        
        public ActionResult Compose(long id, int composeState)
        {
            var message = _messageService.Get(id);
            var model = new MessageModel(message);

            if (!model.UserId.Equals(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(model.FromUserId))
            {
                model.FromUserName = _userService.GetName(model.FromUserId);
            }
            else
            {
                return RedirectToAction("Details", new { id });
            }
            model.Content = string.Empty;
            model.Subject = StringHelper.SetPreFixMessageSubject(model.Subject, ((MessageComposeType)composeState).ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteAll(long[] ids)
        {
            var result = ids.Select(DeleteSingle).ToList();
            var error = result.FirstOrDefault(p => p.Code == HttpStatusCode.BadRequest);
            if (error != null)
            {
                return Json((object)error);
            }

            var reponse = result.FirstOrDefault(p=>p.Code == HttpStatusCode.PartialContent);
            return Json((object)reponse);
        }
        
        [HttpPost]
        [SalvisAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            return Json((object)DeleteSingle(id));
        }

        [HttpPost]
        [SalvisAntiForgeryToken]
        public ActionResult SendCompose(Message message)
        {
            message.InputDate = DateTime.Now;
            message.UserId = message.FromUserId;
            message.FromUserId = User.Identity.GetUserId();
            message.ParentId = message.Id;
            JsonResultData response;
            try
            {
                _messageService.Add(message);
                response = new JsonResultData
                {
                    Code = HttpStatusCode.PartialContent,
                    Message = new MessageBox()
                    {
                        Message = Texts.Mailing_Sent_Success,
                        Type = MessageBoxType.success.ToString()
                    },
                    Data = Url.Action("Index")
                };
            }
            catch (Exception)
            {
                response = new JsonResultData
                {
                    Code = HttpStatusCode.BadRequest,
                    Message = new MessageBox()
                    {
                        Message = Texts.ErrorInternal,
                        Type = MessageBoxType.danger.ToString()
                    },
                    Data = Url.Action("Index")
                };
            }
            return Json((object)response);

        }

        #region Private functions

        private void MarkAsRead(Message message)
        {
            _messageService.MarkAsReadAsync(message.Id);
        }


        private JsonResultData DeleteSingle(long id)
        {
            var message = _messageService.Get(id);
            JsonResultData reponse;
            if (!message.UserId.Equals(User.Identity.GetUserId()))
            {
                reponse = new JsonResultData()
                {
                    Code = HttpStatusCode.BadRequest,
                    Data = Url.Action("Index"),
                    Message = new MessageBox()
                    {
                        Message = "Error de validación.",
                        Type = MessageBoxType.danger.ToString()
                    },
                };
                return reponse;
            }
            _messageService.Delete(message);
            reponse = new JsonResultData()
            {
                Code = HttpStatusCode.PartialContent,
                Data = Url.Action("Index"),
                Message = new MessageBox()
                {
                    Message = "Has eliminado el mensaje.",
                    Type = MessageBoxType.success.ToString()
                }
            };
            return reponse;
        }

        #endregion

        public const int MAX_UNREAD_MESSAGES_SHOWING = 5;
        public const int MAX_MESSAGES_SHOWING = 6;
    }
}
