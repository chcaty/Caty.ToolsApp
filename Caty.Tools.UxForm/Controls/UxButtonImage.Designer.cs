namespace Caty.Tools.UxForm.Controls
{
    sealed partial class UxButtonImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxButtonImage));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.ImageIndex = 0;
            this.lbl.ImageList = this.imageList1;
            this.lbl.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl.Text = "    自定义按钮";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "back.png");
            // 
            // UxButtonImage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.IsShowTips = true;
            this.Name = "UxButtonImage";
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.ImageList imageList1;
    }
}
