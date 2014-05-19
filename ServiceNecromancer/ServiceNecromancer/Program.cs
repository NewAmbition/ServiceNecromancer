using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceNecromancer
{
    class Program
    {
        public static Timer Timer;
        public static List<string> ServicesList;

        static void Main(string[] args)
        {
            ServicesList = GetServicesToMonitor();

            if (!ServicesList.Any())
            {
                SetConsoleTitle();
                Console.WriteLine("No services to monitor. Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {

                ValidateServices(null); // Run it initially

                StartTimer();

                // Don't stop till I tell you to, slave
                Console.Read();
            }
        }

        private static void SetConsoleTitle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Necromancy /ˈnɛkrɵˌmænsi/ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(
                "is a form of magic involving communication with the\ndeceased – either by summoning their spirit as an apparition or raising them\n bodily.");
            ConsoleNewLine(2);
            Console.WriteLine("Currently monitoring for deceased activity...");
            Console.WriteLine("-------------------------------------------------------------------------------");
            ConsoleNewLine(2);
        }

        private static void ConsoleNewLine(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine();
            }
        }

        private static List<string> GetServicesToMonitor()
        {
            //TODO: Add your own way of getting a list of services to monitor
            return new List<string>
            {
               "Bitch", // Invalid name
               "TeamViewer9", // Valid
               "TorchCrashHandler" // Locked
            };
        }

        private static void StartTimer()
        {
            Timer = new Timer( ValidateServices, null, 5000, Timeout.Infinite );
        }

        private static void ValidateServices(Object state)
        {
            SetConsoleTitle();
            foreach (var service in ServicesList)
            {
                var currentService = service; // Maintain this.
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    UpkeepService(currentService);
                }).Start();
            }

            if(Timer != null)
                Timer.Change(5000, Timeout.Infinite);
        }

        static void UpkeepService(string serviceName)
        {
            if (ValidateExistence(serviceName)) return;

            string currentStatus = Enchiridion.Scrolls.CheckServiceCondition(serviceName);

            Console.WriteLine(serviceName + " - " + currentStatus);

            if (currentStatus == "Running") return;

            DebugMessage("---- Attempting to start " + serviceName);

            AttemptStartingService(serviceName);
        }

        private static void AttemptStartingService(string serviceName)
        {
            string response = Enchiridion.Scrolls.StartService(serviceName);
            if (response != "Running" || response != "Starting")
            {
                DebugMessage("//ERROR STARTING: " + serviceName + " --> " + response);
            }
            else
            {
                DebugMessage("---- Started " + serviceName);
            }
        }

        private static bool ValidateExistence(string serviceName)
        {
            if (Enchiridion.Scrolls.ServiceExists(serviceName)) return false;

            DebugMessage("//INVALID SERVICE: " + serviceName);

            return true;
        }

        private static void DebugMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(20); // Just stop a bit so we don't have an issue with color leak
        }
    }
}
