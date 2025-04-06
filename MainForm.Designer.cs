namespace AutoDarkModeMin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TimePickerGroup = new GroupBox();
            panel2 = new Panel();
            DarkStart = new DateTimePicker();
            DarkModeTime = new Label();
            panel1 = new Panel();
            LightStart = new DateTimePicker();
            LightModeTime = new Label();
            SettingGroup = new GroupBox();
            panel3 = new Panel();
            ForceDark = new RadioButton();
            ForceLight = new RadioButton();
            EnableWindowTopMost = new CheckBox();
            AutoStart = new CheckBox();
            groupBox1 = new GroupBox();
            ConfirmUpdate = new Button();
            AboutGitee = new Panel();
            GiteeLink = new Label();
            pictureBox2 = new PictureBox();
            AboutGithub = new Panel();
            GitHubLInk = new Label();
            pictureBox1 = new PictureBox();
            contextMenuStrip = new ContextMenuStrip(components);
            MainFormStripMenuItem = new ToolStripMenuItem();
            CancelAllTopWindowsMenuItem = new ToolStripMenuItem();
            CloseStripMenuItem = new ToolStripMenuItem();
            notifyIcon = new NotifyIcon(components);
            toolTip = new ToolTip(components);
            TimePickerGroup.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SettingGroup.SuspendLayout();
            panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            AboutGitee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            AboutGithub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TimePickerGroup
            // 
            TimePickerGroup.BackColor = Color.Transparent;
            TimePickerGroup.Controls.Add(panel2);
            TimePickerGroup.Controls.Add(panel1);
            TimePickerGroup.Dock = DockStyle.Top;
            TimePickerGroup.Font = new Font("微软雅黑", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            TimePickerGroup.ForeColor = Color.Black;
            TimePickerGroup.Location = new Point(0, 0);
            TimePickerGroup.Name = "TimePickerGroup";
            TimePickerGroup.Size = new Size(800, 187);
            TimePickerGroup.TabIndex = 0;
            TimePickerGroup.TabStop = false;
            TimePickerGroup.Text = "时间选择";
            // 
            // panel2
            // 
            panel2.Controls.Add(DarkStart);
            panel2.Controls.Add(DarkModeTime);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 109);
            panel2.Name = "panel2";
            panel2.Size = new Size(794, 75);
            panel2.TabIndex = 1;
            // 
            // DarkStart
            // 
            DarkStart.CustomFormat = "HH:mm:ss";
            DarkStart.Format = DateTimePickerFormat.Time;
            DarkStart.Location = new Point(519, 28);
            DarkStart.Name = "DarkStart";
            DarkStart.ShowUpDown = true;
            DarkStart.Size = new Size(138, 31);
            DarkStart.TabIndex = 1;
            DarkStart.ValueChanged += DarkStart_ValueChanged;
            // 
            // DarkModeTime
            // 
            DarkModeTime.AutoSize = true;
            DarkModeTime.Location = new Point(48, 35);
            DarkModeTime.Name = "DarkModeTime";
            DarkModeTime.Size = new Size(118, 24);
            DarkModeTime.TabIndex = 0;
            DarkModeTime.Text = "深色开始时间";
            // 
            // panel1
            // 
            panel1.Controls.Add(LightStart);
            panel1.Controls.Add(LightModeTime);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 82);
            panel1.TabIndex = 0;
            // 
            // LightStart
            // 
            LightStart.CalendarForeColor = Color.Black;
            LightStart.CalendarMonthBackground = Color.Black;
            LightStart.CalendarTitleBackColor = Color.Black;
            LightStart.CalendarTitleForeColor = Color.Black;
            LightStart.CalendarTrailingForeColor = Color.Black;
            LightStart.CustomFormat = "HH:mm:ss";
            LightStart.Format = DateTimePickerFormat.Time;
            LightStart.Location = new Point(519, 27);
            LightStart.Name = "LightStart";
            LightStart.ShowUpDown = true;
            LightStart.Size = new Size(138, 31);
            LightStart.TabIndex = 1;
            LightStart.ValueChanged += LightStart_ValueChanged;
            // 
            // LightModeTime
            // 
            LightModeTime.AutoSize = true;
            LightModeTime.Location = new Point(48, 27);
            LightModeTime.Name = "LightModeTime";
            LightModeTime.Size = new Size(118, 24);
            LightModeTime.TabIndex = 0;
            LightModeTime.Text = "浅色开始时间";
            // 
            // SettingGroup
            // 
            SettingGroup.BackColor = Color.Transparent;
            SettingGroup.Controls.Add(panel3);
            SettingGroup.Controls.Add(EnableWindowTopMost);
            SettingGroup.Controls.Add(AutoStart);
            SettingGroup.Dock = DockStyle.Top;
            SettingGroup.Font = new Font("微软雅黑", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            SettingGroup.ForeColor = Color.Black;
            SettingGroup.Location = new Point(0, 187);
            SettingGroup.Name = "SettingGroup";
            SettingGroup.Size = new Size(800, 125);
            SettingGroup.TabIndex = 1;
            SettingGroup.TabStop = false;
            SettingGroup.Text = "设置";
            // 
            // panel3
            // 
            panel3.Controls.Add(ForceDark);
            panel3.Controls.Add(ForceLight);
            panel3.Location = new Point(279, 26);
            panel3.Name = "panel3";
            panel3.Size = new Size(425, 72);
            panel3.TabIndex = 1;
            // 
            // ForceDark
            // 
            ForceDark.AutoSize = true;
            ForceDark.Location = new Point(243, 25);
            ForceDark.Name = "ForceDark";
            ForceDark.Size = new Size(103, 28);
            ForceDark.TabIndex = 0;
            ForceDark.TabStop = true;
            ForceDark.Tag = "1";
            ForceDark.Text = "强制深色";
            ForceDark.UseVisualStyleBackColor = true;
            ForceDark.CheckedChanged += ForceChangeMode;
            // 
            // ForceLight
            // 
            ForceLight.AutoSize = true;
            ForceLight.BackColor = Color.Transparent;
            ForceLight.Location = new Point(77, 25);
            ForceLight.Name = "ForceLight";
            ForceLight.Size = new Size(103, 28);
            ForceLight.TabIndex = 0;
            ForceLight.TabStop = true;
            ForceLight.Tag = "0";
            ForceLight.Text = "强制浅色";
            ForceLight.UseVisualStyleBackColor = false;
            ForceLight.CheckedChanged += ForceChangeMode;
            // 
            // EnableWindowTopMost
            // 
            EnableWindowTopMost.AutoSize = true;
            EnableWindowTopMost.BackColor = Color.Transparent;
            EnableWindowTopMost.FlatAppearance.BorderColor = Color.Lime;
            EnableWindowTopMost.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 255, 192);
            EnableWindowTopMost.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 128);
            EnableWindowTopMost.ForeColor = Color.Black;
            EnableWindowTopMost.Location = new Point(96, 64);
            EnableWindowTopMost.Name = "EnableWindowTopMost";
            EnableWindowTopMost.Size = new Size(140, 28);
            EnableWindowTopMost.TabIndex = 0;
            EnableWindowTopMost.Tag = "4";
            EnableWindowTopMost.Text = "启用窗口置顶";
            toolTip.SetToolTip(EnableWindowTopMost, "启用该选项允许双击状态栏图标后设置需要置顶的窗口。");
            EnableWindowTopMost.UseVisualStyleBackColor = false;
            EnableWindowTopMost.CheckedChanged += EnableWindowTopMost_CheckedChanged;
            // 
            // AutoStart
            // 
            AutoStart.AutoSize = true;
            AutoStart.BackColor = Color.Transparent;
            AutoStart.FlatAppearance.BorderColor = Color.Lime;
            AutoStart.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 255, 192);
            AutoStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 128);
            AutoStart.ForeColor = Color.Black;
            AutoStart.Location = new Point(96, 30);
            AutoStart.Name = "AutoStart";
            AutoStart.Size = new Size(122, 28);
            AutoStart.TabIndex = 0;
            AutoStart.Tag = "4";
            AutoStart.Text = "开机自启动";
            AutoStart.UseVisualStyleBackColor = false;
            AutoStart.CheckedChanged += EnableAutoStart;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(ConfirmUpdate);
            groupBox1.Controls.Add(AboutGitee);
            groupBox1.Controls.Add(AboutGithub);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("微软雅黑", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.ForeColor = Color.Black;
            groupBox1.Location = new Point(0, 312);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 138);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "关于";
            // 
            // ConfirmUpdate
            // 
            ConfirmUpdate.BackColor = Color.White;
            ConfirmUpdate.Location = new Point(583, 46);
            ConfirmUpdate.Name = "ConfirmUpdate";
            ConfirmUpdate.Size = new Size(122, 37);
            ConfirmUpdate.TabIndex = 1;
            ConfirmUpdate.Text = "确认保存";
            ConfirmUpdate.UseVisualStyleBackColor = false;
            ConfirmUpdate.Click += ConfirmUpdate_Click;
            // 
            // AboutGitee
            // 
            AboutGitee.Controls.Add(GiteeLink);
            AboutGitee.Controls.Add(pictureBox2);
            AboutGitee.Location = new Point(371, 27);
            AboutGitee.Name = "AboutGitee";
            AboutGitee.Size = new Size(168, 84);
            AboutGitee.TabIndex = 0;
            // 
            // GiteeLink
            // 
            GiteeLink.AutoSize = true;
            GiteeLink.Cursor = Cursors.Hand;
            GiteeLink.Location = new Point(83, 29);
            GiteeLink.Name = "GiteeLink";
            GiteeLink.Size = new Size(55, 24);
            GiteeLink.TabIndex = 1;
            GiteeLink.Text = "Gitee";
            GiteeLink.Click += GiteeLink_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.gitee;
            pictureBox2.Location = new Point(31, 19);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(46, 46);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // AboutGithub
            // 
            AboutGithub.Controls.Add(GitHubLInk);
            AboutGithub.Controls.Add(pictureBox1);
            AboutGithub.Location = new Point(96, 27);
            AboutGithub.Name = "AboutGithub";
            AboutGithub.Size = new Size(168, 84);
            AboutGithub.TabIndex = 0;
            // 
            // GitHubLInk
            // 
            GitHubLInk.AutoSize = true;
            GitHubLInk.Cursor = Cursors.Hand;
            GitHubLInk.Location = new Point(79, 29);
            GitHubLInk.Name = "GitHubLInk";
            GitHubLInk.Size = new Size(72, 24);
            GitHubLInk.TabIndex = 1;
            GitHubLInk.Text = "GitHub";
            GitHubLInk.Click += GitHubLInk_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.github;
            pictureBox1.Location = new Point(17, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(46, 46);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(20, 20);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { MainFormStripMenuItem, CancelAllTopWindowsMenuItem, CloseStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(199, 76);
            // 
            // MainFormStripMenuItem
            // 
            MainFormStripMenuItem.Name = "MainFormStripMenuItem";
            MainFormStripMenuItem.Size = new Size(198, 24);
            MainFormStripMenuItem.Text = "主页面";
            MainFormStripMenuItem.Click += MainFormStripMenuItem_Click;
            // 
            // CancelAllTopWindowsMenuItem
            // 
            CancelAllTopWindowsMenuItem.Name = "CancelAllTopWindowsMenuItem";
            CancelAllTopWindowsMenuItem.Size = new Size(198, 24);
            CancelAllTopWindowsMenuItem.Text = "清除所有置顶窗口";
            CancelAllTopWindowsMenuItem.Click += CancelAllTopMenuItem_Click;
            // 
            // CloseStripMenuItem
            // 
            CloseStripMenuItem.Name = "CloseStripMenuItem";
            CloseStripMenuItem.Size = new Size(198, 24);
            CloseStripMenuItem.Text = "退出";
            CloseStripMenuItem.Click += CloseStripMenuItem_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "AutoDarkModeMin";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            notifyIcon.MouseMove += notifyIcon_MouseMove;
            // 
            // toolTip
            // 
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "提示";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(SettingGroup);
            Controls.Add(TimePickerGroup);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AutoDarkMin";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            TimePickerGroup.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            SettingGroup.ResumeLayout(false);
            SettingGroup.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            groupBox1.ResumeLayout(false);
            AboutGitee.ResumeLayout(false);
            AboutGitee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            AboutGithub.ResumeLayout(false);
            AboutGithub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox TimePickerGroup;
        private GroupBox SettingGroup;
        private Panel panel2;
        private DateTimePicker DarkStart;
        private Label DarkModeTime;
        private Panel panel1;
        private DateTimePicker LightStart;
        private Label LightModeTime;
        private CheckBox AutoStart;
        private Panel panel3;
        private GroupBox groupBox1;
        private Panel AboutGitee;
        private Label GiteeLink;
        private PictureBox pictureBox2;
        private Panel AboutGithub;
        private Label GitHubLInk;
        private PictureBox pictureBox1;
        private Button ConfirmUpdate;
        private RadioButton ForceDark;
        private RadioButton ForceLight;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem MainFormStripMenuItem;
        private ToolStripMenuItem CloseStripMenuItem;
        private NotifyIcon notifyIcon;
        private CheckBox EnableWindowTopMost;
        private ToolTip toolTip;
        private ToolStripMenuItem CancelAllTopWindowsMenuItem;
    }
}
