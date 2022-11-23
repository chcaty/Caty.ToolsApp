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
            this.spc_source = new System.Windows.Forms.SplitContainer();
            this.spc_content = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip_notify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_source)).BeginInit();
            this.spc_source.Panel2.SuspendLayout();
            this.spc_source.SuspendLayout();
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
            // 
            // spc_source.Panel2
            // 
            this.spc_source.Panel2.Controls.Add(this.spc_content);
            this.spc_source.Size = new System.Drawing.Size(1184, 721);
            this.spc_source.SplitterDistance = 137;
            this.spc_source.TabIndex = 2;
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
            this.spc_content.Size = new System.Drawing.Size(1043, 721);
            this.spc_content.SplitterDistance = 359;
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
            this.spc_source.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_source)).EndInit();
            this.spc_source.ResumeLayout(false);
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
    }
}