using System;
using System.Windows;
using System.ServiceProcess;

namespace Predator
{
    internal static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}