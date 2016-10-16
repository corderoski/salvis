using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.OnlineServices.Entities
{
    internal class ProviderResult : IProviderResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Uri Reference { get; set; }

    }
}
