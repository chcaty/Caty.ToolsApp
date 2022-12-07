using Caty.Tools.UxForm.Controls.TextBox;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    partial class UxComboBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtInput = new Caty.Tools.UxForm.Controls.TextBox.UxTextBoxBase();
            this.lblInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Caty.Tools.UxForm.Properties.Resources.ComboBox;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(136, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(37, 32);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.click_MouseDown);
            // 
            // txtInput
            // 
            this.txtInput.Anchor =
                ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left |
                                                      System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.BackColor = System.Drawing.Color.White;
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.DecLength = 2;
            this.txtInput.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.ForeColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtInput.InputType = TextInputType.NotControl;
            this.txtInput.Location = new System.Drawing.Point(3, 4);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtInput.MaxValue = new decimal(new int[]
            {
                1000000,
                0,
                0,
                0
            });
            this.txtInput.MinValue = new decimal(new int[]
            {
                1000000,
                0,
                0,
                -2147483648
            });
            this.txtInput.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.OldText = null;
            this.txtInput.PromptColor = System.Drawing.Color.Silver;
            this.txtInput.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.PromptText = "";
            this.txtInput.RegexPattern = "";
            this.txtInput.Size = new System.Drawing.Size(133, 24);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.ForeColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInput.Location = new System.Drawing.Point(3, 6);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(0, 20);
            this.lblInput.TabIndex = 2;
            this.lblInput.Visible = false;
            this.lblInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.click_MouseDown);
            // 
            // UxComboBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.CornerRadius = 5;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblInput);
            this.FillColor = System.Drawing.Color.Gainsboro;
            this.IsShowRect = true;
            this.Name = "UxComboBox";
            this.Size = new System.Drawing.Size(173, 32);
            this.Load += new System.EventHandler(this.UxComboBox_Load);
            this.SizeChanged += new System.EventHandler(this.UxComboBox_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.click_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UxTextBoxBase txtInput;
        private System.Windows.Forms.Label lblInput;
    }
}
