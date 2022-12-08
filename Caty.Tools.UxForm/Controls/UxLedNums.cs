using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxLedNums : UserControl
    {
        private string _value;

        [Description("值"), Category("自定义")]
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                ReloadValue();
            }
        }

        private int _lineWidth = 8;

        [Description("线宽度，为了更好的显示效果，请使用偶数"), Category("自定义")]
        public int LineWidth
        {
            get => _lineWidth;
            set
            {
                _lineWidth = value;
                foreach (UxLedNums c in Controls)
                {
                    c.LineWidth = value;
                }
            }
        }

        [Description("颜色"), Category("自定义")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                foreach (UxLedNums c in Controls)
                {
                    c.ForeColor = value;
                }
            }
        }

        public override RightToLeft RightToLeft
        {
            get => base.RightToLeft;
            set
            {
                base.RightToLeft = value;
                ReloadValue();
            }
        }

        private void ReloadValue()
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                Controls.Clear();
                foreach (var uc in _value.Select(item => new UxLedNums
                         {
                             Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left,
                             Value = item.ToString(),
                             ForeColor = ForeColor,
                             LineWidth = _lineWidth
                         }))
                {
                    Controls.Add(uc);
                    if (RightToLeft == RightToLeft.Yes)
                        uc.SendToBack();
                    else
                        uc.BringToFront();
                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }

        public UxLedNums()
        {
            InitializeComponent();
            Value = "0.00";
        }
    }
}
