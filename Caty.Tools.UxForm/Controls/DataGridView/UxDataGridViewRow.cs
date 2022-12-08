using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.DataGridView
{
    public partial class UxDataGridViewRow : UserControl,IDataGridViewRow
    {
        public event DataGridViewRowCustomEventHandler? RowCustomEvent;
        public event DataGridViewEventHandler? CheckBoxChangeEvent;
        public event DataGridViewEventHandler? CellClick;
        public event DataGridViewEventHandler? SourceChanged;
        public List<DataGridViewColumnEntity> Columns { get; set; }

        public object DataSource { get; set; }

        public bool IsShowCheckBox { get; set; }

        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    
                }
            }
        }
        public UxDataGridViewRow()
        {
            InitializeComponent();
        }

        public void BindingCellData()
        {
            foreach (var com in Columns)
            {
                var cs = panCells.Controls.Find("lbl_" + com.DataField, false);
                if (cs is not { Length: > 0 }) continue;
                var pro = DataSource.GetType().GetProperty(com.DataField);
                if (pro != null)
                    cs[0].Text = pro.GetValue(DataSource, null).ToStringExt();
            }
        }

        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            CellClick?.Invoke(sender, new DataGridViewEventArgs()
            {
                CellControl = this,
                CellIndex = (sender as Control).Tag.ToInt()
            });
        }

        public void SetSelect(bool blnSelected)
        {
            BackColor = blnSelected ? Color.FromArgb(255, 247, 245) : Color.Transparent;
        }

        public int RowHeight { get; set; }
        public int RowIndex { get; set; }

        public void ReloadCells()
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                panCells.Controls.Clear();
                panCells.ColumnStyles.Clear();

                var intColumnsCount = Columns.Count();
                if (Columns == null || intColumnsCount <= 0) return;
                if (IsShowCheckBox)
                {
                    intColumnsCount++;
                }
                panCells.ColumnCount = intColumnsCount;
                for (var i = 0; i < intColumnsCount; i++)
                {
                    Control c = null;
                    if (i == 0 && IsShowCheckBox)
                    {
                        panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 30F));

                        var box = new UxCheckBox
                        {
                            Name = "check",
                            TextValue = "",
                            Size = new Size(30, 30),
                            Dock = DockStyle.Fill
                        };
                        box.CheckedChangeEvent += (a, b) =>
                        {
                            IsChecked = box.Checked;
                            CheckBoxChangeEvent?.Invoke(a, new DataGridViewEventArgs()
                            {
                                CellControl = box,
                                CellIndex = 0
                            });
                        };
                        c = box;
                    }
                    else
                    {
                        var item = Columns[i - (IsShowCheckBox ? 1 : 0)];
                        panCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(item.WidthType, item.Width));

                        var lbl = new Label
                        {
                            Tag = i - (IsShowCheckBox ? 1 : 0),
                            Name = "lbl_" + item.DataField,
                            Font = new Font("微软雅黑", 12),
                            ForeColor = Color.Black,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter
                        };
                        lbl.MouseDown += (a, b) =>
                        {
                            Item_MouseDown(a, b);
                        };
                        c = lbl;
                    }
                    panCells.Controls.Add(c, i, 0);
                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }
    }
}
