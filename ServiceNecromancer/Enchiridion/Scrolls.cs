using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Enchiridion
{
    public class Scrolls
    {
        public static List<string> GetServicesList()
        {
            var services = ServiceController.GetServices();

            return services.Select(service => service.ServiceName).ToList();
        }

        public static bool ServiceExists(string servicename)
        {
            return GetServicesList().FirstOrDefault(x => x == servicename) != null;
        }

        public static bool CheckIfServiceInList(string servicename)
        {
            return GetServicesList().Contains(servicename);
        }

        public static string CheckServiceCondition(string servicename)
        {
            ServiceController sc = new ServiceController(servicename);

            sc.Refresh();

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                default:
                    return "Status Changing";
            }
        }

        public static string StartService(string servicename, string[] parameters = null)
        {
            ServiceController sc = new ServiceController(servicename);

            if (sc.Status == ServiceControllerStatus.Running) return "Running";

            try
            {
                if (parameters != null)
                {
                    sc.Start(parameters);
                }
                else
                {
                    sc.Start();
                }

                return CheckServiceCondition(servicename);
            }
            catch (Exception e)
            {
                //Service can't start because you don't have rights, or its locked by another proces or application
                return e.Message;
            }
        }

        public static string StopService(string servicename)
        {
            ServiceController sc = new ServiceController(servicename);

            if (!sc.CanStop) return "Service cannot stop";

            sc.Stop();
            return CheckServiceCondition(servicename);
        }
    }
}
