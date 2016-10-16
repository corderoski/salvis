using System;
using System.Collections.Generic;
using System.Globalization;
using Salvis.Framework.Engine;
using System.Linq;
using Salvis.Resources.Helpers;

namespace Salvis.Resources.Services
{
    public class ResourceService : IResourceService
    {

        public void Dispose()
        {
            
        }

        public bool CreateResourceFile(CultureInfo culture)
        {
            try
            {
                return TextsEngine.CreateResourceFile(culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CreateResourceFile(CultureInfo cultureToCreate, CultureInfo cultureToCopy)
        {
            try
            {
                return TextsEngine.CreateResourceFile(cultureToCreate, cultureToCopy);
            }
            catch (Exception)
            {

                throw;
            } 
        }
        public IEnumerable<TextsResource> Get(CultureInfo culture)
        {
            try
            {
                return TextsEngine.GetTextsResources(culture);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Get(string key, CultureInfo culture)
        {
            try
            {
                return TextsEngine.GetMessege(key, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Get(string key)
        {
            try
            {
                return TextsEngine.GetMessege(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<string> GetFileNames()
        {
            try
            {
                var list = TextsEngine.ListFileName().ToList();
                list.ForEach(p =>
                {
                    p = p.Replace(TextsEngine.FileNameBase, "");
                });

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Dictionary<string, string> GetFileNamesWithCulture()
        {
            try
            {
                return TextsEngine.ListFileNameWithCulture();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Add(TextsResource textsResource, CultureInfo culture)
        {
            try
            {
                return TextsEngine.Add(textsResource, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TextsResource textsResource, CultureInfo culture)
        {
            try
            {
                return TextsEngine.Update(textsResource, culture);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}