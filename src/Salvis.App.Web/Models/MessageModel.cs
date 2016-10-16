using Salvis.Entities;

namespace  Salvis.App.Web.Models
{

    public class MessageModel : Message
    {

        public MessageModel(Message message)
        {
            Id = message.Id;
            InputDate = message.InputDate;
            ParentId = message.ParentId;
            ParentTypeId = message.ParentTypeId;
            Content = message.Content;
            FromUserId = message.FromUserId;
            State = message.State;
            Subject = message.Subject;
            TypeId = message.TypeId;
            UserId = message.UserId;
        }
        public string FromUserName { get; internal set; }
    }
}