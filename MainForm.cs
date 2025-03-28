using System.Diagnostics;
using System.Windows.Media.Media3D;
namespace AutoDarkModeMin
{
    public partial class MainForm : Form
    {

        private bool isSilentMode;
        private readonly TaskSchedule schedule;

        public MainForm(bool isSilentMode)
        {
            InitializeComponent();

            this.isSilentMode = isSilentMode;
            this.schedule = new TaskSchedule() { mainForm = this };
            this.AutoStart.Checked = true;

        }

        //如果是自启动，则隐藏窗口
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (isSilentMode)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            this.schedule.LoadSettings();
            this.LightStart.Value = this.schedule.lightStart;
            this.DarkStart.Value = this.schedule.darkStart;

        }

        private void LightStart_ValueChanged(object sender, EventArgs e)
        {
            schedule.lightStart = this.LightStart.Value;
        }

        private void DarkStart_ValueChanged(object sender, EventArgs e)
        {
            schedule.darkStart = this.DarkStart.Value;
        }

        private void GitHubLInk_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/qiqd/AutoDarkModeMin") { UseShellExecute = true });
        }
        private void GiteeLink_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://gitee.com/qijiugit/auto-dark-mode") { UseShellExecute = true });
        }
        private void ConfirmUpdate_Click(object sender, EventArgs e)
        {
            this.schedule.InitializeSchedule();
            this.schedule.SaveSettings();
        }

        private void ForceChangeMode(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            RegisterHandle.ChangeMode(radioButton?.Tag?.ToString() == "0");

        }
        private void EnableAutoStart(object sender, EventArgs e)
        {
            RegisterHandle.EnableAutoStart(sender);

        }
        //窗口关闭后最小化
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }

        }
        private void MainFormStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
        }
        //右键菜单关闭
        private void CloseStripMenuItem_Click(object sender, EventArgs e)
        {
            this.schedule.scheduler!.Shutdown();
            this.notifyIcon.Dispose();
            Application.Exit();
        }

        //如果是鼠标右键单击，则显示菜单
        private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ShowInTaskbar = true;
                this.Show();
            }
        }

    }
}
