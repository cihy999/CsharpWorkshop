namespace DecodeBytes.WinForm
{
    partial class mainForm
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
            saveFileDialog1 = new SaveFileDialog();
            groupBoxProviders = new GroupBox();
            btn_relaod = new Button();
            SuspendLayout();
            // 
            // groupBoxProviders
            // 
            groupBoxProviders.Location = new Point(30, 88);
            groupBoxProviders.Name = "groupBoxProviders";
            groupBoxProviders.Size = new Size(1199, 533);
            groupBoxProviders.TabIndex = 0;
            groupBoxProviders.TabStop = false;
            groupBoxProviders.Text = "Providers";
            // 
            // btn_relaod
            // 
            btn_relaod.Location = new Point(976, 12);
            btn_relaod.Name = "btn_relaod";
            btn_relaod.Size = new Size(253, 70);
            btn_relaod.TabIndex = 1;
            btn_relaod.Text = "Reload Providers";
            btn_relaod.UseVisualStyleBackColor = true;
            btn_relaod.Click += btn_relaod_Click;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 664);
            Controls.Add(btn_relaod);
            Controls.Add(groupBoxProviders);
            Name = "mainForm";
            Text = "BankWindow";
            Load += mainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private SaveFileDialog saveFileDialog1;
        private GroupBox groupBoxProviders;
        private Button btn_relaod;
    }
}
