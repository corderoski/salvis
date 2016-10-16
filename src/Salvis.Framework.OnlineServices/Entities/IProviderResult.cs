using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.OnlineServices.Entities
{
    public interface IProviderResult
    {

        String Id { get; set; }

        String Name { get; set; }

        String Description { get; set; }

        Uri Reference { get; set; }

    }
}
