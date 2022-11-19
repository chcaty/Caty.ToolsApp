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
            this.btn_moyu = new System.Windows.Forms.Button();
            this.btn_rssConfig = new System.Windows.Forms.Button();
            this.lb_lastUpdateTime = new System.Windows.Forms.Label();
            this.btn_rand = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gb_news = new System.Windows.Forms.GroupBox();
            this.tab_news = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.gb_news.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_moyu
            // 
            this.btn_moyu.Location = new System.Drawing.Point(10, 6);
            this.btn_moyu.Name = "btn_moyu";
            this.btn_moyu.Size = new System.Drawing.Size(75, 23);
            this.btn_moyu.TabIndex = 1;
            this.btn_moyu.Text = "摸鱼日历";
            this.btn_moyu.UseVisualStyleBackColor = true;
            this.btn_moyu.Click += new System.EventHandler(this.btn_moyu_Click);
            // 
            // btn_rssConfig
            // 
            this.btn_rssConfig.Location = new System.Drawing.Point(91, 6);
            this.btn_rssConfig.Name = "btn_rssConfig";
            this.btn_rssConfig.Size = new System.Drawing.Size(75, 23);
            this.btn_rssConfig.TabIndex = 2;
            this.btn_rssConfig.Text = "RSS源配置";
            this.btn_rssConfig.UseVisualStyleBackColor = true;
            this.btn_rssConfig.Click += new System.EventHandler(this.btn_rssConfig_Click);
            // 
            // lb_lastUpdateTime
            // 
            this.lb_lastUpdateTime.AutoSize = true;
            this.lb_lastUpdateTime.Location = new System.Drawing.Point(324, 9);
            this.lb_lastUpdateTime.Name = "lb_lastUpdateTime";
            this.lb_lastUpdateTime.Size = new System.Drawing.Size(43, 17);
            this.lb_lastUpdateTime.TabIndex = 3;
            this.lb_lastUpdateTime.Text = "label1";
            // 
            // btn_rand
            // 
            this.btn_rand.Location = new System.Drawing.Point(172, 6);
            this.btn_rand.Name = "btn_rand";
            this.btn_rand.Size = new System.Drawing.Size(82, 23);
            this.btn_rand.TabIndex = 4;
            this.btn_rand.Text = "随机换壁纸";
            this.btn_rand.UseVisualStyleBackColor = true;
            this.btn_rand.Click += new System.EventHandler(this.btn_rand_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_lastUpdateTime);
            this.panel1.Controls.Add(this.btn_moyu);
            this.panel1.Controls.Add(this.btn_rssConfig);
            this.panel1.Controls.Add(this.btn_rand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 420);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 34);
            this.panel1.TabIndex = 5;
            // 
            // gb_news
            // 
            this.gb_news.BackColor = System.Drawing.SystemColors.Control;
            this.gb_news.Controls.Add(this.tab_news);
            this.gb_news.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_news.Location = new System.Drawing.Point(0, 0);
            this.gb_news.Name = "gb_news";
            this.gb_news.Size = new System.Drawing.Size(675, 420);
            this.gb_news.TabIndex = 6;
            this.gb_news.TabStop = false;
            this.gb_news.Text = "当前热点";
            // 
            // tab_news
            // 
            this.tab_news.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_news.Location = new System.Drawing.Point(3, 19);
            this.tab_news.Name = "tab_news";
            this.tab_news.SelectedIndex = 0;
            this.tab_news.Size = new System.Drawing.Size(669, 398);
            this.tab_news.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 454);
            this.Controls.Add(this.gb_news);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "工作姬";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gb_news.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button btn_moyu;
        private Button btn_rssConfig;
        private Label lb_lastUpdateTime;
        private Button btn_rand;
        private Panel panel1;
        private GroupBox gb_news;
        private TabControl tab_news;
    }
}