namespace Caty.Tools.UxForm.Controls.DataGridView
{
    partial class UxDataGridViewRow
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
            this.panCells = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // ucSplitLine_H1
            // 
            this.uxSplitLineH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.uxSplitLineH1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSplitLineH1.Location = new System.Drawing.Point(0, 55);
            this.uxSplitLineH1.Name = "uxSplitLineH1";
            this.uxSplitLineH1.Size = new System.Drawing.Size(661, 1);
            this.uxSplitLineH1.TabIndex = 0;
            this.uxSplitLineH1.TabStop = false;
            // 
            // panCells
            // 
            this.panCells.ColumnCount = 1;
            this.panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCells.Location = new System.Drawing.Point(0, 0);
            this.panCells.Name = "panCells";
            this.panCells.RowCount = 1;
            this.panCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panCells.Size = new System.Drawing.Size(661, 55);
            this.panCells.TabIndex = 1;
            // 
            // UCDataGridViewItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panCells);
            this.Controls.Add(this.uxSplitLineH1);
            this.Name = "UCDataGridViewItem";
            this.Size = new System.Drawing.Size(661, 56);
            this.ResumeLayout(false);
        }

        #endregion

        private UxSplitLineH uxSplitLineH1;
        private System.Windows.Forms.TableLayoutPanel panCells;
    }
}
