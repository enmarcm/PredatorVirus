using System.Linq;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System;

namespace Predator
{
    internal class FilesManager
    {
        private readonly string directoryName = ConfigurationManager.AppSettings["directoryName"].ToString();
        private readonly EventLog eventLog = new EventLog("Apllication");

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
                try
                {
                    eventLog.WriteEntry("Inicio del proceso de Copia");

                    string tempFilePath = Path.GetTempFileName();
           
                    tempZipPath = Path.ChangeExtension(tempFilePath, ".zip");

                    ZipFile.CreateFromDirectory(directoryPath, tempZipPath);

                    eventLog.WriteEntry("Se ha finalizado el proceso de copia", EventLogEntryType.Information);
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                }
            }
            else
            {
                eventLog.WriteEntry("El directorio no existe", EventLogEntryType.Error);
            }

            return tempZipPath;
        }
    }
}
