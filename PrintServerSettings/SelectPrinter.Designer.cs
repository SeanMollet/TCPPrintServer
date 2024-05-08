namespace PrintServerSettings
{
    partial class SelectPrinter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            listBox1 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "Available";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(75, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(236, 214);
            listBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(12, 232);
            button1.Name = "button1";
            button1.Size = new Size(147, 23);
            button1.TabIndex = 4;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(165, 232);
            button2.Name = "button2";
            button2.Size = new Size(146, 23);
            button2.TabIndex = 5;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 261);
            button3.Name = "button3";
            button3.Size = new Size(299, 23);
            button3.TabIndex = 6;
            button3.Text = "Install Service";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 290);
            button4.Name = "button4";
            button4.Size = new Size(299, 23);
            button4.TabIndex = 7;
            button4.Text = "Remove Service";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // SelectPrinter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 333);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Controls.Add(label2);
            Name = "SelectPrinter";
            Text = "Select printer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private ListBox listBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}