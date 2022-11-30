namespace Caty.Tools.WinForm.UxControl
{
    partial class Line
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
            this.lb_line = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_line
            // 
            this.lb_line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_line.Location = new System.Drawing.Point(0, 0);
            this.lb_line.Name = "lb_line";
            this.lb_line.Size = new System.Drawing.Size(150, 2);
            this.lb_line.TabIndex = 0;
            // 
            // Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_line);
            this.Name = "Line";
            this.Size = new System.Drawing.Size(150, 2);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lb_line;
    }
}
