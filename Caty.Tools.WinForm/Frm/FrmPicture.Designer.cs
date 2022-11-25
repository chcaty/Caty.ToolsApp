namespace Caty.Tools.WinForm.Frm
{
    partial class FrmPicture
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
            this.pic_view = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_view)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_view
            // 
            this.pic_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_view.Location = new System.Drawing.Point(0, 0);
            this.pic_view.Name = "pic_view";
            this.pic_view.Size = new System.Drawing.Size(545, 710);
            this.pic_view.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_view.TabIndex = 1;
            this.pic_view.TabStop = false;
            // 
            // FrmPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 710);
            this.Controls.Add(this.pic_view);
            this.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.Name = "FrmPicture";
            this.Text = "图片预览";
            this.Load += new System.EventHandler(this.FrmPicture_Load);
            this.Controls.SetChildIndex(this.pic_view, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pic_view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pic_view;
    }
}