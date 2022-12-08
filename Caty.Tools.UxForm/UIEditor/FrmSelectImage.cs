﻿using Caty.Tools.UxForm.Helpers;
using Caty.Tools.UxForm.IconFont;

namespace Caty.Tools.UxForm.UIEditor
{
    public partial class FrmSelectImage : Form
    {
        public Image SelectImage { get; set; }
        public FrmSelectImage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), @"错误");
            }
        }

        private void tabControlExt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveControl = tabControlExt1.SelectedIndex == 0 ? flowLayoutPanel1 : flowLayoutPanel2;
        }

        private void FrmSelectImage_Load(object sender, EventArgs e)
        {
            var nameList = Enum.GetNames(typeof(FontIcons));
            var lst = nameList.ToList();
            lst.Sort();
            foreach (var item in lst)
            {
                var icon = (FontIcons)Enum.Parse(typeof(FontIcons), item);
                var lbl = new Label();
                lbl.AutoSize = false;
                lbl.Size = new Size(300, 35);
                lbl.ForeColor = Color.FromArgb(255, 77, 59);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Margin = new Padding(5);
                lbl.DoubleClick += lbl_DoubleClick;
                lbl.Text = @"       " + item;
                lbl.Image = FontImages.GetImage(icon, 32, Color.FromArgb(255, 77, 59));
                lbl.ImageAlign = ContentAlignment.MiddleLeft;
                lbl.Font = new Font("微软雅黑", 12);
                lbl.Tag = icon;
                if (item.StartsWith("A_"))
                {
                    flowLayoutPanel1.Controls.Add(lbl);
                }
                else
                {
                    flowLayoutPanel2.Controls.Add(lbl);
                }
            }
            ActiveControl = flowLayoutPanel1;
        }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            var icon = (FontIcons)lbl.Tag;
            var intSize = ucTextBoxEx1.InputText.ToInt();
            if (intSize <= 0)
                intSize = 32;
            SelectImage = FontImages.GetImage(icon, intSize, txtForeColor.BackColor, txtBackcolor.BackColor == Color.White ? Color.Empty : txtBackcolor.BackColor);
            DialogResult = DialogResult.OK;
            Close();
        }


        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            var ColorForm = new ColorDialog();
            ColorForm.AnyColor = true;
            var lstCustomColors = new List<int>();
            for (var i = 0; i < 16; i++)
            {
                lstCustomColors.Add(ColorTranslator.ToOle(ControlHelper.Colors[i]));
            }
            ColorForm.CustomColors = lstCustomColors.ToArray();
            if (ColorForm.ShowDialog() != DialogResult.OK) return;
            var getColor = ColorForm.Color;
            txt.BackColor = getColor;
            txt.Text = getColor.R + "," + getColor.G + "," + getColor.B;
        }
    }
}
