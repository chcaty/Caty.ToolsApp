namespace Caty.Tools.UxForm.Controls
{
    partial class UxControlBase
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(9f, 20f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.DoubleBuffered = true;
            this.Font = new Font("微软雅黑", 15f, FontStyle.Regular, GraphicsUnit.Pixel);
            base.Margin = new Padding(4, 5, 4, 5);
            base.Name = "UCBase";
            base.Size = new Size(237, 154);
            base.ResumeLayout(false);
        }

        #endregion
    }
}
