using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Salvis.Entities;
using Salvis.Framework.OnlineServices;
using Salvis.Framework.OnlineServices.Entities;
using Salvis.Framework.OnlineServices.Factory;
using Salvis.Framework;
using Salvis.Framework.Services;

namespace Salvis.App.Web.Controllers
{

    [AllowAnonymous]
    public class LibraryController : SalvisBaseController
    {

        private readonly IOnlineServiceFactory _onlineServiceFactory;

        private readonly IOnlineService _itunesServicesOnlineService;

        private readonly ITipService _tipService;

        public LibraryController(IOnlineServiceFactory factory, ITipService tipService)
        {
            _tipService = tipService;
            _onlineServiceFactory = factory;
            _itunesServicesOnlineService = _onlineServiceFactory.Create(OnlineProvider.iTunes);
        }


        //
        // GET: /Library/

        public ActionResult Index(string options = null, string term = null)
        {
            IEnumerable<IProviderResult> result = new List<IProviderResult>();

            if (!string.IsNullOrEmpty(options) && !string.IsNullOrEmpty(term))
                result = _itunesServicesOnlineService.GetContentByType(options, term);

            return View(result);
        }

        public ActionResult LoadNewTip()
        {
            //TODO: Algoritmo para sacar un tip diferente al que ya se ha mostrado...

            var test = Guid.NewGuid().ToString().Substring(0, 10);
            var list = new List<Tip>
            {
                new Tip()
                {
                    DescriptionEN = test,
                    DescriptionES = test,
                    Enable = true,
                    Id = 1,
                    Type = TipTypeEnum.Good
                }
            };
              return PartialView("_TipPartial",list); 
        }

        public ActionResult LoadPartialTip(int max)
        {
            //TODO: Cuando funke el service remove el comentado debajo XD.
            //var list = _tipService.GetRandom(max);
            
            var list = new List<Tip>
                {
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 1,
                            Type = TipTypeEnum.Good
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 2,
                            Type = TipTypeEnum.Good
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 3,
                            Type = TipTypeEnum.Danger
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 4,
                            Type = TipTypeEnum.Danger
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 5,
                            Type = TipTypeEnum.Remember
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 6,
                            Type = TipTypeEnum.Remember
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 7,
                            Type = TipTypeEnum.Warning
                        },new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 8,
                            Type = TipTypeEnum.Warning
                        },new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 9,
                            Type = TipTypeEnum.Warning
                        },new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id = 10,
                            Type = TipTypeEnum.Warning
                        },
                    new Tip()
                        {
                            DescriptionEN = "asdf",
                            DescriptionES = "asdf",
                            Enable = true,
                            Id =11,
                            Type = TipTypeEnum.Warning
                        }
                };


            return PartialView("_TipPartial",list);
        }

        public ActionResult Tips()
        {
            return View();
        }
    }
}
