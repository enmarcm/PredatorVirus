using System.Linq;
using System.Configuration;
using System.IO;
using System.IO.Compression;

namespace Predator
{
    internal class FilesManager
    {
        private readonly string directoryName = ConfigurationManager.AppSettings["directoryName"].ToString();
        public string SearchDirectory() {
            string directoryPath = Directory.GetDirectories(@"C:\", this.directoryName, SearchOption.AllDirectories).FirstOrDefault();
            return directoryPath;
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }


        public string SearchFile(string fileName)
        {
            string filePath = Directory.GetFiles(@"C:\", fileName, SearchOption.AllDirectories).FirstOrDefault();
            return filePath;
        }

        public string CopyToZip(string directoryPath)
        {
            string tempZipPath = null;

            if (Directory.Exists(directoryPath))
            {
                string tempFilePath = Path.GetTempFileName();
                tempZipPath = Path.ChangeExtension(tempFilePath, ".zip");

                ZipFile.CreateFromDirectory(directoryPath, tempZipPath);
            }

            return tempZipPath;
        }
    }
}
