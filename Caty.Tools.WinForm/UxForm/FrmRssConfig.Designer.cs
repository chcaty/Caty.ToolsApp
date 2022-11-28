namespace Caty.Tools.WinForm.UxForm
{
    partial class FrmRssConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.panel_middle = new System.Windows.Forms.Panel();
            this.lb_id = new System.Windows.Forms.Label();
            this.ck_IsEnable = new System.Windows.Forms.CheckBox();
            this.rtb_Url = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_bottom.SuspendLayout();
            this.panel_middle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.btn_close);
            this.panel_bottom.Controls.Add(this.btn_save);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 226);
            this.panel_bottom.Margin = new System.Windows.Forms.Padding(4);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(298, 42);
            this.panel_bottom.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close.Location = new System.Drawing.Point(145, 0);
            this.btn_close.Margin = new System.Windows.Forms.Padding(4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(153, 42);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_save.Location = new System.Drawing.Point(0, 0);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(145, 42);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // panel_middle
            // 
            this.panel_middle.Controls.Add(this.lb_id);
            this.panel_middle.Controls.Add(this.ck_IsEnable);
            this.panel_middle.Controls.Add(this.rtb_Url);
            this.panel_middle.Controls.Add(this.label2);
            this.panel_middle.Controls.Add(this.txt_name);
            this.panel_middle.Controls.Add(this.label1);
            this.panel_middle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_middle.Location = new System.Drawing.Point(0, 0);
            this.panel_middle.Margin = new System.Windows.Forms.Padding(4);
            this.panel_middle.Name = "panel_middle";
            this.panel_middle.Size = new System.Drawing.Size(298, 226);
            this.panel_middle.TabIndex = 2;
            // 
            // lb_id
            // 
            this.lb_id.AutoSize = true;
            this.lb_id.Location = new System.Drawing.Point(69, 7);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(24, 21);
            this.lb_id.TabIndex = 7;
            this.lb_id.Text = "id";
            this.lb_id.Visible = false;
            // 
            // ck_IsEnable
            // 
            this.ck_IsEnable.AutoSize = true;
            this.ck_IsEnable.Location = new System.Drawing.Point(69, 172);
            this.ck_IsEnable.Name = "ck_IsEnable";
            this.ck_IsEnable.Size = new System.Drawing.Size(93, 25);
            this.ck_IsEnable.TabIndex = 6;
            this.ck_IsEnable.Text = "是否启用";
            this.ck_IsEnable.UseVisualStyleBackColor = true;
            this.ck_IsEnable.CheckedChanged += new System.EventHandler(this.ck_IsEnable_CheckedChanged);
            // 
            // rtb_Url
            // 
            this.rtb_Url.Location = new System.Drawing.Point(69, 65);
            this.rtb_Url.Name = "rtb_Url";
            this.rtb_Url.Size = new System.Drawing.Size(210, 96);
            this.rtb_Url.TabIndex = 5;
            this.rtb_Url.Text = "";
            this.rtb_Url.TextChanged += new System.EventHandler(this.rtb_Url_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "链接";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(69, 31);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(210, 28);
            this.txt_name.TabIndex = 1;
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // FrmRssConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 268);
            this.Controls.Add(this.panel_middle);
            this.Controls.Add(this.panel_bottom);
            this.Margin = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.Name = "FrmRssConfig";
            this.Text = "RSS源配置";
            this.Load += new System.EventHandler(this.FrmRssConfig_Load);
            this.Controls.SetChildIndex(this.panel_bottom, 0);
            this.Controls.SetChildIndex(this.panel_middle, 0);
            this.panel_bottom.ResumeLayout(false);
            this.panel_middle.ResumeLayout(false);
            this.panel_middle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_bottom;
        private Button btn_save;
        private Panel panel_middle;
        private Button btn_close;
        private CheckBox ck_IsEnable;
        private RichTextBox rtb_Url;
        private Label label2;
        private TextBox txt_name;
        private Label label1;
        private Label lb_id;
    }
}