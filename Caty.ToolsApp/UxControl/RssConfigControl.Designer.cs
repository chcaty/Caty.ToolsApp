namespace Caty.ToolsApp
{
    partial class RssConfigControl
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
            this.ck_enable = new System.Windows.Forms.CheckBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.txt_desc = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ck_enable
            // 
            this.ck_enable.AutoSize = true;
            this.ck_enable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ck_enable.Location = new System.Drawing.Point(0, 0);
            this.ck_enable.Name = "ck_enable";
            this.ck_enable.Size = new System.Drawing.Size(15, 25);
            this.ck_enable.TabIndex = 1;
            this.ck_enable.UseVisualStyleBackColor = true;
            this.ck_enable.CheckedChanged += new System.EventHandler(this.ck_enable_CheckedChanged);
            // 
            // txt_name
            // 
            this.txt_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_name.Location = new System.Drawing.Point(0, 0);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(89, 23);
            this.txt_name.TabIndex = 2;
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // txt_url
            // 
            this.txt_url.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_url.Location = new System.Drawing.Point(0, 0);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(237, 23);
            this.txt_url.TabIndex = 3;
            this.txt_url.TextChanged += new System.EventHandler(this.txt_url_TextChanged);
            // 
            // txt_desc
            // 
            this.txt_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_desc.Location = new System.Drawing.Point(0, 0);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(284, 23);
            this.txt_desc.TabIndex = 4;
            this.txt_desc.TextChanged += new System.EventHandler(this.txt_desc_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ck_enable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 25);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txt_name);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(15, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(89, 25);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_url);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(104, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(237, 25);
            this.panel3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txt_desc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(341, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(284, 25);
            this.panel4.TabIndex = 8;
            // 
            // RssConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "RssConfigControl";
            this.Size = new System.Drawing.Size(625, 25);
            this.Load += new System.EventHandler(this.RssConfigControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox ck_enable;
        private TextBox txt_name;
        private TextBox txt_url;
        private TextBox txt_desc;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}
