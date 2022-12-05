namespace Caty.Tools.UxForm.Forms
{
    public partial class FrmWaiting : FrmBase
    {
        public string Msg
        {
            get => label2.Text;
            set => label2.Text = value;
        }

        public FrmWaiting()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.ImageIndex == imageList1.Images.Count - 1)
            {
                label1.ImageIndex = 0;
            }
            else
            {
                label1.ImageIndex ++;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Opacity = 1.0;
            timer2.Enabled = false;
        }

        public void ShowForm(int sleepTime = 1)
        {
            Opacity = 0.0;
            if (sleepTime <= 0)
            {
                sleepTime = 1;
            }
            Show();
            timer2.Interval = sleepTime;
            timer2.Enabled = true;
        }
    }
}
