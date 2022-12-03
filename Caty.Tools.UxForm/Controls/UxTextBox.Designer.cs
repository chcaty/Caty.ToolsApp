namespace Caty.Tools.UxForm.Controls
{
    partial class UxTextBox
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(UxTextBox));
            this.txtInput = new Caty.Tools.UxForm.Controls.UxTextBoxBase();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.btnClear = new System.Windows.Forms.Panel();
            this.btnKeyBord = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // 
            // txtInput
            // 
            this.txtInput.Anchor =
                ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left |
                                                      System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.DecLength = 2;
            this.txtInput.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.InputType = Caty.Tools.UxForm.Helpers.TextInputType.NotControl;
            this.txtInput.Location = new System.Drawing.Point(8, 9);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtInput.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.txtInput.MinValue = new decimal(new int[] { 1000000, 0, 0, -2147483648 });
            this.txtInput.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.OldText = null;
            this.txtInput.PromptColor = System.Drawing.Color.Gray;
            this.txtInput.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.PromptText = "";
            this.txtInput.RegexPattern = "";
            this.txtInput.Size = new System.Drawing.Size(309, 24);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream =
                ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ic_cancel_black_24dp.png");
            this.imageList1.Images.SetKeyName(1, "ic_search_black_24dp.png");
            this.imageList1.Images.SetKeyName(2, "keyboard.png");
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::Caty.Tools.UxForm.Properties.Resources.input_clear;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.Location = new System.Drawing.Point(227, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Visible = false;
            this.btnClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClear_MouseDown);
            // 
            // btnKeybord
            // 
            this.btnKeyBord.BackgroundImage = global::Caty.Tools.UxForm.Properties.Resources.keyboard;
            this.btnKeyBord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnKeyBord.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnKeyBord.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnKeyBord.Location = new System.Drawing.Point(257, 5);
            this.btnKeyBord.Name = "btnKeyBord";
            this.btnKeyBord.Size = new System.Drawing.Size(30, 32);
            this.btnKeyBord.TabIndex = 6;
            this.btnKeyBord.Visible = false;
            this.btnKeyBord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnKeyBord_MouseDown);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::Caty.Tools.UxForm.Properties.Resources.ic_search_black_24dp;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Location = new System.Drawing.Point(287, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Visible = false;
            this.btnSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseDown);
            // 
            // UCTextBoxEx
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.CornerRadius = 5;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnKeyBord);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtInput);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.IsShowRect = true;
            this.IsRadius = true;
            this.Name = "UCTextBoxEx";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(322, 42);
            this.Load += new System.EventHandler(this.UCTextBoxEx_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UCTextBoxEx_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        public UxTextBoxBase txtInput;
        private System.Windows.Forms.Panel btnClear;
        private System.Windows.Forms.Panel btnSearch;
        private System.Windows.Forms.Panel btnKeyBord;

    }
}
