namespace Caty.Tools.UxForm.Controls.DataGridView
{
    partial class UxDataGridView
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
            this.panHead = new System.Windows.Forms.Panel();
            this.panColumns = new System.Windows.Forms.TableLayoutPanel();
            this.uxSplitLineH1 = new Caty.Tools.UxForm.Controls.UxSplitLineH();
            this.panRow = new System.Windows.Forms.Panel();
            this.panHeadLeft = new System.Windows.Forms.Panel();
            this.panHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHead
            // 
            this.panHead.Controls.Add(this.panColumns);
            this.panHead.Controls.Add(this.uxSplitLineH1);
            this.panHead.Controls.Add(this.panHeadLeft);
            this.panHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHead.Location = new System.Drawing.Point(0, 0);
            this.panHead.Name = "panHead";
            this.panHead.Size = new System.Drawing.Size(1061, 40);
            this.panHead.TabIndex = 0;
            // 
            // panColumns
            // 
            this.panColumns.ColumnCount = 1;
            this.panColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panColumns.Location = new System.Drawing.Point(0, 0);
            this.panColumns.Name = "panColumns";
            this.panColumns.RowCount = 1;
            this.panColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panColumns.Size = new System.Drawing.Size(1061, 39);
            this.panColumns.TabIndex = 1;
            // 
            // ucSplitLine_H1
            // 
            this.uxSplitLineH1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.uxSplitLineH1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSplitLineH1.Location = new System.Drawing.Point(0, 39);
            this.uxSplitLineH1.Name = "ucSplitLine_H1";
            this.uxSplitLineH1.Size = new System.Drawing.Size(1061, 1);
            this.uxSplitLineH1.TabIndex = 0;
            this.uxSplitLineH1.TabStop = false;
            // 
            // panRow
            // 
            this.panRow.AutoScroll = true;
            this.panRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panRow.Location = new System.Drawing.Point(0, 40);
            this.panRow.Name = "panRow";
            this.panRow.Size = new System.Drawing.Size(1061, 475);
            this.panRow.TabIndex = 1;
            // 
            // panHeadLeft
            // 
            this.panHeadLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panHeadLeft.Location = new System.Drawing.Point(0, 0);
            this.panHeadLeft.Name = "panHeadLeft";
            this.panHeadLeft.Size = new System.Drawing.Size(0, 39);
            this.panHeadLeft.TabIndex = 2;
            // 
            // UCDataGridView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panRow);
            this.Controls.Add(this.panHead);
            this.Name = "UCDataGridView";
            this.Size = new System.Drawing.Size(1061, 565); 
            this.panHead.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panHead;
        private System.Windows.Forms.TableLayoutPanel panColumns;
        private UxSplitLineH uxSplitLineH1;
        private System.Windows.Forms.Panel panHeadLeft;
        private System.Windows.Forms.Panel panRow;
    }
}
