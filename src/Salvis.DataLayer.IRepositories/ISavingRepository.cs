using Salvis.Entities;
using System.Collections.Generic;

namespace Salvis.DataLayer.Repositories
{
    public interface ISavingRepository : IRepositoryBaseOperation<Saving>
    {

        Saving GetByCode(string code);

        IEnumerable<Saving> GetByUserId(string userId);
    }
}
