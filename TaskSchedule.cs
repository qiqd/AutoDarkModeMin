using Quartz;
using Quartz.Impl;
using System.Text.Json;
namespace MiniAutoDarkMode
{

    internal class TaskSchedule
    {
        internal bool enablePin = false; //是否启用置顶窗口
        //默认的触发时间
        internal static TimeOnly lightStart = new TimeOnly(6, 0, 0);
        internal static TimeOnly darkStart = new TimeOnly(20, 0, 0);
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
        internal MainForm? mainForm { get; set; }
        internal static IntPtr mainFormHandle { get; set; }
        public TaskSchedule()
        {
            //Environment.SpecialFolder.Programs
            this.userPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            this.appFolder = Path.Combine(userPath, "AutoDarkMin");
            LoadSettings();
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
        public void SaveSettings()
        {
            UserInfo userInfo = new UserInfo() { start = lightStart, end = darkStart, enablePin = enablePin };
            string v = JsonSerializer.Serialize(userInfo);

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            File.WriteAllText(appFolder + "\\setting.json", v);

        }
        public void LoadSettings()
        {
            try
            {
                UserInfo? userInfo = JsonSerializer.Deserialize<UserInfo>(File.ReadAllText(appFolder + "\\setting.json"));
                lightStart = userInfo!.start ?? new TimeOnly(6, 0, 0);
                darkStart = userInfo.end ?? new TimeOnly(20, 0, 0);
                enablePin = userInfo.enablePin;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (File.Exists(appFolder + "\\setting.json"))
                {
                    File.Delete("setting.json");
                }
            }

        }
        class JobToDark : IJob
        {

            public Task Execute(IJobExecutionContext context)
            {

                RegisterHandle.ChangeMode(false, mainFormHandle);
                return Task.CompletedTask;
            }
        }
        class JobToLight : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                RegisterHandle.ChangeMode(true, mainFormHandle);
                return Task.CompletedTask;
            }
        }
    }

}
