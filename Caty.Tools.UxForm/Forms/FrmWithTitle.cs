using System.ComponentModel;

namespace Caty.Tools.UxForm.Forms
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmWithTitle : FrmBase
    {
        [Description("窗体标题"), Category("自定义")]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        private bool _isShowCloseBtn;

        [Description("是否显示右上角关闭按钮"), Category("自定义")]
        public bool IsShowCloseBtn
        {
            get => _isShowCloseBtn;
            set
            {
                _isShowCloseBtn = value;
                btnClose.Visible = value;
                if (!value) return;
                btnClose.Location = new Point(Width - btnClose.Width - 10, 0);
                btnClose.BringToFront();
            }
        }

        public FrmWithTitle()
        {
            InitializeComponent();
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void FrmWithTitle_Shown(object sender, EventArgs e)
        {
            if (!IsShowCloseBtn) return;
            btnClose.Location = new Point(Width - btnClose.Width - 10, 0);
            btnClose.BringToFront();
        }
    }
}
