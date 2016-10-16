using System.IO;

namespace Salvis.Resources.Helpers
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

            using (StreamWriter writer = new StreamWriter(fullPath, overrider, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(content);
                writer.Flush();
            }
        }
    }
}
