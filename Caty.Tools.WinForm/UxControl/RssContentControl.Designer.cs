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
            this.txt_name = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_pubDate = new System.Windows.Forms.TextBox();
            this.line1 = new Caty.Tools.WinForm.UxControl.Line();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.txt_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 34);
            this.panel1.TabIndex = 0;
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_name.Location = new System.Drawing.Point(0, 0);
            this.txt_name.Multiline = true;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(344, 34);
            this.txt_name.TabIndex = 0;
            this.txt_name.Click += new System.EventHandler(this.RssContentControl_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.txt_pubDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 18);
            this.panel2.TabIndex = 4;
            // 
            // txt_pubDate
            // 
            this.txt_pubDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_pubDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_pubDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_pubDate.Location = new System.Drawing.Point(0, 0);
            this.txt_pubDate.Multiline = true;
            this.txt_pubDate.Name = "txt_pubDate";
            this.txt_pubDate.ReadOnly = true;
            this.txt_pubDate.Size = new System.Drawing.Size(344, 18);
            this.txt_pubDate.TabIndex = 0;
            // 
            // line1
            // 
            this.line1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.line1.Location = new System.Drawing.Point(0, 52);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(344, 2);
            this.line1.TabIndex = 5;
            // 
            // RssContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.line1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RssContentControl";
            this.Size = new System.Drawing.Size(344, 54);
            this.Load += new System.EventHandler(this.RssContentControl_Load);
            this.Click += new System.EventHandler(this.RssContentControl_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TextBox txt_name;
        private Panel panel2;
        private TextBox txt_pubDate;
        private Line line1;
    }
}
