namespace Caty.Tools.UxForm.Controls
{
    partial class UxTimePanel
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
            this.panMain = new System.Windows.Forms.TableLayoutPanel();
            this.ucSplitLine_V1 = new Caty.Tools.UxForm.Controls.UxSplitLineV();
            this.ucSplitLine_V2 = new Caty.Tools.UxForm.Controls.UxSplitLineV();
            this.ucSplitLine_H1 = new Caty.Tools.UxForm.Controls.UxSplitLineH();
            this.ucSplitLine_H2 = new Caty.Tools.UxForm.Controls.UxSplitLineH();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.ColumnCount = 1;
            this.panMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(1, 1);
            this.panMain.Name = "panMain";
            this.panMain.RowCount = 1;
            this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panMain.Size = new System.Drawing.Size(99, 228);
            this.panMain.TabIndex = 0;
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))),
                ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(0, 0);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(1, 230);
            this.ucSplitLine_V1.TabIndex = 0;
            this.ucSplitLine_V1.TabStop = false;
            this.ucSplitLine_V1.Visible = false;
            // 
            // ucSplitLine_V2
            // 
            this.ucSplitLine_V2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))),
                ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_V2.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucSplitLine_V2.Location = new System.Drawing.Point(100, 0);
            this.ucSplitLine_V2.Name = "ucSplitLine_V2";
            this.ucSplitLine_V2.Size = new System.Drawing.Size(1, 230);
            this.ucSplitLine_V2.TabIndex = 1;
            this.ucSplitLine_V2.TabStop = false;
            this.ucSplitLine_V2.Visible = false;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))),
                ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(1, 0);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(99, 1);
            this.ucSplitLine_H1.TabIndex = 0;
            this.ucSplitLine_H1.TabStop = false;
            this.ucSplitLine_H1.Visible = false;
            // 
            // ucSplitLine_H2
            // 
            this.ucSplitLine_H2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))),
                ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_H2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_H2.Location = new System.Drawing.Point(1, 229);
            this.ucSplitLine_H2.Name = "ucSplitLine_H2";
            this.ucSplitLine_H2.Size = new System.Drawing.Size(99, 1);
            this.ucSplitLine_H2.TabIndex = 2;
            this.ucSplitLine_H2.TabStop = false;
            this.ucSplitLine_H2.Visible = false;
            // 
            // UxTimePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.ucSplitLine_H2);
            this.Controls.Add(this.ucSplitLine_H1);
            this.Controls.Add(this.ucSplitLine_V2);
            this.Controls.Add(this.ucSplitLine_V1);
            this.Name = "UxTimePanel";
            this.Size = new System.Drawing.Size(101, 230);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panMain;
        private UxSplitLineV ucSplitLine_V1;
        private UxSplitLineV ucSplitLine_V2;
        private UxSplitLineH ucSplitLine_H1;
        private UxSplitLineH ucSplitLine_H2;
    }
}
