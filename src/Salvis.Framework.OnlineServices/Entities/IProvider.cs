using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.OnlineServices.Entities
{
    interface IProvider
    {
        String Name { get; set; }
        List<String> Options { get; set; }
    }
}
