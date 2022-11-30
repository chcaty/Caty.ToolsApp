namespace Caty.Tools.UxForm.Controls
{
    partial class UxButtonBase
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxButtonBase));
            this.lbl = new System.Windows.Forms.Label();
            this.lblTips = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(184, 60);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "自定义按钮";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDown);
            // 
            // lblTips
            // 
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTips.BackColor = System.Drawing.Color.Transparent;
            this.lblTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTips.ForeColor = System.Drawing.Color.White;
            this.lblTips.ImageIndex = 0;
            this.lblTips.ImageList = this.imageList1;
            this.lblTips.Location = new System.Drawing.Point(158, 0);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(24, 24);
            this.lblTips.TabIndex = 1;
            this.lblTips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTips.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tips.png");
            // 
            // UxButtonBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.lbl);
            this.CornerRadius = 5;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UxButtonBase";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.Size = new System.Drawing.Size(184, 60);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.ImageList imageList1;
    }
}
