namespace Caty.Tools.UxForm.Controls
{
    partial class UxDataGridViewTreeRow
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
            this.panCells = new System.Windows.Forms.TableLayoutPanel();
            this.panLeft = new System.Windows.Forms.Panel();
            this.panMain = new System.Windows.Forms.Panel();
            this.uxSplitLineH1 = new Caty.Tools.UxForm.Controls.UxSplitLineH();
            this.uxSplitLineV1 = new Caty.Tools.UxForm.Controls.UxSplitLineV();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCells
            // 
            this.panCells.ColumnCount = 1;
            this.panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCells.Location = new System.Drawing.Point(24, 0);
            this.panCells.Name = "panCells";
            this.panCells.RowCount = 1;
            this.panCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.panCells.Size = new System.Drawing.Size(636, 64);
            this.panCells.TabIndex = 2;
            // 
            // panLeft
            // 
            this.panLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(0, 0);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(24, 64);
            this.panLeft.TabIndex = 0;
            this.panLeft.Tag = "0";
            this.panLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanLeft_MouseDown);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.panCells);
            this.panMain.Controls.Add(this.panLeft);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(1, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(660, 64);
            this.panMain.TabIndex = 0;
            // 
            // uxSplitLineH1
            // 
            this.uxSplitLineH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.uxSplitLineH1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSplitLineH1.Location = new System.Drawing.Point(1, 64);
            this.uxSplitLineH1.Name = "uxSplitLineH1";
            this.uxSplitLineH1.Size = new System.Drawing.Size(660, 1);
            this.uxSplitLineH1.TabIndex = 1;
            this.uxSplitLineH1.TabStop = false;
            // 
            // uxSplitLineV1
            // 
            this.uxSplitLineV1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.uxSplitLineV1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uxSplitLineV1.Location = new System.Drawing.Point(0, 0);
            this.uxSplitLineV1.Name = "uxSplitLineV1";
            this.uxSplitLineV1.Size = new System.Drawing.Size(1, 65);
            this.uxSplitLineV1.TabIndex = 0;
            this.uxSplitLineV1.TabStop = false;
            this.uxSplitLineV1.Visible = false;
            // 
            // UCDataGridViewTreeRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.uxSplitLineH1);
            this.Controls.Add(this.uxSplitLineV1);
            this.Name = "UCDataGridViewTreeRow";
            this.Size = new System.Drawing.Size(661, 65);
            this.panMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// The pan cells
        /// </summary>
        private System.Windows.Forms.TableLayoutPanel panCells;
        /// <summary>
        /// The uc split line h1
        /// </summary>
        private UxSplitLineH uxSplitLineH1;
        /// <summary>
        /// The pan left
        /// </summary>
        private System.Windows.Forms.Panel panLeft;
        /// <summary>
        /// The pan main
        /// </summary>
        private System.Windows.Forms.Panel panMain;
        private UxSplitLineV uxSplitLineV1;
    }
}
