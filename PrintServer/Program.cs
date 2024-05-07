namespace PrintServer
{
    public class Print
    {
        public static void printLog(string message)
        {
            message = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff ") + message;
            Console.WriteLine(message);
        }
        public static void Main()
        {
            var server = new SocketPrintServer(printLog);
            server.Start();

            while (server.isRunning())
            {
                Thread.Sleep(500);
            }
            //var printer = RawPrinterHelper.GetConfiguredPrinter();
            //if (String.IsNullOrEmpty(printer))
            //{
            //    printer = RawPrinterHelper.FindLabelPrinter();
            //    if (!String.IsNullOrEmpty(printer))
            //    {
            //        RawPrinterHelper.SaveConfiguredPrinter(printer);
            //    }
            //}

            //if (String.IsNullOrEmpty(printer))
            //{
            //    Console.WriteLine("No suitable printer found!");
            //    return;
            //}
            //List<byte> label = new List<byte>();
            //label.Add(0xEF);
            //label.Add(0xBB);
            //label.Add(0xBF);
            //label.Add(0x10);

            //var partNumber = "TestPart";
            //var labeltext = "CT~~CD,~CC^~CT~\n^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR2,2~SD15^JUS^LRN^CI28^PA0,1,1,0^XZ^XA^MMT^PW406^LL0406^LS0^FT5,80^A0N,80,40";
            //labeltext += "^FH\\^FD" + partNumber + "^FS^PQ1,0,1,Y^XZ";

            //label.AddRange(Encoding.ASCII.GetBytes(labeltext));

            //RawPrinterHelper.SendBytesToPrinter(printer, label.ToArray());
        }
    }


}