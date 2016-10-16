using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Salvis.Framework.Helpers;
using Salvis.Framework.IO;
using Salvis.Framework.Engine;

namespace Salvis.Resources.Helpers
{
    internal class TextsEngine : IEngine
    {
        internal const string FileNameBase = "Texts_";
        private const string PathBase = @"C:\Users\martin.acosta\Documents";

        /// <summary>
        /// Get the message based on key and culture.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal static string GetMessege(string key, CultureInfo culture)
        {
            var item = GetTextsResource(key, culture);
            return item.Message;
        }

        /// <summary>
        /// Get the message based on key and current culture.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value correpond of key.</returns>
        internal static string GetMessege(string key)
        {
            return GetMessege(key, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        ///Get list of file name of all resource file that exists.
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<string> ListFileName()
        {
            var files = Directory.EnumerateFiles(PathBase).ToList();
            var fileNames = files.Select(Path.GetFileName);
            return fileNames.Where(p => p.StartsWith(FileNameBase));
        }

        /// <summary>
        /// Get all file name with her cultura of all resource file that exists.
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, string> ListFileNameWithCulture()
        {
            var listResource = ListFileName();
            var dictionary = new Dictionary<string, string>();
            foreach (var item in listResource)
            {
                var culture = item.Substring(item.IndexOf('_') + 1, 5);
                dictionary.Add(item, culture);
            }
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textsResource"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal static bool Add(TextsResource textsResource, CultureInfo culture)
        {
            var textsResources = GetTextsResources(culture);
            textsResources.Add(textsResource);
            var textsSerialized = JsonHelper.Serializer(textsResource);
            var fileIo = new FileOperations();

            var fileNameLang = FileNameLang(culture);
            fileIo.Save(textsSerialized, PathBase, fileNameLang, true);
            return true;
        }

        internal static bool Update(TextsResource textsResource, CultureInfo culture)
        {
            var list = GetTextsResources(culture);
            var toRemove = GetTextsResource(textsResource.Key, culture);
            list.Remove(toRemove);
            list.Add(textsResource);

            var textsSerialized = JsonHelper.Serializer(list);
            var fileIo = new FileOperations();

            var fileNameLang = FileNameLang(culture);
            fileIo.Save(textsSerialized, PathBase, fileNameLang, true);
            return false;
        }

        internal static bool CreateResourceFile(CultureInfo culture)
        {
            var io = new FileOperations();
            var fileNameLang = FileNameLang(culture);
            var list = ListFileNameWithCulture();
            if (
                list.Any(
                    item =>
                    item.Key.Equals(culture.Name, StringComparison.InvariantCultureIgnoreCase)))
                return false;

            io.Save(DefaultResource(), PathBase, fileNameLang);
            return true;
        }
        /// <summary>
        /// Create a resource 
        /// </summary>
        /// <param name="cultureToCreate"></param>
        /// <param name="cultureToCopy"></param>
        /// <returns></returns>
        internal static bool CreateResourceFile(CultureInfo cultureToCreate, CultureInfo cultureToCopy)
        {
            var io = new FileOperations();
            var fileNameLang = FileNameLang(cultureToCreate);
            var list = ListFileNameWithCulture();
            if (
                list.Any(
                    item =>
                    item.Key.Equals(cultureToCreate.Name, StringComparison.InvariantCultureIgnoreCase)))
                return false;

            var content = ResourceContent(cultureToCopy);
            io.Save(content, PathBase, fileNameLang);
            return true;
        }

        #region -Helper
        /// <summary>
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private static string FileNameLang(CultureInfo culture)
        {
            var language = culture.Name;
            return string.Format("{0}{1}", FileNameBase, language);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private static string ResourceContent(CultureInfo culture)
        {
            var fileNameLang = FileNameLang(culture);
            var fileio = new FileOperations();
            return fileio.OpenFile(PathBase, fileNameLang);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal static IList<TextsResource> GetTextsResources(CultureInfo culture)
        {
            var textsResources = DeserializerFile(culture);
            return textsResources;
        }
        private static TextsResource GetTextsResource(string key, CultureInfo culture)
        {
            var textsResources = DeserializerFile(culture);
            return textsResources.FirstOrDefault(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        private static IList<TextsResource> DeserializerFile(CultureInfo culture)
        {
            return JsonHelper.Deserializer<List<TextsResource>>(ResourceContent(culture));
        }

        private static string DefaultResource()
        {
            var obj = new TextsResource() { Key = "nullo", Message = "nullo" };
            return JsonHelper.Serializer(new List<TextsResource> { obj });
        }


        #endregion


    }
}