using PrintServerService;

namespace PrintServerSettings
{
    public partial class SelectPrinter : Form
    {
        public SelectPrinter()
        {
            InitializeComponent();
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            var printers = RawPrinterHelper.GetListOfPrinters();
            var selected = RawPrinterHelper.GetPrinter();

            int pos = 0;
            foreach (var printer in printers)
            {
                if (!String.IsNullOrEmpty(printer))
                {
                    listBox1.Items.Add(printer);
                    if (!String.IsNullOrEmpty(selected) && selected == printer)
                    {
                        listBox1.SelectedIndex = pos;
                    }
                    pos++;
                }
            }

        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            this.Text = "Select printer";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    var printer = listBox1.SelectedItem.ToString();
                    RawPrinterHelper.SaveConfiguredPrinter(printer);
                }
                this.Text = "Select printer - Saved";
                this.Close();
            }
            catch (Exception E)
            {
                this.Text = "Select printer - Error saving";
                Console.WriteLine("Error saving printer:" + E.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string findInstallUtil()
        {
            string NetPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
            string checkPath = Path.Combine(NetPath, "InstallUtil.exe");
            if (File.Exists(checkPath))
            {
                return checkPath;
            }
            return "";
        }

        private bool callInstaller(bool remove = false)
        {
            try
            {
                var installer = findInstallUtil();
                if (String.IsNullOrEmpty(installer))
                {
                    Console.WriteLine("Couldn't find installutil.exe! Unable to install.");
                    return false;
                }

                var ourPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (ourPath == null)
                {
                    Console.WriteLine("Couldn't find PriintServerService.exe! Unable to install.");
                    return false;
                }
                ourPath = Path.GetDirectoryName(ourPath);
                if (ourPath == null)
                {
                    Console.WriteLine("Couldn't find PriintServerService.exe! Unable to install.");
                    return false;
                }
                ourPath = Path.Combine(ourPath, "PrintServiceService.exe");
                if (!File.Exists(ourPath))
                {
                    Console.WriteLine("Couldn't find PriintServerService.exe! Unable to install.");
                    return false;
                }
                string arguments;

                if (remove)
                {
                    arguments = "-u " + ourPath;
                }
                else
                {
                    arguments = ourPath;
                }
                var proc = System.Diagnostics.Process.Start(installer, arguments);

            }
            catch (Exception E)
            {
                Console.WriteLine("Error (un)installing service:" + E.ToString());
            }
            return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var result = callInstaller(false);
            if (result)
            {
                this.Text = "Select printer - Service installed";
            }
            else
            {
                this.Text = "Select printer - Failed to install service";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = callInstaller(true);
            if (result)
            {
                this.Text = "Select printer - Service removed";
            }
            else
            {
                this.Text = "Select printer - Failed to remove service";
            }

        }
    }
}