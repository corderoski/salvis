using System.IO;
using Salvis.Framework.Helpers;

namespace Salvis.Framework.IO
{
    internal class FileOperations
    {
        public string OpenFile(string path, string fileName)
        {
            var fullPath = Path.Combine(path, fileName);
            using (StreamReader reader = new StreamReader(fullPath))
            {
                return reader.ReadToEnd();
            }
        }

        public void Save(string content, string path, string fileName, bool overrider = false)
        {
            var fullPath = Path.Combine(path, fileName);

            using (StreamWriter writer = new StreamWriter(fullPath, overrider, ConfigurationHelper.DefaultEncoding))
            {
                writer.WriteLine(content);
                writer.Flush();
            }
        }
    }
}
