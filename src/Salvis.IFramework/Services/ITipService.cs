using System.Collections.Generic;
using Salvis.Entities;

namespace Salvis.Framework.Services
{
    public interface ITipService : IServiceBase<Tip>
    {

        Tip Add(Tip item);

        /// <summary>
        /// Get an amoung of item specified by maxItem;
        /// </summary>
        /// <param name="maxItem"></param>
        /// <returns></returns>
        IEnumerable<Tip> GetRandom(int maxItem = 0);
    }
}
