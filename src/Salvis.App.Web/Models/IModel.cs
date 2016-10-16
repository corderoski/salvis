using System.Collections.Generic;

namespace  Salvis.App.Web.Models
{
    public interface IModel
    {
        bool IsValid();
        //IEnumerable<string> Errors { get; set; }
    }
}
