using System.ComponentModel;
using Caty.Tools.UxForm.Forms;

namespace Caty.Tools.UxForm.Controls
{
    [DefaultEvent("SelectedChangedEvent")]
    public partial class UxComboBox : UxControlBase
    {
        private Color _foreColor = Color.FromArgb(64, 64, 64);

        [Description("文字颜色"), Category("自定义")]
        public override Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                lblInput.ForeColor = value;
                txtInput.ForeColor = value;
            }
        }

        public event EventHandler? SelectedChangedEvent;
        public event EventHandler? TextChangedEvent;

        private ComboBoxStyle _boxStyle = ComboBoxStyle.DropDown;

        [Description("控件样式"), Category("自定义")]
        public ComboBoxStyle BoxStyle
        {
            get => _boxStyle;
            set
            {
                _boxStyle = value;
                if (value == ComboBoxStyle.DropDownList)
                {
                    lblInput.Visible = true;
                    txtInput.Visible = false;
                }
                else
                {
                    lblInput.Visible = false;
                    txtInput.Visible = true;
                }

                if (_boxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.BackColor = _backColor;
                    FillColor = _backColor;
                    RectColor = _backColor;
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    FillColor = Color.White;
                    RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }

        private Font _font = new("微软雅黑", 12);

        [Description("字体"), Category("自定义")]
        public new Font Font
        {
            get => _font;
            set
            {
                _font = value;
                lblInput.Font = value;
                txtInput.Font = value;
                txtInput.PromptFont = value;
                txtInput.Location = txtInput.Location with { Y = (Height - txtInput.Height) / 2 };
                lblInput.Location = lblInput.Location with { Y = (Height - lblInput.Height) / 2 };
            }
        }

        private string _textValue;

        public string TextValue
        {
            get => _textValue;
            set
            {
                _textValue = value;
                if (lblInput.Text != value)
                    lblInput.Text = value;
                if (txtInput.Text != value)
                    txtInput.Text = value;
            }
        }

        private List<KeyValuePair<string, string>> _source;

        public List<KeyValuePair<string, string>> Source
        {
            get => _source;
            set
            {
                _source = value;
                _selectedIndex = -1;
                _selectedValue = "";
                _selectedItem = new KeyValuePair<string, string>();
                _selectedText = "";
                lblInput.Text = "";
                txtInput.Text = "";
            }
        }

        private KeyValuePair<string, string> _selectedItem;

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < 0 || _source is not { Count: > 0 } || value >= _source.Count)
                {
                    _selectedIndex = -1;
                    _selectedValue = "";
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
                else
                {
                    _selectedIndex = value;
                    _selectedItem = _source[value];
                    _selectedValue = _source[value].Key;
                    SelectedText = _source[value].Value;
                }
            }
        }

        private string _selectedValue = "";

        public string SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (_source is not { Count: > 0 })
                {
                    SelectedText = "";
                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                }
                else
                {
                    for (var i = 0; i < _source.Count; i++)
                    {
                        if (_source[i].Key != value) continue;
                        _selectedValue = value;
                        _selectedIndex = i;
                        _selectedItem = _source[i];
                        SelectedText = _source[i].Value;
                        return;
                    }

                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
            }
        }

        private string _selectedText = "";

        public string SelectedText
        {
            get => _selectedText;
            private set
            {
                _selectedText = value;
                lblInput.Text = _selectedText;
                txtInput.Text = _selectedText;
                SelectedChangedEvent?.Invoke(this, null);
            }
        }

        public int ItemWidth { get; set; } = 70;

        public int DropPanelHeight { get; set; } = -1;

        private Color _backColor = Color.FromArgb(240, 240, 240);

        public Color BackColorExt
        {
            get => _backColor;
            set
            {
                if (value == Color.Transparent)
                    return;
                _backColor = value;
                lblInput.BackColor = value;

                if (_boxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.BackColor = value;
                    FillColor = value;
                    RectColor = value;
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    FillColor = Color.White;
                    RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }

        public UxComboBox()
        {
            InitializeComponent();
            lblInput.BackColor = _backColor;
            if (_boxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _backColor;
                FillColor = _backColor;
                RectColor = _backColor;
            }
            else
            {
                txtInput.BackColor = Color.White;
                FillColor = Color.White;
                RectColor = Color.FromArgb(220, 220, 220);
            }

            base.BackColor = Color.Transparent;
        }

        private void UxComboBox_SizeChanged(object sender, EventArgs e)
        {
            txtInput.Location = txtInput.Location with { Y = (Height - txtInput.Height) / 2 };
            lblInput.Location = lblInput.Location with { Y = (Height - lblInput.Height) / 2 };
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            TextValue = txtInput.Text;
            TextChangedEvent?.Invoke(this, null);
        }

        private void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {
                if (Source is not { Count: > 0 }) return;
                int intRow;
                var intCom = 1;
                var p = PointToScreen(Location);
                while (true)
                {
                    var intScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if ((p.Y + Height + Source.Count / intCom * 50 < intScreenHeight ||
                         p.Y - Source.Count / intCom * 50 > 0)
                        && (DropPanelHeight <= 0 || (Source.Count / intCom * 50 <= DropPanelHeight)))
                    {
                        intRow = Source.Count / intCom + (Source.Count % intCom != 0 ? 1 : 0);
                        break;
                    }

                    intCom++;
                }

                UxTimePanel ucTime = new UxTimePanel();
                ucTime.IsShowBorder = true;
                var intWidth = Width / intCom;
                if (intWidth < ItemWidth)
                    intWidth = ItemWidth;
                var size = new Size(intCom * intWidth, intRow * 50);
                ucTime.Size = size;
                ucTime.FirstEvent = true;
                ucTime.SelectSourceEvent += ucTime_SelectSourceEvent;
                ucTime.Row = intRow;
                ucTime.Column = intCom;
                var lst = Source.Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToList();

                ucTime.Source = lst;

                ucTime.SetSelect(_selectedValue);

                _frmAnchor = new FrmAnchor(this, ucTime);
                _frmAnchor.Load += (a, b) => { (a as Form).Size = size; };

                _frmAnchor.Show(FindForm());
            }
            else
            {
                _frmAnchor.Close();
            }
        }


        private FrmAnchor _frmAnchor;

        private void ucTime_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor is not { IsDisposed: false, Visible: true }) return;
            SelectedValue = sender.ToString();
            _frmAnchor.Close();
        }

        private void UxComboBox_Load(object sender, EventArgs e)
        {
            if (_boxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _backColor;
                FillColor = _backColor;
                RectColor = _backColor;
            }
            else
            {
                txtInput.BackColor = Color.White;
                FillColor = Color.White;
                RectColor = Color.FromArgb(220, 220, 220);
            }
        }
    }
}
