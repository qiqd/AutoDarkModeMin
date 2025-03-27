using Microsoft.Win32;
using Quartz;
using Quartz.Impl;
using System.Diagnostics;
namespace AutoDarkModeMin
{
    public partial class MainForm : Form
    {
        // 创建调度器
        private IScheduler? scheduler;
        //默认的触发时间
        private DateTime lightStart = new DateTime(2022, 1, 1, 6, 0, 0);
        private DateTime darkStart = new DateTime(2022, 1, 1, 20, 0, 0);
        //自启动注册表路径
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        //设置系统主题的注册表路径
        private const string ThemePath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RunKey = "AutoDarkModeMin";

        //应用程序主题的注册表键
        private const string AppsUseLightThemeKey = "AppsUseLightTheme";
        //系统主题的注册表键
        private const string SystemUsesLightThemeKey = "SystemUsesLightTheme";

        public MainForm()
        {
            InitializeComponent();
            InitializeSchedule();

        }



        class JobToDark : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                ChangeMode(false);
                return Task.CompletedTask;
            }
        }
        class JobToLight : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                ChangeMode(true);
                return Task.CompletedTask;
            }
        }
        private async void InitializeSchedule()
        {
            this.scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            // 创建深色模式作业
            IJobDetail darkJob = JobBuilder.Create<JobToDark>()
                .WithIdentity("JobToDark", "group1")
                .Build();

            // 创建浅色模式作业
            IJobDetail lightJob = JobBuilder.Create<JobToLight>()
                .WithIdentity("JobToLight", "group1") // 同一个组也可以
                .Build();

            // 创建深色模式触发器
            ITrigger darkTrigger = TriggerBuilder.Create()
                .WithIdentity("darkTrigger", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(darkStart.Hour, darkStart.Minute))
                .Build();

            // 创建浅色模式触发器
            ITrigger lightTrigger = TriggerBuilder.Create()
                .WithIdentity("lightTrigger", "group1") // 触发器名称需要唯一
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(lightStart.Hour, lightStart.Minute))
                .Build();
            bool darktriggerExists = await scheduler.CheckExists(new TriggerKey("darkTrigger", "group1"));
            bool lighttriggerExists = await scheduler.CheckExists(new TriggerKey("lightTrigger", "group1"));
            if (!darktriggerExists || !lighttriggerExists)
            {
                // 如果触发器不存在，则添加到调度器
                if (!darktriggerExists)
                {
                    await scheduler.ScheduleJob(darkJob, darkTrigger);
                }
                if (!lighttriggerExists)
                {
                    await scheduler.ScheduleJob(lightJob, lightTrigger);
                }
            }
            else
            {
                // 如果触发器已存在，则替换触发器
                await scheduler.RescheduleJob(new TriggerKey("darkTrigger", "group1"), darkTrigger);
                await scheduler.RescheduleJob(new TriggerKey("lightTrigger", "group1"), lightTrigger);
            }


        }

        private void LightStart_ValueChanged(object sender, EventArgs e)
        {
            lightStart = this.LightStart.Value;
        }

        private void DarkStart_ValueChanged(object sender, EventArgs e)
        {
            darkStart = this.DarkStart.Value;
        }
        private static void ChangeMode(bool isLight)
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(ThemePath, true))
            {
                if (key == null)
                {
                    MessageBox.Show("无法打开注册表项");
                    return;
                }
                // 设置系统主题（如果支持）
                if (key.GetValue(SystemUsesLightThemeKey) != null)
                {
                    key.SetValue(SystemUsesLightThemeKey, isLight ? 1 : 0, RegistryValueKind.DWord);
                }

                // 设置应用程序主题
                key.SetValue(AppsUseLightThemeKey, isLight ? 1 : 0, RegistryValueKind.DWord);
                // 异步刷新系统主题
                Task.Run(() => ForceRefreshSysTheme.RefreshTheme());

                //ExplorerRestart.RestartExplorer();

            }
        }

        private void GitHubLInk_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/qiqd/AutoDarkModeMin"));
        }
        private void GiteeLink_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://gitee.com/qijiugit/auto-dark-mode"));
        }
        private void ConfirmUpdate_Click(object sender, EventArgs e)
        {
            this.InitializeSchedule();
        }


        private void ForceChangeMode(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            ChangeMode(radioButton.Tag.ToString() == "0");
        }
        private void EnableAutoStart(object sender, EventArgs e)
        {
            CheckBox? checkBox = sender as CheckBox;
            MessageBox.Show(checkBox.Checked ? "开机自启已启用" : "开机自启已禁用");
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true))
            {
                if (key == null)
                {
                    MessageBox.Show("无法打开注册表项");
                    return;
                }
                if (checkBox.Checked)
                {
                    key.SetValue(RunKey, Application.ExecutablePath);
                }
                else
                {
                    key.DeleteValue(RunKey, false);
                }
            }
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
            this.Show();
        }

        private void CloseStripMenuItem_Click(object sender, EventArgs e)
        {
            this.scheduler?.Shutdown();
            this.notifyIcon.Dispose();
            Application.Exit();
        }


        private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
            }
        }


    }
}
