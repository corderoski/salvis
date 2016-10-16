using Salvis.Framework.Services;
using System.Collections.Generic;
using System.Globalization;


namespace Salvis.Resources.Services
{
    public interface IResourceService : IService
    {
        bool CreateResourceFile(CultureInfo cultureToCreate, CultureInfo cultureToCopy);
        bool CreateResourceFile(CultureInfo culture);
        IEnumerable<TextsResource> Get(CultureInfo culture);
        string Get(string key, CultureInfo culture);
        string Get(string key);
        IEnumerable<string> GetFileNames();
        Dictionary<string, string> GetFileNamesWithCulture();
        bool Add(TextsResource textsResource, CultureInfo culture);
        bool Update(TextsResource textsResource, CultureInfo culture);
    }
}