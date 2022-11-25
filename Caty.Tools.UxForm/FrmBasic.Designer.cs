namespace Caty.Tools.UxForm
{
    partial class FrmBasic
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_basic = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_basic
            // 
            this.panel_basic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_basic.Location = new System.Drawing.Point(0, 0);
            this.panel_basic.Margin = new System.Windows.Forms.Padding(4);
            this.panel_basic.Name = "panel_basic";
            this.panel_basic.Size = new System.Drawing.Size(622, 628);
            this.panel_basic.TabIndex = 0;
            // 
            // FrmBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(622, 628);
            this.Controls.Add(this.panel_basic);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBasic";
            this.Text = "FrmBasic";
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_basic;
    }
}