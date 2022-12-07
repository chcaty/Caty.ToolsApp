namespace Caty.Tools.UxForm.Controls
{
    partial class UxProcessLine
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
            this.uxProcessLine1 = new Caty.Tools.UxForm.Controls.UxProcessLineBase();
            this.SuspendLayout();
            // 
            // ucProcessLine1
            // 
            this.uxProcessLine1.Anchor =
                ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                                                        System.Windows.Forms.AnchorStyles.Bottom)
                                                       | System.Windows.Forms.AnchorStyles.Left)
                                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.uxProcessLine1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))),
                ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.uxProcessLine1.Font = new System.Drawing.Font("Arial Unicode MS", 10F);
            this.uxProcessLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))),
                ((int)(((byte)(59)))));
            this.uxProcessLine1.Location = new System.Drawing.Point(18, 33);
            this.uxProcessLine1.MaxValue = 100;
            this.uxProcessLine1.Name = "ucProcessLine1";
            this.uxProcessLine1.Size = new System.Drawing.Size(399, 16);
            this.uxProcessLine1.TabIndex = 0;
            this.uxProcessLine1.Text = "ucProcessLine1";
            this.uxProcessLine1.Value = 0;
            this.uxProcessLine1.ValueBackGroundColor = System.Drawing.Color.White;
            this.uxProcessLine1.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))),
                ((int)(((byte)(119)))), ((int)(((byte)(232)))));
            this.uxProcessLine1.ValueTextType = ValueTextType.None;
            // 
            // UxProcessLine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.uxProcessLine1);
            this.Name = "UxProcessLine";
            this.Size = new System.Drawing.Size(434, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private UxProcessLineBase uxProcessLine1;
    }
}
