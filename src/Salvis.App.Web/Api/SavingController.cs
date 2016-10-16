using Salvis.Framework.Services;
using System.Web.Http;

namespace Salvis.App.Web.Api
{
    [Authorize]
    public class SavingController : ApiController
    {

        private readonly ISavingService _savingService;


        public SavingController(ISavingService savingService)
        {
            _savingService = savingService;
        }


        [Route("api/saving/{id}/")]
        public IHttpActionResult Delete(string id)
        {
            return _savingService.DeleteByCode(id) ? Ok() : (IHttpActionResult)BadRequest();
        }
    }
}
