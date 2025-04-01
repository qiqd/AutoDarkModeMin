using Microsoft.Win32;

namespace AutoDarkModeMin
{
    internal class RegisterHandle
    {
        //自启动注册表路径
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        //设置系统主题的注册表路径
        private const string ThemePath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RunKey = "AutoDarkModeMin";

        //应用程序主题的注册表键
        private const string AppsUseLightThemeKey = "AppsUseLightTheme";
        //系统主题的注册表键
        private const string SystemUsesLightThemeKey = "SystemUsesLightTheme";
        internal static void ChangeMode(bool isLight, IntPtr handle)
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(ThemePath, true);
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
            Task.Run(() =>
            {
                NotifySysChangeTheme.RefreshTheme();
                int transparency = isLight ? 0 : 1; // 窗口深浅色模式，·1·为深色模式，·0·为浅色模式
                NotifySysChangeTheme.DwmSetWindowAttribute(handle, 20, ref transparency, sizeof(int));
            });
        }
        internal static void EnableAutoStart(object sender)
        {
            CheckBox? checkBox = sender as CheckBox;
            //MessageBox.Show(checkBox.Checked ? "开机自启已启用" : "开机自启已禁用");
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath, true))
            {
                if (key == null)
                {
                    MessageBox.Show("无法打开注册表项");
                    return;
                }
                if (checkBox!.Checked)
                {
                    // 添加 /silent 参数以标识自启动模式
                    key.SetValue(RunKey, $"\"{Application.ExecutablePath}\" /silent");

                }
                else
                {
                    key.DeleteValue(RunKey, false);
                }
            }
        }
        internal static bool InitializeAutoStartButton()
        {

            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKeyPath))
            {
                return key != null;

            }
        }
    }
}
