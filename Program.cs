namespace AutoDarkModeMin
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "AutoDarkModeMin");
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                bool isSilentMode = Array.Exists(args, arg => arg.Equals("/silent", StringComparison.OrdinalIgnoreCase));
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm(isSilentMode));
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已经在运行了");
            }
        }
    }
}