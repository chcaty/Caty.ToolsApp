﻿using Caty.Tools.UxForm.Forms;

namespace Caty.Tools.WinForm.UxForm;

public partial class FrmPicture : FrmDialog
{
    private readonly Image _image;
    public FrmPicture(Image image, string name)
    {
        InitializeComponent();
        _image = image;
        Text = name;
    }

    private void FrmPicture_Load(object sender, EventArgs e)
    {
        pic_view.Image = _image;
    }
}