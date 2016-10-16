using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface IDebtRepository : IRepositoryBaseOperation<Debt>
    {

        Debt GetByCode(string code);

        /// <summary>
        /// Gets all the Debts related to an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A list of items matching an user.</returns>
        IEnumerable<Debt> GetByUserId(string userId);

        bool DeleteByCode(string code);

    }
}
