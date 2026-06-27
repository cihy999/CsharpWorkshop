using System.ComponentModel;

namespace EAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            if (worker == null) return;

            for (int i = 1; i <= 100; i++)
            {
                // 檢查是否取消
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // 模擬下載延遲
                Thread.Sleep(100);

                // 回報進度
                worker.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = $"{e.ProgressPercentage}%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            button1.Text = "OK";

            if (e.Error != null)
            {
                MessageBox.Show($"下載過程發生錯誤：{e.Error.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                progressBar1.Value = 0;
                label1.Text = "0%";
                MessageBox.Show("下載已被取消！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("下載完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                // 避免使用者重複取消
                button1.Enabled = false;
                // 取消背景工作
                backgroundWorker1.CancelAsync();
            }
            else 
            {
                button1.Text = "Cancel";
                progressBar1.Value = 0;
                label1.Text = $"{progressBar1.Value}%";

                // 啟動背景工作
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}
