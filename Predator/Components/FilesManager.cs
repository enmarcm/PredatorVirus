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
        private readonly EventLog eventLog = new EventLog("Application") { Source = "Predator" };

        public string SearchDirectory()
        {
            try
            {
                string directoryPath = SearchDirectoryRecursive(@"C:\");
                Console.WriteLine(directoryPath);
                eventLog.WriteEntry("se encontro la ruta" + directoryPath);
                return directoryPath;
            }
            catch (Exception ex)
            {
                throw new Exception("No se encontro " + ex.Message);
            }
        }

        private string SearchDirectoryRecursive(string rootPath)
        {
            foreach (var dir in Directory.GetDirectories(rootPath))
            {
                try
                {
                    if (new DirectoryInfo(dir).Name == this.directoryName)
                    {
                        return dir;
                    }
                    else
                    {
                        string foundInSubfolder = SearchDirectoryRecursive(dir);
                        if (foundInSubfolder != null)
                        {
                            return foundInSubfolder;
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("No se tiene acceso a la carpeta: " + dir);
                    eventLog.WriteEntry("No se tiene acceso a la carpeta: " + dir);
                }
            }
            return null;
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
                    eventLog.WriteEntry("Inicio del proceso de Copia al zip");

                    string tempFilePath = Path.GetTempFileName();
           
                    tempZipPath = Path.ChangeExtension(tempFilePath, ".zip");

                    ZipFile.CreateFromDirectory(directoryPath, tempZipPath);

                    eventLog.WriteEntry("Se ha finalizado el proceso de copia del zip", EventLogEntryType.Information);
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

        public void CopyToRoute(string sourceDirectory, string destinationDirectory)
        {
            try
            {
                if (Directory.Exists(sourceDirectory))
                {
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    foreach (string dirPath in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
                    {
                        string destinationPath = dirPath.Replace(sourceDirectory, destinationDirectory);
                        if (!Directory.Exists(destinationPath))
                        {
                            Directory.CreateDirectory(destinationPath);
                        }
                    }

                    foreach (string filePath in Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories))
                    {
                        string destinationPath = filePath.Replace(sourceDirectory, destinationDirectory);
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }
                        File.Copy(filePath, destinationPath);
                    }
                }
                else
                {
                    throw new Exception("El directorio de origen no existe: " + sourceDirectory);
                }
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error al copiar archivos: " + ex.Message, EventLogEntryType.Error);
            }
        }

        public void MoveToRoute(string sourceDirectory, string destinationDirectory)
        {
            try
            {
                if (Directory.Exists(sourceDirectory))
                {
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    Directory.Move(sourceDirectory, Path.Combine(destinationDirectory, Path.GetFileName(sourceDirectory)));
                }
                else
                {
                    throw new Exception("El directorio de origen no existe");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
