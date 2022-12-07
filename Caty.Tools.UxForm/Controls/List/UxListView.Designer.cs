namespace Caty.Tools.UxForm.Controls.List
{
    partial class UxListView
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
            this.panMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panPage = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.AutoScroll = true;
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(5);
            this.panMain.Size = new System.Drawing.Size(462, 319);
            this.panMain.TabIndex = 1;
            this.panMain.Resize += new System.EventHandler(this.PanMain_Resize);
            // 
            // panPage
            // 
            this.panPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panPage.Location = new System.Drawing.Point(0, 319);
            this.panPage.Name = "panPage";
            this.panPage.Size = new System.Drawing.Size(462, 44);
            this.panPage.TabIndex = 0;
            this.panPage.Visible = false;
            // 
            // UCListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panPage);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCListView";
            this.Size = new System.Drawing.Size(462, 363);
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// The pan main
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel panMain;
        /// <summary>
        /// The pan page
        /// </summary>
        private System.Windows.Forms.Panel panPage;
    }
}
