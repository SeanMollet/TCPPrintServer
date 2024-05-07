using System.Net;
using System.Net.Sockets;

namespace PrintServer
{
    public class SocketPrintServer
    {
        public SocketPrintServer(Action<string> _loggingAction)
        {
            loggingAction = _loggingAction;
        }

        private Thread? bgThread = null;
        private bool threadContinue = true;
        private Action<string> loggingAction = null;
        public void Start()
        {
            bgThread = new Thread(socketProcessor);
            bgThread.Start();
        }

        public void Shutdown()
        {
            threadContinue = false;
            bgThread?.Join();
        }

        public bool isRunning()
        {
            return threadContinue;
        }

        public void socketProcessor()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9100);
            server.Start();
            while (threadContinue)
            {
                TcpClient client = server.AcceptTcpClient();

                NetworkStream ns = client.GetStream();
                ns.ReadTimeout = 2 * 1000; //Time these out so we can close down

                List<byte> printJob = new List<byte>();
                int maxZeroReads = 5;
                while (client.Connected && maxZeroReads > 0)
                {
                    byte[] msg = new byte[512];
                    int readLength = ns.Read(msg, 0, msg.Length);
                    if (readLength > 0)
                    {
                        maxZeroReads--;
                    }
                    for (int i = 0; i < readLength; i++)
                    {
                        printJob.Add(msg[i]);
                    }
                }

                if (client != null && client.Connected)
                {
                    client.Close();
                }

                if (printJob.Count > 0)
                {
                    string printer = "Unknown";
                    try
                    {
                        printer = RawPrinterHelper.GetPrinter();
                        if (!String.IsNullOrEmpty(printer) && printer != "Unknown")
                        {
                            RawPrinterHelper.SendBytesToPrinter(printer, printJob.ToArray());
                            loggingAction("Sent job of " + printJob.Count.ToString() + " bytes to printer: " + printer);
                        }
                    }
                    catch (Exception E)
                    {
                        loggingAction("Error printing job to (" + printer + "):" + E.ToString());
                    }
                }

            }
        }
    }
}
