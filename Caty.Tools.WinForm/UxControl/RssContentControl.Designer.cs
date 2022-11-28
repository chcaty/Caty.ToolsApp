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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_pubDate = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtb_desc = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(344, 39);
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
            this.txt_name.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_name.Size = new System.Drawing.Size(344, 39);
            this.txt_name.TabIndex = 0;
            this.txt_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_name.Click += new System.EventHandler(this.RssContentControl_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 3);
            this.label1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.txt_pubDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 24);
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
            this.txt_pubDate.Size = new System.Drawing.Size(344, 24);
            this.txt_pubDate.TabIndex = 0;
            this.txt_pubDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.rtb_desc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel3.Location = new System.Drawing.Point(0, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(344, 78);
            this.panel3.TabIndex = 5;
            // 
            // rtb_desc
            // 
            this.rtb_desc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_desc.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtb_desc.Location = new System.Drawing.Point(0, 0);
            this.rtb_desc.Name = "rtb_desc";
            this.rtb_desc.ReadOnly = true;
            this.rtb_desc.Size = new System.Drawing.Size(344, 78);
            this.rtb_desc.TabIndex = 0;
            this.rtb_desc.Text = "";
            // 
            // RssContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "RssContentControl";
            this.Size = new System.Drawing.Size(344, 144);
            this.Load += new System.EventHandler(this.RssContentControl_Load);
            this.Click += new System.EventHandler(this.RssContentControl_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox txt_name;
        private Panel panel2;
        private TextBox txt_pubDate;
        private Panel panel3;
        private RichTextBox rtb_desc;
    }
}
