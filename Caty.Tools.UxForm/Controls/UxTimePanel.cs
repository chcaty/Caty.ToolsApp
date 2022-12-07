using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxTimePanel : UserControl
    {
        public event EventHandler? SelectSourceEvent;
        private List<KeyValuePair<string, string>>? _source;
        public bool FirstEvent { get; set; }

        public List<KeyValuePair<string, string>>? Source
        {
            get => _source;
            set
            {
                _source = value;
                SetSource(value);
            }
        }

        private bool _isShowBorder;

        public bool IsShowBorder
        {
            get => _isShowBorder;
            set
            {
                _isShowBorder = value;
                ucSplitLine_H1.Visible = value;
                ucSplitLine_H2.Visible = value;
                ucSplitLine_V1.Visible = value;
                ucSplitLine_V2.Visible = value;
            }
        }

        private UxButtonBase? _selectButton;

        /// <summary>
        /// 选中按钮
        /// </summary>
        public UxButtonBase? SelectButton
        {
            get => _selectButton;
            set
            {
                if (_selectButton is { IsDisposed: false })
                {
                    _selectButton.FillColor = Color.White;
                    _selectButton.RectColor = Color.White;
                    _selectButton.BtnForeColor = Color.FromArgb(66, 66, 66);
                }

                var blnEvent = FirstEvent || (_selectButton != null);
                _selectButton = value;
                if (_selectButton == null) return;
                _selectButton.FillColor = Color.FromArgb(255, 77, 59);
                _selectButton.RectColor = Color.FromArgb(255, 77, 59);
                _selectButton.BtnForeColor = Color.White;
                if (blnEvent && SelectSourceEvent != null)
                    SelectSourceEvent(_selectButton.Tag.ToStringExt(), null);
            }
        }

        public UxTimePanel()
        {
            InitializeComponent();
        }


        private int _row;

        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                ReloadPanel();
            }
        }


        private int _column;

        public int Column
        {
            get => _column;
            set
            {
                _column = value;
                ReloadPanel();
            }
        }

        #region 设置面板数据源

        /// <summary>
        /// 功能描述:设置面板数据源
        /// </summary>
        /// <param name="lstSource">lstSource</param>
        public void SetSource(List<KeyValuePair<string, string>>? lstSource)
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                if (_row <= 0 || _column <= 0)
                    return;
                if (Source != lstSource)
                    Source = lstSource;
                var index = 0;
                SelectButton = null;
                foreach (UxButtonBase btn in panMain.Controls)
                {
                    if (lstSource != null && index < lstSource.Count)
                    {
                        btn.BtnText = lstSource[index].Value;
                        btn.Tag = lstSource[index].Key;
                        index++;
                    }
                    else
                    {
                        btn.BtnText = "";
                        btn.Tag = null;
                    }
                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }

        #endregion

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <param name="strKey"></param>
        public void SetSelect(string strKey)
        {
            foreach (UxButtonBase item in panMain.Controls)
            {
                if (item.Tag == null || item.Tag.ToStringExt() != strKey) continue;
                SelectButton = item;
                return;
            }

            SelectButton = new UxButtonBase();
        }


        #region 重置面板

        /// <summary>
        /// 功能描述:重置面板
        /// </summary>
        public void ReloadPanel()
        {
            if (_row <= 0 || _column <= 0)
                return;
            SelectButton = null;
            panMain.Controls.Clear();
            panMain.ColumnCount = _column;
            panMain.ColumnStyles.Clear();
            for (var i = 0; i < _column; i++)
            {
                panMain.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 50F));
            }

            panMain.RowCount = _row;
            panMain.RowStyles.Clear();
            for (var i = 0; i < _row; i++)
            {
                panMain.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 50F));
            }

            for (var i = 0; i < _row; i++)
            {
                for (var j = 0; j < _column; j++)
                {
                    UxButtonBase btn = new()
                    {
                        BackColor = Color.Transparent,
                        BtnBackColor = Color.Transparent,
                        BtnFont = new Font("微软雅黑", 13F),
                        BtnForeColor = Color.FromArgb(66, 66, 66),
                        CornerRadius = 5,
                        Dock = DockStyle.Fill,
                        FillColor = Color.White,
                        Font = new Font("微软雅黑", 15F, FontStyle.Regular,
                        GraphicsUnit.Pixel),
                        Cursor = Cursor.Current,
                        IsShowRect = true,
                        IsRadius = true,
                        IsShowTips = false,
                        Name = "btn_" + i + "_" + j,
                        RectColor = Color.White,
                        RectWidth = 1,
                        Width = Width,
                        TabIndex = 0,
                        TipsText = ""
                    };
                    btn.BtnClick += btn_BtnClick;
                    panMain.Controls.Add(btn, j, i);
                }
            }

            if (Source != null)
            {
                SetSource(Source);
            }
        }

        #endregion

        private void btn_BtnClick(object sender, EventArgs e)
        {
            var btn = (UxButtonBase)sender;
            if (btn.Tag == null)
                return;
            SelectButton = btn;
        }
    }
}
