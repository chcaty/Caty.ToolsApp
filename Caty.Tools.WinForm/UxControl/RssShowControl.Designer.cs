namespace Caty.Tools.WinForm.UxControl
{
    partial class RssShowControl
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
            this.panel_left = new System.Windows.Forms.Panel();
            this.spc_content = new System.Windows.Forms.SplitContainer();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.panel_button = new System.Windows.Forms.Panel();
            this.panel_source = new System.Windows.Forms.Panel();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_read = new System.Windows.Forms.Button();
            this.panel_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_content)).BeginInit();
            this.spc_content.Panel2.SuspendLayout();
            this.spc_content.SuspendLayout();
            this.panel_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.panel_source);
            this.panel_left.Controls.Add(this.panel_button);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(150, 721);
            this.panel_left.TabIndex = 0;
            // 
            // spc_content
            // 
            this.spc_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_content.Location = new System.Drawing.Point(150, 0);
            this.spc_content.Name = "spc_content";
            // 
            // spc_content.Panel2
            // 
            this.spc_content.Panel2.Controls.Add(this.chromiumWebBrowser1);
            this.spc_content.Size = new System.Drawing.Size(833, 721);
            this.spc_content.SplitterDistance = 277;
            this.spc_content.TabIndex = 1;
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(552, 721);
            this.chromiumWebBrowser1.TabIndex = 0;
            this.chromiumWebBrowser1.Text = "chromiumWebBrowser1";
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.btn_read);
            this.panel_button.Controls.Add(this.btn_edit);
            this.panel_button.Controls.Add(this.btn_add);
            this.panel_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_button.Location = new System.Drawing.Point(0, 671);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(150, 50);
            this.panel_button.TabIndex = 0;
            // 
            // panel_source
            // 
            this.panel_source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_source.Location = new System.Drawing.Point(0, 0);
            this.panel_source.Name = "panel_source";
            this.panel_source.Size = new System.Drawing.Size(150, 671);
            this.panel_source.TabIndex = 0;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.Transparent;
            this.btn_add.BackgroundImage = global::Caty.Tools.WinForm.Properties.Resources.increase;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.Location = new System.Drawing.Point(0, 0);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(50, 50);
            this.btn_add.TabIndex = 0;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.Transparent;
            this.btn_edit.BackgroundImage = global::Caty.Tools.WinForm.Properties.Resources.edit;
            this.btn_edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_edit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_edit.FlatAppearance.BorderSize = 0;
            this.btn_edit.Location = new System.Drawing.Point(50, 0);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(50, 50);
            this.btn_edit.TabIndex = 1;
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_read
            // 
            this.btn_read.BackColor = System.Drawing.Color.Transparent;
            this.btn_read.BackgroundImage = global::Caty.Tools.WinForm.Properties.Resources.selection;
            this.btn_read.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_read.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_read.FlatAppearance.BorderSize = 0;
            this.btn_read.Location = new System.Drawing.Point(100, 0);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(50, 50);
            this.btn_read.TabIndex = 2;
            this.btn_read.UseVisualStyleBackColor = false;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // RssShowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spc_content);
            this.Controls.Add(this.panel_left);
            this.Name = "RssShowControl";
            this.Size = new System.Drawing.Size(983, 721);
            this.panel_left.ResumeLayout(false);
            this.spc_content.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_content)).EndInit();
            this.spc_content.ResumeLayout(false);
            this.panel_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_left;
        private SplitContainer spc_content;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private Panel panel_source;
        private Panel panel_button;
        private Button btn_read;
        private Button btn_edit;
        private Button btn_add;
    }
}
