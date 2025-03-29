using Microsoft.Win32;
using Quartz;
using Quartz.Impl;
using System.IO;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AutoDarkModeMin
{

    internal class TaskSchedule
    {
        //默认的触发时间
        internal DateTime lightStart = new DateTime(2022, 1, 1, 6, 0, 0);
        internal DateTime darkStart = new DateTime(2022, 1, 1, 20, 0, 0);
        // 创建调度器
        internal IScheduler? scheduler;
        //设置系统主题的注册表路径
        private const string ThemePath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        //应用程序主题的注册表键
        private const string AppsUseLightThemeKey = "AppsUseLightTheme";
        //系统主题的注册表键
        private const string SystemUsesLightThemeKey = "SystemUsesLightTheme";
        internal string userPath;
        internal string appFolder;
        public MainForm? mainForm { get; set; }
        public TaskSchedule()
        {
            this.userPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            this.appFolder = Path.Combine(userPath, "AutoDarkMin");
            InitializeSchedule();
        }
        internal async void InitializeSchedule()
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
                Task.Run(() => NotifySysChangeTheme.RefreshTheme());

            }
        }
        public void SaveSettings()
        {
            UserInfo userInfo = new UserInfo() { start = lightStart, end = darkStart };
            string v = JsonSerializer.Serialize(userInfo);

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            File.WriteAllText(appFolder + "setting.json", v);
        }
        public void LoadSettings()
        {
            try
            {
                UserInfo? userInfo = JsonSerializer.Deserialize<UserInfo>(File.ReadAllText(appFolder + "setting.json"));
                this.lightStart = userInfo.start ?? new DateTime(2022, 1, 1, 6, 0, 0);
                this.darkStart = userInfo.end ?? new DateTime(2022, 1, 1, 20, 0, 0);
            }
            catch (Exception e)
            {
                File.Delete("setting.json");
            }

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
    }

}
