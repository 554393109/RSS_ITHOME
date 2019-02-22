namespace APP
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_refresh = new System.Windows.Forms.Button();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pubDate_sub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Link_WEB = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Link_WAP = new System.Windows.Forms.DataGridViewLinkColumn();
            this.newsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_hide = new System.Windows.Forms.Button();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Exit_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_isShowTip = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_isWap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Exit_0 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.cms_notify.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(12, 27);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 0;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // dgv_list
            // 
            this.dgv_list.AllowUserToAddRows = false;
            this.dgv_list.AllowUserToDeleteRows = false;
            this.dgv_list.AllowUserToResizeRows = false;
            this.dgv_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.pubDate_sub,
            this.Link_WEB,
            this.Link_WAP,
            this.newsID});
            this.dgv_list.Location = new System.Drawing.Point(12, 56);
            this.dgv_list.MultiSelect = false;
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowTemplate.Height = 23;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(460, 355);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellDoubleClick);
            // 
            // Title
            // 
            this.Title.HeaderText = "标题";
            this.Title.MinimumWidth = 400;
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 400;
            // 
            // pubDate_sub
            // 
            this.pubDate_sub.HeaderText = "发布时间";
            this.pubDate_sub.MinimumWidth = 115;
            this.pubDate_sub.Name = "pubDate_sub";
            this.pubDate_sub.ReadOnly = true;
            this.pubDate_sub.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pubDate_sub.Width = 115;
            // 
            // Link_WEB
            // 
            this.Link_WEB.HeaderText = "WEB链接";
            this.Link_WEB.MinimumWidth = 100;
            this.Link_WEB.Name = "Link_WEB";
            this.Link_WEB.ReadOnly = true;
            // 
            // Link_WAP
            // 
            this.Link_WAP.HeaderText = "WAP链接";
            this.Link_WAP.MinimumWidth = 100;
            this.Link_WAP.Name = "Link_WAP";
            this.Link_WAP.ReadOnly = true;
            // 
            // newsID
            // 
            this.newsID.HeaderText = "文章Id";
            this.newsID.MinimumWidth = 65;
            this.newsID.Name = "newsID";
            this.newsID.ReadOnly = true;
            this.newsID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.newsID.Width = 65;
            // 
            // btn_hide
            // 
            this.btn_hide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_hide.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_hide.Location = new System.Drawing.Point(397, 27);
            this.btn_hide.Name = "btn_hide";
            this.btn_hide.Size = new System.Drawing.Size(75, 23);
            this.btn_hide.TabIndex = 0;
            this.btn_hide.Text = "Hide";
            this.btn_hide.UseVisualStyleBackColor = true;
            this.btn_hide.Click += new System.EventHandler(this.btn_hide_Click);
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.cms_notify;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Visible = true;
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // cms_notify
            // 
            this.cms_notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Exit_1});
            this.cms_notify.Name = "cms_notify";
            this.cms_notify.Size = new System.Drawing.Size(116, 26);
            // 
            // tsmi_Exit_1
            // 
            this.tsmi_Exit_1.Name = "tsmi_Exit_1";
            this.tsmi_Exit_1.Size = new System.Drawing.Size(180, 22);
            this.tsmi_Exit_1.Text = "退出(&e)";
            this.tsmi_Exit_1.Click += new System.EventHandler(this.tsmi_Exit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Setting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_Setting
            // 
            this.tsmi_Setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_isShowTip,
            this.tsmi_isWap,
            this.toolStripSeparator1,
            this.tsmi_Exit_0});
            this.tsmi_Setting.Name = "tsmi_Setting";
            this.tsmi_Setting.Size = new System.Drawing.Size(58, 21);
            this.tsmi_Setting.Text = "设置(&s)";
            // 
            // tsmi_isShowTip
            // 
            this.tsmi_isShowTip.Name = "tsmi_isShowTip";
            this.tsmi_isShowTip.Size = new System.Drawing.Size(180, 22);
            this.tsmi_isShowTip.Text = "更新提示";
            // 
            // tsmi_isWap
            // 
            this.tsmi_isWap.Name = "tsmi_isWap";
            this.tsmi_isWap.Size = new System.Drawing.Size(180, 22);
            this.tsmi_isWap.Text = "WAP版";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmi_Exit_0
            // 
            this.tsmi_Exit_0.Name = "tsmi_Exit_0";
            this.tsmi_Exit_0.Size = new System.Drawing.Size(180, 22);
            this.tsmi_Exit_0.Text = "退出(&e)";
            this.tsmi_Exit_0.Click += new System.EventHandler(this.tsmi_Exit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_hide;
            this.ClientSize = new System.Drawing.Size(484, 423);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dgv_list);
            this.Controls.Add(this.btn_hide);
            this.Controls.Add(this.btn_refresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 140);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.cms_notify.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.Button btn_hide;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip cms_notify;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Exit_1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_isShowTip;
        private System.Windows.Forms.ToolStripMenuItem tsmi_isWap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn pubDate_sub;
        private System.Windows.Forms.DataGridViewLinkColumn Link_WEB;
        private System.Windows.Forms.DataGridViewLinkColumn Link_WAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Exit_0;
    }
}

