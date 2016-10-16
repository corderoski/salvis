namespace Salvis.App.Web.Models
{
    public class MessageBox
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }

    public enum MessageBoxType
    {
        success,
        info,
        warning,
        danger
    }
}