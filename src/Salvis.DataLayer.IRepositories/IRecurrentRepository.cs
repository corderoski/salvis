
using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface IRecurrentRepository : IRepositoryBaseOperation<Recurrent>
    {

        Recurrent GetByCode(string code);

        /// <summary>
        /// Gets all the Debts related to an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A list of items matching an user.</returns>
        IEnumerable<Recurrent> GetByUserId(string userId);

        bool DeleteByCode(string code);

    }
}
