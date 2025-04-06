using System.Runtime.InteropServices;
using System.Text;

namespace AutoDarkModeMinNetFrameworkEdtion
{
    internal class WindowTopMost
    {
        private Form form;
        private List<IntPtr> TopWindowsList = new List<IntPtr>(); // 存储置顶窗口句柄
        // 导入 SetWindowPos 函数
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        //通过窗口句柄获取窗口类名
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        // 获取窗口标题
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // 定义特殊窗口句柄值
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);     // 置顶窗口
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);  // 取消置顶

        // 定义 SetWindowPos 的标志
        private const uint SWP_NOSIZE = 0x0001;       // 不改变窗口大小
        private const uint SWP_NOMOVE = 0x0002;      // 不改变窗口位置
        private const uint SWP_SHOWWINDOW = 0x0040;  // 显示窗口

        // 导入 GetAncestor 函数,用于获取主窗口句柄
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

        // 定义 GetAncestor 的标志
        private const uint GA_PARENT = 1;      // 获取父窗口
        private const uint GA_ROOT = 2;        // 获取根窗口（顶级窗口）
        private const uint GA_ROOTOWNER = 3;  // 获取拥有者窗口（通常也是顶级窗口）
        //根据鼠标位置获取窗口句柄
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point point);
        public WindowTopMost()
        {
            Form assisteForm = new Form();
            assisteForm.ShowInTaskbar = false;
            assisteForm.KeyPreview = true;
            assisteForm.StartPosition = FormStartPosition.Manual;
            assisteForm.Location = new Point(0, 0);
            assisteForm.FormBorderStyle = FormBorderStyle.None;
            assisteForm.Opacity = 0.3;
            assisteForm.MinimumSize = new Size(1, 1);
            assisteForm.Size = new Size(1, 1);
            assisteForm.WindowState = FormWindowState.Maximized;
            SetWindowPos(assisteForm.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
            assisteForm.MouseClick += DeactivateToSetWindowTopMost;
            assisteForm.KeyDown += EscKeyDown;
            Label label = new Label();
            label.Text = "按下Esc键或者鼠标右键退出窗口捕获";
            label.Font = new Font(FontFamily.GenericSerif, 12);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.AutoSize = false;
            label.Height = 20;
            label.Dock = DockStyle.Top;
            assisteForm.Controls.Add(label);

            this.form = assisteForm;
        }

        private void EscKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.form.Hide();
            }
        }
        public int GetTopWindowsCount()
        {
            return TopWindowsList.Count;
        }
        public void SetWindowTopMost()
        {
            this.form.Show();
            this.form.Cursor = Cursors.Hand;
            //this.form.Activate();
        }
        /// <summary>
        /// 移除所有置顶的窗口同时关闭辅助窗口
        /// </summary>
        public void RemoveAllTopWindows()
        {
            foreach (IntPtr item in TopWindowsList)
            {
                SetWindowPos(item, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
            }
            TopWindowsList.Clear();
            this.form?.Dispose();
            this.form?.Close();

        }
        /// <summary>
        /// 检查窗口是否为桌面或任务栏
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>如果窗口是桌面或任务栏，则返回 true；否则返回 false</returns>
        private bool IsDesktopOrTaskbar(IntPtr hWnd)
        {
            const int MaxClassNameLength = 256;
            StringBuilder className = new StringBuilder(MaxClassNameLength);

            // 获取窗口类名
            GetClassName(hWnd, className, className.Capacity);

            string classNameStr = className.ToString();

            // 检查是否为桌面或任务栏
            if (classNameStr == "Progman" || classNameStr == "WorkerW" || classNameStr == "Shell_TrayWnd")
            {
                this.form.Hide();
                MessageBox.Show("请勿置顶桌面或任务栏窗口。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            return false;
        }
        /// <summary>
        /// 辅助窗口失焦获取鼠标的坐标来得到窗口位置
        /// </summary>
        /// <param name="sender">辅助窗口</param>
        /// <param name="e">事件参数</param>
        private void DeactivateToSetWindowTopMost(object? sender, MouseEventArgs e)
        {
            this.form.Hide();
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            System.Drawing.Point position = Cursor.Position;
            IntPtr intPtr = WindowFromPoint(position);
            // 获取主窗口句柄
            IntPtr mainWindowHandle = GetAncestor(intPtr, GA_ROOT);

            try
            {
                if (IsDesktopOrTaskbar(mainWindowHandle))
                {
                    return;
                }

                if (mainWindowHandle == IntPtr.Zero)
                {
                    Console.WriteLine("未获取到窗口");
                    return;
                }
                bool contains = TopWindowsList.Contains(mainWindowHandle);
                if (contains)
                {
                    bool result = SetWindowPos(mainWindowHandle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
                    TopWindowsList.Remove(mainWindowHandle);
                    Console.WriteLine($"{result} to cancel top most.");
                }
                else
                {
                    bool success = SetWindowPos(mainWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
                    if (!success)
                    {
                        MessageBox.Show("置顶窗口失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    TopWindowsList.Add(mainWindowHandle);

                }
            }
            finally
            {
                //this.form.Hide();
            }

        }

    }
}

