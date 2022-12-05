namespace Caty.Tools.UxForm.Forms
{
    public partial class FrmDialog : FrmBase
    {
        private readonly bool _isEnterClose;
        public FrmDialog(string message, string title, bool isShowCancel = false, bool isShowClose = false, bool isEnterClose = true)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(title))
                lblTitle.Text = title;
            lblMsg.Text = message;
            if (isShowCancel)
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 1;
                tableLayoutPanel1.ColumnStyles[2].Width = 50;
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
            }

            btnClose.Visible = isShowClose;
            _isEnterClose = isEnterClose;
        }


        public static DialogResult ShowDialog(IWin32Window owner, string message, string title = "提示",
            bool isShowCancel = false, bool isShowMaskDialog = true, bool isShowClose = false, bool isEnterClose = true)
        {
            DialogResult result;
            if (owner is { } or Control { IsDisposed: true })
            {
                result = new FrmDialog(message,title,isShowCancel,isShowClose,isEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                }.ShowDialog();
            }
            else
            {
                if (owner is Control control)
                {
                    owner = control.FindForm();
                }

                result = new FrmDialog(message, title, isShowCancel, isShowClose, isEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                }.ShowDialog(owner);
            }
            return result;
        }

        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override void DoEnter()
        {
            if (_isEnterClose)
                DialogResult = DialogResult.OK;
        }

        private void FrmDialog_VisibleChanged(object sender, EventArgs e) { }
    }
}
