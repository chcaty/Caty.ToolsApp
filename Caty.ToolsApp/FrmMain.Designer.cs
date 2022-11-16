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
            this.gb_news = new System.Windows.Forms.GroupBox();
            this.tab_news = new System.Windows.Forms.TabControl();
            this.btn_moyu = new System.Windows.Forms.Button();
            this.btn_rssConfig = new System.Windows.Forms.Button();
            this.gb_news.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_news
            // 
            this.gb_news.BackColor = System.Drawing.SystemColors.Control;
            this.gb_news.Controls.Add(this.tab_news);
            this.gb_news.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_news.Location = new System.Drawing.Point(0, 0);
            this.gb_news.Name = "gb_news";
            this.gb_news.Size = new System.Drawing.Size(675, 415);
            this.gb_news.TabIndex = 0;
            this.gb_news.TabStop = false;
            this.gb_news.Text = "当前热点";
            // 
            // tab_news
            // 
            this.tab_news.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_news.Location = new System.Drawing.Point(3, 19);
            this.tab_news.Name = "tab_news";
            this.tab_news.SelectedIndex = 0;
            this.tab_news.Size = new System.Drawing.Size(669, 393);
            this.tab_news.TabIndex = 0;
            // 
            // btn_moyu
            // 
            this.btn_moyu.Location = new System.Drawing.Point(12, 421);
            this.btn_moyu.Name = "btn_moyu";
            this.btn_moyu.Size = new System.Drawing.Size(75, 23);
            this.btn_moyu.TabIndex = 1;
            this.btn_moyu.Text = "摸鱼日历";
            this.btn_moyu.UseVisualStyleBackColor = true;
            this.btn_moyu.Click += new System.EventHandler(this.btn_moyu_Click);
            // 
            // btn_rssConfig
            // 
            this.btn_rssConfig.Location = new System.Drawing.Point(93, 421);
            this.btn_rssConfig.Name = "btn_rssConfig";
            this.btn_rssConfig.Size = new System.Drawing.Size(75, 23);
            this.btn_rssConfig.TabIndex = 2;
            this.btn_rssConfig.Text = "RSS源配置";
            this.btn_rssConfig.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 454);
            this.Controls.Add(this.btn_rssConfig);
            this.Controls.Add(this.btn_moyu);
            this.Controls.Add(this.gb_news);
            this.Name = "FrmMain";
            this.Text = "工作姬";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gb_news.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox gb_news;
        private TabControl tab_news;
        private Button btn_moyu;
        private Button btn_rssConfig;
    }
}