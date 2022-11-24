namespace Caty.Tools.WinForm
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.notifyIcon_main = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.item_main = new System.Windows.Forms.ToolStripMenuItem();
            this.item_bing = new System.Windows.Forms.ToolStripMenuItem();
            this.item_close = new System.Windows.Forms.ToolStripMenuItem();
            this.spc_source = new System.Windows.Forms.SplitContainer();
            this.panel_source = new System.Windows.Forms.Panel();
            this.panel_config = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_read = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_rand = new System.Windows.Forms.Button();
            this.btn_moyu = new System.Windows.Forms.Button();
            this.spc_content = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip_notify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_source)).BeginInit();
            this.spc_source.Panel1.SuspendLayout();
            this.spc_source.Panel2.SuspendLayout();
            this.spc_source.SuspendLayout();
            this.panel_config.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_content)).BeginInit();
            this.spc_content.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon_main
            // 
            this.notifyIcon_main.ContextMenuStrip = this.contextMenuStrip_notify;
            this.notifyIcon_main.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_main.Icon")));
            this.notifyIcon_main.Text = "工作姬";
            this.notifyIcon_main.Visible = true;
            this.notifyIcon_main.Click += new System.EventHandler(this.notifyIcon_main_Click);
            this.notifyIcon_main.DoubleClick += new System.EventHandler(this.notifyIcon_main_DoubleClick);
            this.notifyIcon_main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_main_MouseClick);
            // 
            // contextMenuStrip_notify
            // 
            this.contextMenuStrip_notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_main,
            this.item_bing,
            this.item_close});
            this.contextMenuStrip_notify.Name = "contextMenuStrip_notify";
            this.contextMenuStrip_notify.Size = new System.Drawing.Size(137, 70);
            // 
            // item_main
            // 
            this.item_main.Name = "item_main";
            this.item_main.Size = new System.Drawing.Size(136, 22);
            this.item_main.Text = "显示主窗体";
            this.item_main.Click += new System.EventHandler(this.item_main_Click);
            // 
            // item_bing
            // 
            this.item_bing.Name = "item_bing";
            this.item_bing.Size = new System.Drawing.Size(136, 22);
            this.item_bing.Text = "随机换壁纸";
            // 
            // item_close
            // 
            this.item_close.Name = "item_close";
            this.item_close.Size = new System.Drawing.Size(136, 22);
            this.item_close.Text = "退出";
            this.item_close.Click += new System.EventHandler(this.item_close_Click);
            // 
            // spc_source
            // 
            this.spc_source.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_source.Location = new System.Drawing.Point(0, 0);
            this.spc_source.Name = "spc_source";
            // 
            // spc_source.Panel1
            // 
            this.spc_source.Panel1.AutoScroll = true;
            this.spc_source.Panel1.Controls.Add(this.panel_source);
            this.spc_source.Panel1.Controls.Add(this.panel_config);
            // 
            // spc_source.Panel2
            // 
            this.spc_source.Panel2.Controls.Add(this.spc_content);
            this.spc_source.Size = new System.Drawing.Size(1184, 721);
            this.spc_source.SplitterDistance = 168;
            this.spc_source.TabIndex = 2;
            // 
            // panel_source
            // 
            this.panel_source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_source.Location = new System.Drawing.Point(0, 0);
            this.panel_source.Name = "panel_source";
            this.panel_source.Size = new System.Drawing.Size(166, 628);
            this.panel_source.TabIndex = 0;
            // 
            // panel_config
            // 
            this.panel_config.Controls.Add(this.panel1);
            this.panel_config.Controls.Add(this.btn_rand);
            this.panel_config.Controls.Add(this.btn_moyu);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_config.Location = new System.Drawing.Point(0, 628);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(166, 91);
            this.panel_config.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_read);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.btn_edit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 33);
            this.panel1.TabIndex = 0;
            // 
            // btn_read
            // 
            this.btn_read.BackColor = System.Drawing.SystemColors.Control;
            this.btn_read.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_read.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_read.Location = new System.Drawing.Point(113, 0);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(53, 33);
            this.btn_read.TabIndex = 2;
            this.btn_read.Text = "已读";
            this.btn_read.UseVisualStyleBackColor = false;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_add.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_add.Location = new System.Drawing.Point(56, 0);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(57, 33);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_edit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_edit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_edit.Location = new System.Drawing.Point(0, 0);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(56, 33);
            this.btn_edit.TabIndex = 0;
            this.btn_edit.Text = "编辑";
            this.btn_edit.UseVisualStyleBackColor = false;
            // 
            // btn_rand
            // 
            this.btn_rand.BackColor = System.Drawing.SystemColors.Control;
            this.btn_rand.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_rand.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_rand.Location = new System.Drawing.Point(0, 29);
            this.btn_rand.Name = "btn_rand";
            this.btn_rand.Size = new System.Drawing.Size(166, 29);
            this.btn_rand.TabIndex = 1;
            this.btn_rand.Text = "随机换壁纸";
            this.btn_rand.UseVisualStyleBackColor = false;
            this.btn_rand.Click += new System.EventHandler(this.btn_rand_Click);
            // 
            // btn_moyu
            // 
            this.btn_moyu.BackColor = System.Drawing.SystemColors.Control;
            this.btn_moyu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_moyu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_moyu.Location = new System.Drawing.Point(0, 0);
            this.btn_moyu.Name = "btn_moyu";
            this.btn_moyu.Size = new System.Drawing.Size(166, 29);
            this.btn_moyu.TabIndex = 0;
            this.btn_moyu.Text = "摸鱼日历";
            this.btn_moyu.UseVisualStyleBackColor = false;
            this.btn_moyu.Click += new System.EventHandler(this.btn_moyu_Click);
            // 
            // spc_content
            // 
            this.spc_content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spc_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_content.Location = new System.Drawing.Point(0, 0);
            this.spc_content.Name = "spc_content";
            // 
            // spc_content.Panel1
            // 
            this.spc_content.Panel1.AutoScroll = true;
            // 
            // spc_content.Panel2
            // 
            this.spc_content.Panel2.AutoScroll = true;
            this.spc_content.Size = new System.Drawing.Size(1012, 721);
            this.spc_content.SplitterDistance = 348;
            this.spc_content.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 721);
            this.Controls.Add(this.spc_source);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "FrmMain";
            this.Text = "工作姬";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Controls.SetChildIndex(this.spc_source, 0);
            this.contextMenuStrip_notify.ResumeLayout(false);
            this.spc_source.Panel1.ResumeLayout(false);
            this.spc_source.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_source)).EndInit();
            this.spc_source.ResumeLayout(false);
            this.panel_config.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_content)).EndInit();
            this.spc_content.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private NotifyIcon notifyIcon_main;
        private ContextMenuStrip contextMenuStrip_notify;
        private ToolStripMenuItem item_bing;
        private ToolStripMenuItem item_close;
        private ToolStripMenuItem item_main;
        private SplitContainer spc_source;
        private SplitContainer spc_content;
        private Panel panel_source;
        private Panel panel_config;
        private Button btn_rand;
        private Button btn_moyu;
        private Panel panel1;
        private Button btn_read;
        private Button btn_add;
        private Button btn_edit;
    }
}