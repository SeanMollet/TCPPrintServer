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
    }
}