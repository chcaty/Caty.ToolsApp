namespace Caty.Tools.UxForm.Controls
{
    partial class UxDropDownButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxDropDownButton));
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl.ImageList = null;
            this.lbl.Text = "自定义按钮";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCDropDownBtn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BtnFont = new System.Drawing.Font("微软雅黑", 14F);
            this.BtnForeColor = System.Drawing.Color.White;
            this.ForeColor = System.Drawing.Color.White;
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UCDropDownBtn";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
