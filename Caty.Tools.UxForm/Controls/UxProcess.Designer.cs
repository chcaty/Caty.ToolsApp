namespace Caty.Tools.UxForm.Controls
{
    partial class UxProcess
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(127)))), ((int)(((byte)(203)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 22);
            this.panel1.TabIndex = 0;
            // 
            // UxProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.CornerRadius = 5;
            this.Controls.Add(this.panel1);
            this.IsRadius = true;
            this.Name = "UxProcess";
            this.Size = new System.Drawing.Size(291, 22);
            this.SizeChanged += new System.EventHandler(this.UxProcess_SizeChanged);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}
