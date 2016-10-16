using Salvis.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.Services
{
    public class OperationService : IOperationService
    {

        private readonly IOperationRepository _operationRepository;

        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
