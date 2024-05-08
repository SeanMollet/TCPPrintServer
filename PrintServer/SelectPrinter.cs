using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Windows.Forms;

namespace PrintServer
{
    public partial class SelectPrinter : Form
    {
        private SocketPrintServer server = null;
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

            server = new SocketPrintServer(UpdateLog);
            server.Start();

            //If we've already been configured, launch minimized
            if (RawPrinterHelper.GetConfiguredPrinter().Length > 0)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            UpdateButton();

        }

        private void UpdateButton()
        {
            if (StartupExists())
            {
                this.button4.Text = "Remove Startup";
            }
            else
            {
                this.button4.Text = "Install Startup";
            }
        }
        private bool StartupExists()
        {
            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var binPath = Path.Combine(startUpFolderPath, "PrintServer.lnk");
            if (System.IO.File.Exists(binPath))
            {
                return true;
            }
            return false;
        }
        private void InstallStartup()
        {
            WshShell wshShell = new WshShell();

            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Create the shortcut
            shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(startUpFolderPath + "\\PrintServer.lnk");

            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Launch Print Server";
            // shortcut.IconLocation = Application.StartupPath + @"\App.ico";
            shortcut.Save();
        }

        private void RemoveStartup()
        {
            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var binPath = Path.Combine(startUpFolderPath, "PrintServer.lnk");
            if (System.IO.File.Exists(binPath))
            {
                System.IO.File.Delete(binPath);
            }
        }
        private void UpdateLog(string message)
        {
            this.button3.Invoke((MethodInvoker)delegate
            {
                var oldtext = this.textBox1.Text;
                var newtext = oldtext + message + Environment.NewLine;
                this.textBox1.Text = newtext;

            });
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                this.WindowState = FormWindowState.Minimized;
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



        private void form_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "Print server minimized to tray.";
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void SelectPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Shutdown();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (StartupExists())
            {
                RemoveStartup();
            }
            else
            {
                InstallStartup();
            }
            UpdateButton();
        }
    }
}