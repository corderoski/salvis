using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Entities.Results
{
    public interface IResult
    {
        /// <summary>
        /// Resulted object. Can be Null.
        /// </summary>
        Object Result { get; }

        IEnumerable<String> Errors { get; }
        
    }
}
