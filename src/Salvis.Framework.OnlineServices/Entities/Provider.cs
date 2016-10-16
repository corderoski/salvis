using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.OnlineServices.Entities
{
    public class Provider : IProvider
    {
        public String Name { get; set; }
        public List<String> Options { get; set; }
    }
}
