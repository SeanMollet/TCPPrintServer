using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace PrintServerService
{
    public partial class PrintServer : ServiceBase
    {
        private EventLog eventLog1;
        private SocketPrintServer server = null;
        public PrintServer()
        {
            InitializeComponent();
            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("PrintServer"))
            {
                EventLog.CreateEventSource("PrintServer", "PrintServerLog");
            }

            eventLog1.Source = "PrintServer";
            eventLog1.Log = "PrintServerLog";
        }

        private void logPrints(string message)
        {
            eventLog1.WriteEntry(message);
        }

        protected override void OnStart(string[] args)
        {
            var printer = RawPrinterHelper.GetPrinter();
            if (string.IsNullOrEmpty(printer))
            {
                eventLog1.WriteEntry("Unable to find printer! Please configure.");
                Environment.Exit(1);
            }
            eventLog1.WriteEntry("Launching print server");
            server = new SocketPrintServer(logPrints);
            server.Start();
        }

        protected override void OnStop()
        {
            if (server != null)
            {
                eventLog1.WriteEntry("Stopping print server");
                server.Shutdown();
            }
        }
    }
}
