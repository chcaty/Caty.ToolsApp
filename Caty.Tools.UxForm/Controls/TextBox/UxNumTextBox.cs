using Caty.Tools.UxForm.Controls.KeyBoard;
using Caty.Tools.UxForm.Forms;
using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;
using System.Globalization;

namespace Caty.Tools.UxForm.Controls.TextBox
{
    [DefaultEvent("NumChanged")]
    public partial class UxNumTextBox : UserControl
    {
        [Description("弹出输入键盘时发生"), Category("自定义")]
        public event EventHandler? ShowKeyBorderEvent;

        [Description("关闭输入键盘时发生"), Category("自定义")]
        public event EventHandler? HideKeyBorderEvent;

        [Description("数字改变时发生"), Category("自定义")]
        public event EventHandler? NumChanged;

        [Description("输入类型"), Category("自定义")]
        public TextInputType InputType
        {
            get => txtNum.InputType;
            set
            {
                if (value == TextInputType.NotControl)
                {
                    return;
                }

                txtNum.InputType = value;
            }
        }

        [Description("数字是否可手动编辑"), Category("自定义")]
        public bool IsNumCanInput
        {
            get => txtNum.Enabled;
            set => txtNum.Enabled = value;
        }

        [Description("当InputType为数字类型时，能输入的最大值")]
        public decimal MaxValue
        {
            get => txtNum.MaxValue;
            set => txtNum.MaxValue = value;
        }

        [Description("当InputType为数字类型时，能输入的最小值")]
        public decimal MinValue
        {
            get => txtNum.MinValue;
            set => txtNum.MinValue = value;
        }

        [Description("键盘样式"), Category("自定义")]
        public KeyBoardType KeyBoardType { get; set; } = KeyBoardType.UxKeyBorderNum;

        [Description("数值"), Category("自定义")]
        public decimal Num
        {
            get => Convert.ToDecimal(txtNum.Text);
            set => txtNum.Text = value.ToString(CultureInfo.CurrentCulture);
        }

        [Description("字体"), Category("自定义")]
        public new Font Font
        {
            get => txtNum.Font;
            set => txtNum.Font = value;
        }

        [Description("增加按钮点击事件"), Category("自定义")]
        public event EventHandler? AddClick;

        [Description("减少按钮点击事件"), Category("自定义")]
        public event EventHandler? MinusClick;

        public UxNumTextBox()
        {
            InitializeComponent();
            txtNum.TextChanged += txtNum_TextChange;
        }

        private void txtNum_TextChange(object sender, EventArgs e)
        {
            NumChanged?.Invoke(txtNum.Text.ToString(), e);
        }

        private FrmAnchor frmAnchor;

        private void txtNum_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsNumCanInput) return;
            if (KeyBoardType == KeyBoardType.Null) return;
            switch (KeyBoardType)
            {
                case KeyBoardType.UxKeyBorderAll_EN:
                    var uxKeyBoardAll = new UxKeyBoardAll();
                    uxKeyBoardAll.RetractClick += (a, b) => { frmAnchor.Hide(); };
                    uxKeyBoardAll.EnterClick += (a, b) => { frmAnchor.Hide(); };
                    frmAnchor = new FrmAnchor(this, uxKeyBoardAll);
                    frmAnchor.VisibleChanged += frmAnchor_VisibleChanged;
                    frmAnchor.Show(this.FindForm());
                    break;
                case KeyBoardType.UxKeyBorderNum:
                    var uxKeyBoardNum = new UxKeyBoardNum();
                    uxKeyBoardNum.EnterClick += (a, b) => { frmAnchor.Hide(); };
                    frmAnchor = new FrmAnchor(this, uxKeyBoardNum);
                    frmAnchor.VisibleChanged += frmAnchor_VisibleChanged;
                    frmAnchor.Show(this.FindForm());
                    break;
            }
        }

        private void frmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            if (frmAnchor.Visible)
            {
                ShowKeyBorderEvent?.Invoke(this, null);
            }
            else
            {
                HideKeyBorderEvent?.Invoke(this, null);
            }
        }

        public void NumAddClick()
        {
            btnAdd_MouseDown(null, null);
        }

        public void NumMinusClick()
        {
            btnAdd_MouseDown(null, null);
        }

        private void btnAdd_MouseDown(object sender, MouseEventArgs e)
        {
            AddClick?.Invoke(this, e);

            var dec = Convert.ToDecimal(txtNum.Text);
            dec--;
            txtNum.Text = dec.ToString();
        }


        private void btnMinus_MouseDown(object sender, MouseEventArgs e)
        {
            MinusClick?.Invoke(this, e);

            var dec = Convert.ToDecimal(txtNum.Text);
            dec--;
            txtNum.Text  = dec.ToString();
        }

        private void UxNumTextBox_Load(object sender, EventArgs e)
        {
            txtNum.BackColor = BackColor;
        }

        private void txtNum_FontChanged(object sender, EventArgs e)
        {
            txtNum.Location = txtNum.Location with { Y = (this.Height - txtNum.Height) / 2 };
        }

        private void UxNumTextBox_BackColorChanged(object sender, EventArgs e)
        {
            Color color = this.BackColor;
            Control control = this;
            while (color == Color.Transparent)
            {
                control = control.Parent;
                if(control == null)
                    break;
                color = control.BackColor;
            }

            if (color == Color.Transparent)
            {
                return;
            }
            txtNum.BackColor = color;
        }
    }
}
