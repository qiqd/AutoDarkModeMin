using AutoDarkModeMinNetFrameworkEdtion;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MiniAutoDarkMode
{
    public partial class MainForm : Form
    {  // 定义 DWM_WINDOW_CORNER_PREFERENCE 枚举
        private enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,      // 默认（系统决定）
            DWMWCP_DONOTROUND = 1,  // 禁用圆角
            DWMWCP_ROUND = 2,       // 启用圆角
            DWMWCP_ROUNDSMALL = 3   // 小圆角
        }
        private WindowTopMost? windowTopMost;
        private const int WM_SETTINGCHANGE = 0x001A;
        private bool isSilentMode;
        private readonly TaskSchedule schedule;

        public MainForm(bool isSilentMode)
        {
            InitializeComponent();

            this.isSilentMode = isSilentMode;
            this.schedule = new TaskSchedule() { mainForm = this };
            TaskSchedule.mainFormHandle = this.Handle;
            this.AutoStart.Checked = true;
            InitializeTheme();
        }

        private void InitializeTheme()
        {
            //this.schedule.LoadSettings();
            //this.schedule.InitializeSchedule();
            TimeOnly timeOnly = TimeOnly.FromDateTime(DateTime.Now);
            if (timeOnly >= TaskSchedule.lightStart && timeOnly < TaskSchedule.darkStart)
            {
                RegisterHandle.ChangeMode(true, this.Handle);
                ApplyLightTheme(this);
            }
            else
            {
                RegisterHandle.ChangeMode(false, this.Handle);
                ApplyDarkTheme(this);
            }
        }

        /// <summary>
        /// 窗体加载事件,设置窗体不显示在任务栏以及任务切换器页面的显示必须要按照这个顺序，否则会失效。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (isSilentMode)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                base.SetVisibleCore(false);
            }

            this.LightStart.Value = new DateTime(2022, 1, 1, TaskSchedule.lightStart.Hour, TaskSchedule.lightStart.Minute, 0);
            this.DarkStart.Value = new DateTime(2022, 1, 1, TaskSchedule.darkStart.Hour, TaskSchedule.darkStart.Minute, 0);
            this.EnableWindowTopMost.Checked = this.schedule.enablePin;
            const int DWMWA_WINDOW_CORNER_PREFERENCE = 33; // 属性ID
            int cornerPreference = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUNDSMALL; // 圆角样式
            NotifySysChangeTheme.DwmSetWindowAttribute(this.contextMenuStrip.Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref cornerPreference, sizeof(int));
        }

        private void LightStart_ValueChanged(object sender, EventArgs e)
        {
            TaskSchedule.lightStart = TimeOnly.FromDateTime(this.LightStart.Value);
        }

        private void DarkStart_ValueChanged(object sender, EventArgs e)
        {
            TaskSchedule.darkStart = TimeOnly.FromDateTime(this.DarkStart.Value);
        }

        private void GitHubLInk_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/qiqd/MiniAutoDarkMode") { UseShellExecute = true });
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
            RegisterHandle.ChangeMode(radioButton?.Tag?.ToString() == "0", this.Handle);

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
            if (this.windowTopMost != null)
            {
                windowTopMost.RemoveAllTopWindows();
                this.windowTopMost = null;
            }
            this.schedule.scheduler!.Shutdown();
            this.notifyIcon.Dispose();
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETTINGCHANGE)
            {
                if (m.LParam != IntPtr.Zero && Marshal.PtrToStringUni(m.LParam) == "ImmersiveColorSet")
                {
                    bool isDarkMode = SystemThemeHelper.IsSystemInDarkMode();
                    ToggleTheme(isDarkMode);
                }
            }
            base.WndProc(ref m);
        }
        public void ToggleTheme(bool isDarkMode)
        {
            if (isDarkMode)
            {
                ApplyDarkTheme(this);
            }
            else
            {
                ApplyLightTheme(this);
            }
        }

        public void ApplyDarkTheme(Control control)
        {
            control.BackColor = Color.FromArgb(45, 45, 48);
            control.ForeColor = Color.White;
            foreach (Control child in control.Controls)
            {
                ApplyDarkTheme(child);
            }
        }

        public void ApplyLightTheme(Control control)
        {
            control.BackColor = SystemColors.Window;
            control.ForeColor = SystemColors.ControlText;
            foreach (Control child in control.Controls)
            {
                ApplyLightTheme(child);
            }
        }
        private void EnableWindowTopMost_CheckedChanged(object sender, EventArgs e)
        {

            this.schedule.enablePin = EnableWindowTopMost.Checked;
        }

        private void CancelAllTopMenuItem_Click(object sender, EventArgs e)
        {
            if (this.windowTopMost == null) return;
            windowTopMost.RemoveAllTopWindows();
            this.windowTopMost = null;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.windowTopMost == null)
            {
                this.windowTopMost = new WindowTopMost();

            }
            if (this.EnableWindowTopMost.Checked)
            {
                windowTopMost.SetWindowTopMost();
            }

        }

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            this.notifyIcon.Text = this.windowTopMost == null ? "MiniAutoDarkMode" : $"MiniAutoDarkMode - 置顶窗口数: {this.windowTopMost.GetTopWindowsCount()}";
        }
    }

}

