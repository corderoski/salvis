using Microsoft.AspNet.Identity;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;
using System.Linq;
using System.Web.Http;

namespace Salvis.App.Web.Api
{
    [Authorize]
    public class MessagesController : ApiController
    {

        private readonly IMessageService _messageService;
        private readonly IConfigurationService _configurationService;

        public MessagesController(IMessageService messageService, IConfigurationService configurationService)
        {
            _messageService = messageService;
            _configurationService = configurationService;
        }

        [HttpGet]
        public IHttpActionResult GetNotifications()
        {
            var unread = _messageService.GetUnreadByUserId(User.Identity.GetUserId());

            var count = unread.Count();
            var messages = unread.Take(_configurationService.MessagesMaxUnreadShowing).ToList();
            messages.ForEach(p => p.Content = StringHelper.TruncateString(p.Content, _configurationService.TruncatedTextMaxLength));

            var data = new
            {
                count = count,
                messages = messages
            };
            return Ok(data);
        }

    }
}
