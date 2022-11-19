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
            this.SuspendLayout();
            // 
            // ck_enable
            // 
            this.ck_enable.AutoSize = true;
            this.ck_enable.Location = new System.Drawing.Point(6, 9);
            this.ck_enable.Name = "ck_enable";
            this.ck_enable.Size = new System.Drawing.Size(15, 14);
            this.ck_enable.TabIndex = 1;
            this.ck_enable.UseVisualStyleBackColor = true;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(27, 5);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(83, 23);
            this.txt_name.TabIndex = 2;
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(116, 5);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(231, 23);
            this.txt_url.TabIndex = 3;
            // 
            // txt_desc
            // 
            this.txt_desc.Location = new System.Drawing.Point(353, 5);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(146, 23);
            this.txt_desc.TabIndex = 4;
            // 
            // RssConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.ck_enable);
            this.Name = "RssConfigControl";
            this.Size = new System.Drawing.Size(505, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox ck_enable;
        private TextBox txt_name;
        private TextBox txt_url;
        private TextBox txt_desc;
    }
}
