namespace Caty.ToolsApp.UxControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_pubDate = new System.Windows.Forms.Label();
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
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 26);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_pubDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 32);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb_desc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(394, 83);
            this.panel3.TabIndex = 2;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.Location = new System.Drawing.Point(0, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(43, 17);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "label1";
            // 
            // lb_pubDate
            // 
            this.lb_pubDate.AutoSize = true;
            this.lb_pubDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_pubDate.Location = new System.Drawing.Point(0, 0);
            this.lb_pubDate.Name = "lb_pubDate";
            this.lb_pubDate.Size = new System.Drawing.Size(43, 17);
            this.lb_pubDate.TabIndex = 0;
            this.lb_pubDate.Text = "label2";
            // 
            // lb_desc
            // 
            this.lb_desc.AutoSize = true;
            this.lb_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_desc.Location = new System.Drawing.Point(0, 0);
            this.lb_desc.Name = "lb_desc";
            this.lb_desc.Size = new System.Drawing.Size(43, 17);
            this.lb_desc.TabIndex = 0;
            this.lb_desc.Text = "label3";
            // 
            // RssContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RssContentControl";
            this.Size = new System.Drawing.Size(394, 141);
            this.Load += new System.EventHandler(this.RssContentControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lb_name;
        private Label lb_pubDate;
        private Label lb_desc;
    }
}
