using Caty.Tools.UxForm.Forms;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new FrmWithTitle();
            frm.ShowDialog();
        }
    }
}