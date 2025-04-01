using Microsoft.Win32;

namespace AutoDarkModeMin
{
    internal class SystemThemeHelper
    {
        public static bool IsSystemInDarkMode()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
            {
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    if (value is int themeValue)
                    {
                        return themeValue == 0;
                    }
                }
            }
            return false;
        }
    }
}
