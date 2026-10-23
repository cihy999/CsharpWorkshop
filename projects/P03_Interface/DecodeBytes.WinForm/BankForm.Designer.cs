namespace DecodeBytes.WinForm
{
    partial class BankForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            tbox_cardNumber = new TextBox();
            tbox_amount = new TextBox();
            label2 = new Label();
            btn_addToBalance = new Button();
            btn_checkBalance = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 100);
            label1.Name = "label1";
            label1.Size = new Size(122, 23);
            label1.TabIndex = 0;
            label1.Text = "Card number";
            // 
            // tbox_cardNumber
            // 
            tbox_cardNumber.Location = new Point(258, 97);
            tbox_cardNumber.Name = "tbox_cardNumber";
            tbox_cardNumber.Size = new Size(330, 30);
            tbox_cardNumber.TabIndex = 1;
            // 
            // tbox_amount
            // 
            tbox_amount.Location = new Point(258, 161);
            tbox_amount.Name = "tbox_amount";
            tbox_amount.Size = new Size(330, 30);
            tbox_amount.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(70, 164);
            label2.Name = "label2";
            label2.Size = new Size(79, 23);
            label2.TabIndex = 2;
            label2.Text = "Amount";
            // 
            // btn_addToBalance
            // 
            btn_addToBalance.Location = new Point(258, 286);
            btn_addToBalance.Name = "btn_addToBalance";
            btn_addToBalance.Size = new Size(241, 53);
            btn_addToBalance.TabIndex = 4;
            btn_addToBalance.Text = "Add to balance";
            btn_addToBalance.UseVisualStyleBackColor = true;
            btn_addToBalance.Click += btn_addToBalance_Click;
            // 
            // btn_checkBalance
            // 
            btn_checkBalance.Location = new Point(636, 97);
            btn_checkBalance.Name = "btn_checkBalance";
            btn_checkBalance.Size = new Size(129, 94);
            btn_checkBalance.TabIndex = 5;
            btn_checkBalance.Text = "Check Balance";
            btn_checkBalance.UseVisualStyleBackColor = true;
            btn_checkBalance.Click += btn_checkBalance_Click;
            // 
            // BankForm
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 424);
            Controls.Add(btn_checkBalance);
            Controls.Add(btn_addToBalance);
            Controls.Add(tbox_amount);
            Controls.Add(label2);
            Controls.Add(tbox_cardNumber);
            Controls.Add(label1);
            Name = "BankForm";
            Text = "BankForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbox_cardNumber;
        private TextBox tbox_amount;
        private Label label2;
        private Button btn_addToBalance;
        private Button btn_checkBalance;
    }
}