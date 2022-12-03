namespace Caty.Tools.UxForm.Controls.List
{
    partial class UxHorizontalListItem
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;

            this.lblTitle = new System.Windows.Forms.Label();
            this.uxSplitLineH1 = new UxSplitLineH();
            this.SuspendLayout();

            // 
            // lblTitle
            //
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new Font("微软雅黑", 10F);
            this.lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            this.lblTitle.Location = new System.Drawing.Point(1, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(118, 50);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "分类名称\r\n分类名称";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucSplitLine_H1
            // 
            this.uxSplitLineH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))),
                ((int)(((byte)(59)))));
            this.uxSplitLineH1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSplitLineH1.Location = new System.Drawing.Point(1, 50);
            this.uxSplitLineH1.Name = "uxSplitLineH1";
            this.uxSplitLineH1.Size = new System.Drawing.Size(118, 3);
            this.uxSplitLineH1.TabIndex = 0;
            this.uxSplitLineH1.TabStop = false;
            // 
            // UCHorizontalListItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.uxSplitLineH1);
            this.Name = "UxHorizontalListItem";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.Size = new System.Drawing.Size(120, 53);
            this.ResumeLayout(false);
        }

        #endregion

        private UxSplitLineH uxSplitLineH1;
        private System.Windows.Forms.Label lblTitle;
    }
}
