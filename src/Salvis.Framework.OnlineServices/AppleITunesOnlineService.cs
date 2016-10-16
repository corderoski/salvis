using System;
using System.Collections.Generic;
using System.Linq;
using Salvis.Framework.Net;
using Salvis.Framework.OnlineServices.Entities;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Salvis.Framework.Services;
using Salvis.Framework.Helpers;

namespace Salvis.Framework.OnlineServices
{

    public class AppleITunesOnlineService : OnlineService, IOnlineService
    {

        private readonly IConfigurationService _configurationService;
        public AppleITunesOnlineService()
        {
            _configurationService = new ConfigurationService();
        }

        public const string URL_BASE = "https://itunes.apple.com/search";

        [Obsolete("NotImplementedException. Workaround: ICatalogService", true)]
        public IEnumerable<object> GetContentTypes()
        {
            return null;
        }

        public IEnumerable<IProviderResult> GetContentByType(string typeId, string seekPattern)
        {
            if (String.IsNullOrEmpty(seekPattern)) throw new InvalidOperationException("Parameter seekPattern cannot be null or empty.");

            var parameters = new Dictionary<String, String>
                {
                    {"entity", typeId},
                    {"term", seekPattern}
                };
            var requestResult = Requester.MakeHttpRequest(URL_BASE, parameters);

            var items = new Collection<ProviderResult>();

            var json = JsonConvert.DeserializeObject<dynamic>(requestResult);

            var jsonArray = JsonConvert.DeserializeObject<dynamic[]>(json.results + "");

            foreach (var item in jsonArray)
            {
                var providerResult = new ProviderResult();
                switch (typeId)
                {
                    case "podcast":
                        providerResult.Description = ToShort(item.artistName.ToString());
                        providerResult.Id = item.trackId;
                        providerResult.Name = item.trackName;
                        providerResult.Reference = item.collectionViewUrl;
                        break;
                    case "ebook":
                        providerResult.Description = ToShort(item.description.ToString());
                        providerResult.Id = item.artistId;
                        providerResult.Name = item.artistName;
                        providerResult.Reference = item.trackViewUrl;
                        break;
                    case "audiobook":
                        providerResult.Description = ToShort(item.description.ToString());
                        providerResult.Id = item.artistId;
                        providerResult.Name = item.artistName;
                        providerResult.Reference = item.collectionViewUrl;
                        break;
                    case "tvShow":
                        providerResult.Description = String.Empty; //item.artistName;
                        providerResult.Id = item.artistId;
                        providerResult.Name = item.artistName;
                        providerResult.Reference = item.artistLinkUrl;
                        break;
                    default:
                        throw new NotImplementedException("typeId from Provider wasn't implemented yet.");
                }
                items.Add(providerResult);
            }

            if (!items.Any()) return new ProviderResult[0];

            var count = ConfigurationHelper.GetModuleSetting<int>(ONLINE_SERVICE_CONTENT_COUNT);
            return items.Take(count);
        }

        private string ToShort(string value)
        {
            return StringHelper.TruncateString(value, _configurationService.TruncatedTextMaxLength);
        }

    }

}
