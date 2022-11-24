namespace Caty.Tools.WinForm.UxControl
{
    partial class RssContentControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_name = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_pubDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb_desc = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 39);
            this.panel1.TabIndex = 0;
            // 
            // lb_name
            // 
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.Location = new System.Drawing.Point(0, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(234, 39);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "label1";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_name.Click += new System.EventHandler(this.lb_name_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_pubDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 20);
            this.panel2.TabIndex = 1;
            // 
            // lb_pubDate
            // 
            this.lb_pubDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_pubDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_pubDate.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_pubDate.Location = new System.Drawing.Point(0, 0);
            this.lb_pubDate.Name = "lb_pubDate";
            this.lb_pubDate.Size = new System.Drawing.Size(234, 20);
            this.lb_pubDate.TabIndex = 0;
            this.lb_pubDate.Text = "label2";
            this.lb_pubDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_pubDate.Click += new System.EventHandler(this.lb_pubDate_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 2);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb_desc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel3.Location = new System.Drawing.Point(0, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 54);
            this.panel3.TabIndex = 4;
            // 
            // lb_desc
            // 
            this.lb_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_desc.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_desc.Location = new System.Drawing.Point(0, 0);
            this.lb_desc.Name = "lb_desc";
            this.lb_desc.Size = new System.Drawing.Size(234, 54);
            this.lb_desc.TabIndex = 0;
            this.lb_desc.Text = "label1";
            this.lb_desc.Click += new System.EventHandler(this.lb_desc_Click);
            // 
            // RssContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RssContentControl";
            this.Size = new System.Drawing.Size(234, 115);
            this.Load += new System.EventHandler(this.RssContentControl_Load);
            this.Click += new System.EventHandler(this.RssContentControl_Click);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lb_name;
        private Label lb_pubDate;
        private Label label1;
        private Panel panel3;
        private Label lb_desc;
    }
}
