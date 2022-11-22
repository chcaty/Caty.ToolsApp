namespace Caty.ToolsApp
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
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel_middle = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.contextMenuStrip_notify.SuspendLayout();
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
            // panel_left
            // 
            this.panel_left.AutoScroll = true;
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Margin = new System.Windows.Forms.Padding(4);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(200, 721);
            this.panel_left.TabIndex = 2;
            // 
            // panel_middle
            // 
            this.panel_middle.AutoScroll = true;
            this.panel_middle.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_middle.Location = new System.Drawing.Point(200, 0);
            this.panel_middle.Margin = new System.Windows.Forms.Padding(4);
            this.panel_middle.Name = "panel_middle";
            this.panel_middle.Size = new System.Drawing.Size(400, 721);
            this.panel_middle.TabIndex = 3;
            // 
            // panel_right
            // 
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_right.Location = new System.Drawing.Point(600, 0);
            this.panel_right.Margin = new System.Windows.Forms.Padding(4);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(584, 721);
            this.panel_right.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 721);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.panel_middle);
            this.Controls.Add(this.panel_left);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "FrmMain";
            this.Text = "工作姬";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Controls.SetChildIndex(this.panel_left, 0);
            this.Controls.SetChildIndex(this.panel_middle, 0);
            this.Controls.SetChildIndex(this.panel_right, 0);
            this.contextMenuStrip_notify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private NotifyIcon notifyIcon_main;
        private ContextMenuStrip contextMenuStrip_notify;
        private ToolStripMenuItem item_bing;
        private ToolStripMenuItem item_close;
        private ToolStripMenuItem item_main;
        private Panel panel_left;
        private Panel panel_middle;
        private Panel panel_right;
    }
}