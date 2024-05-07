using System.ServiceProcess;

namespace PrintServerService
{
    public partial class PrintServer : ServiceBase
    {
        public PrintServer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
