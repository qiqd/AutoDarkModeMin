using Microsoft.Win32;
using Quartz;
using Quartz.Impl;
using System.Diagnostics;
namespace AutoDarkModeMin
{
    public partial class MainForm : Form
    {
        // ����������
        private IScheduler? scheduler;
        //Ĭ�ϵĴ���ʱ��
        private DateTime lightStart = new DateTime(2022, 1, 1, 6, 0, 0);
        private DateTime darkStart = new DateTime(2022, 1, 1, 20, 0, 0);
        //������ע���·��
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        //����ϵͳ�����ע���·��
        private const string ThemePath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RunKey = "AutoDarkModeMin";

        //Ӧ�ó��������ע����
        private const string AppsUseLightThemeKey = "AppsUseLightTheme";
        //ϵͳ�����ע����
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

            // ������ɫģʽ��ҵ
            IJobDetail darkJob = JobBuilder.Create<JobToDark>()
                .WithIdentity("JobToDark", "group1")
                .Build();

            // ����ǳɫģʽ��ҵ
            IJobDetail lightJob = JobBuilder.Create<JobToLight>()
                .WithIdentity("JobToLight", "group1") // ͬһ����Ҳ����
                .Build();

            // ������ɫģʽ������
            ITrigger darkTrigger = TriggerBuilder.Create()
                .WithIdentity("darkTrigger", "group1")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(darkStart.Hour, darkStart.Minute))
                .Build();

            // ����ǳɫģʽ������
            ITrigger lightTrigger = TriggerBuilder.Create()
                .WithIdentity("lightTrigger", "group1") // ������������ҪΨһ
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(lightStart.Hour, lightStart.Minute))
                .Build();
            bool darktriggerExists = await scheduler.CheckExists(new TriggerKey("darkTrigger", "group1"));
            bool lighttriggerExists = await scheduler.CheckExists(new TriggerKey("lightTrigger", "group1"));
            if (!darktriggerExists || !lighttriggerExists)
            {
                // ��������������ڣ�����ӵ�������
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
                // ����������Ѵ��ڣ����滻������
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
                    MessageBox.Show("�޷���ע�����");
                    return;
                }
                // ����ϵͳ���⣨���֧�֣�
                if (key.GetValue(SystemUsesLightThemeKey) != null)
                {
                    key.SetValue(SystemUsesLightThemeKey, isLight ? 1 : 0, RegistryValueKind.DWord);
                }

                // ����Ӧ�ó�������
                key.SetValue(AppsUseLightThemeKey, isLight ? 1 : 0, RegistryValueKind.DWord);
                // �첽ˢ��ϵͳ����
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
            MessageBox.Show(checkBox.Checked ? "��������������" : "���������ѽ���");
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true))
            {
                if (key == null)
                {
                    MessageBox.Show("�޷���ע�����");
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
        //���ڹرպ���С��
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
