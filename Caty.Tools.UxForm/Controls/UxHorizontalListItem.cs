using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls
{
    [ToolboxItem(false)]
    public partial class UxHorizontalListItem : UserControl
    {
        public event EventHandler SelectedItem;
        private KeyValuePair<string,string> _DataSource = new();

        public KeyValuePair<string, string> DataSource
        {
            get => _DataSource;
            set
            {
                _DataSource = value;
                var intWidth = ControlHelper.GetStringWidth(value.Value, lblTitle.CreateGraphics(), lblTitle.Font);
                if (intWidth < 50)
                {
                    intWidth = 50;
                }

                Width = intWidth + 20;
                lblTitle.Text = value.Value;
                SetSelect(false);
            }
        }

        public UxHorizontalListItem()
        {
            InitializeComponent();
            Dock = DockStyle.Right;
            MouseDown += Item_MouseDown;
            lblTitle.MouseDown += Item_MouseDown;
            uxSplitLineH1.MouseDown += Item_MouseDown;
        }

        void Item_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem(this, e);
            }
        }

        public void SetSelect(bool isSelect)
        {
            if (isSelect)
            {
                lblTitle.ForeColor = Color.FromArgb(255, 77, 59);
                uxSplitLineH1.Visible = true;
                lblTitle.Padding = new Padding(0, 0, 0, 5);
            }
            else
            {
                lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
                uxSplitLineH1.Visible = false;
                lblTitle.Padding = new Padding(0, 0, 0, 0);
            }
        }
    }
}
