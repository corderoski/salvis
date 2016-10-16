
namespace  Salvis.App.Web.Models
{
    public class OperationNotificationModel : IModel
    {
        public int Interval { get; set; }
        public string Hour { get; set; }
        public bool Push { get; set; }
        public bool Sms { get; set; }
        public bool Email { get; set; }

        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}