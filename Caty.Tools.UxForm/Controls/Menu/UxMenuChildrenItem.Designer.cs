namespace Caty.Tools.UxForm.Controls.Menu
{
    partial class UxMenuChildrenItem
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
            this.uxSplitLineH1 = new Caty.Tools.UxForm.Controls.UxSplitLineH();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxSplitLineH1
            // 
            this.uxSplitLineH1.BackColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(43)))), ((int)(((byte)(54)))));
            this.uxSplitLineH1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSplitLineH1.Location = new System.Drawing.Point(0, 59);
            this.uxSplitLineH1.Name = "uxSplitLineH1";
            this.uxSplitLineH1.Size = new System.Drawing.Size(200, 1);
            this.uxSplitLineH1.TabIndex = 0;
            this.uxSplitLineH1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(152)))),
                ((int)(((byte)(170)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 59);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "子项";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCMenuChildrenItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.uxSplitLineH1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "UCMenuChildrenItem";
            this.Size = new System.Drawing.Size(200, 60);
            this.ResumeLayout(false);
        }

        #endregion

        private UxSplitLineH uxSplitLineH1;
        private System.Windows.Forms.Label lblTitle;
    }
}
