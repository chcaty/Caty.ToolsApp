using System.ComponentModel;

namespace Caty.Tools.UxForm.Forms
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmWithOKCancel2 : FrmWithTitle
    {
        public FrmWithOKCancel2()
        {
            InitializeComponent();
        }

        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            DoEnter();
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DoEsc();
        }

        protected override void DoEnter()
        {
            DialogResult = DialogResult.OK;
        }
    }
}
