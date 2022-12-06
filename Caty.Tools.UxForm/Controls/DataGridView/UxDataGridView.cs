using System.Collections;
using System.ComponentModel;
using System.Data;
using Caty.Tools.UxForm.Controls.Page;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.DataGridView
{
    public partial class UxDataGridView : UserControl
    {
        /// <summary>
        /// 标题字体
        /// </summary>
        [Description("标题字体"), Category("自定义")]
        public Font HeadFont { get; set; } = new("微软雅黑", 12F);

        /// <summary>
        /// 标题字体颜色
        /// </summary>
        [Description("标题文字颜色"), Category("自定义")]
        public Color HeadTextColor { get; set; } = Color.Black;

        private bool _isShowHead = true;
        /// <summary>
        /// 是否显示标题
        /// </summary>
        [Description("是否显示标题"), Category("自定义")]
        public bool IsShowHead
        {
            get => _isShowHead;
            set
            {
                _isShowHead = value;
                panHead.Visible = value;
                if (_page == null) return;
                ResetShowCount();
                _page.PageSize = _showCount;
            }
        }
        private int _headHeight = 40;
        /// <summary>
        /// 标题高度
        /// </summary>
        [Description("标题高度"), Category("自定义")]
        public int HeadHeight
        {
            get => _headHeight;
            set
            {
                _headHeight = value;
                panHead.Height = value;
            }
        }

        private bool _isShowCheckBox;
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [Description("是否显示选择框"), Category("自定义")]
        public bool IsShowCheckBox
        {
            get => _isShowCheckBox;
            set
            {
                if (value == _isShowCheckBox) return;
                _isShowCheckBox = value;
                LoadColumns();
            }
        }

        /// <summary>
        /// 行高
        /// </summary>
        [Description("数据行高"), Category("自定义")]
        public int RowHeight { get; set; } = 40;

        private int _showCount;
        /// <summary>
        /// 
        /// </summary>
        [Description("可显示个数"), Category("自定义")]
        public int ShowCount
        {
            get => _showCount;
            private set
            {
                _showCount = value;
                if (_page != null)
                {
                    _page.PageSize = value;
                }
            }
        }

        private List<DataGridViewColumnEntity> _columns;
        /// <summary>
        /// 列
        /// </summary>
        [Description("列"), Category("自定义")]
        public List<DataGridViewColumnEntity> Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                LoadColumns();
            }
        }

        private object _dataSource;
        /// <summary>
        /// 数据源,支持列表或table，如果使用翻页控件，请使用翻页控件的DataSource
        /// </summary>
        [Description("数据源,支持列表或table，如果使用翻页控件，请使用翻页控件的DataSource"), Category("自定义")]
        public object DataSource
        {
            get => _dataSource;
            set
            {
                if (value == null)
                    return;
                if (_dataSource is not DataTable && (value is not IList))
                {
                    throw new Exception("数据源不是有效的数据类型，请使用DataTable或列表");
                }

                _dataSource = value;
                ReloadSource();
            }
        }

        public List<IDataGridViewRow> Rows { get; private set; }

        private Type _rowType = typeof(UxDataGridViewRow);
        /// <summary>
        /// 行元素类型，默认UCDataGridViewItem
        /// </summary>
        [Description("行控件类型，默认UCDataGridViewRow，如果不满足请自定义行控件实现接口IDataGridViewRow"), Category("自定义")]
        public Type RowType
        {
            get => _rowType;
            set
            {
                if (!typeof(IDataGridViewRow).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                    throw new Exception("行控件没有实现IDataGridViewRow接口");
                _rowType = value;
            }
        }

        /// <summary>
        /// 选中的节点
        /// </summary>
        [Description("选中行"), Category("自定义")]
        public IDataGridViewRow SelectRow { get; private set; }


        /// <summary>
        /// 选中的行，如果显示CheckBox，则以CheckBox选中为准
        /// </summary>
        [Description("选中的行，如果显示CheckBox，则以CheckBox选中为准"), Category("自定义")]
        public List<IDataGridViewRow> SelectRows
        {
            get
            {
                return _isShowCheckBox ? Rows.FindAll(p => p.IsChecked) : new List<IDataGridViewRow> { SelectRow };
            }
        }


        private UxPageBase _page;
        /// <summary>
        /// 翻页控件
        /// </summary>
        [Description("翻页控件，如果UCPagerControl不满足你的需求，请自定义翻页控件并继承UCPagerControlBase"), Category("自定义")]
        public UxPageBase Page
        {
            get => _page;
            set
            {
                if (value is not IPageControl || !value.GetType().IsSubclassOf(typeof(UxPageBase)))
                    throw new Exception("翻页控件没有继承UCPagerControlBase");
                panPage.Visible = value != null;
                _page = value;
                if (value != null)
                {
                    _page.ShowSourceChanged += page_ShowSourceChanged;
                    _page.Dock = DockStyle.Fill;
                    panPage.Controls.Clear();
                    panPage.Controls.Add(_page);
                    ResetShowCount();
                    _page.PageSize = ShowCount;
                    DataSource = _page.GetCurrentSource();
                }
                else
                {
                    _page = null;
                }
            }
        }

        private void page_ShowSourceChanged(object currentSource)
        {
            DataSource = currentSource;
        }

        [Description("选中标题选择框事件"), Category("自定义")]
        public EventHandler HeadCheckBoxChangeEvent;
        [Description("标题点击事件"), Category("自定义")]
        public EventHandler HeadColumnClickEvent;
        [Description("项点击事件"), Category("自定义")]
        public event DataGridViewEventHandler ItemClick;
        [Description("数据源改变事件"), Category("自定义")]
        public event DataGridViewEventHandler SourceChanged;

        public UxDataGridView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 功能描述:加载column
        /// 作　　者:HZH
        /// 创建日期:2019-08-08 17:51:50
        /// 任务编号:POS
        /// </summary>
        private void LoadColumns()
        {
            try
            {
                if (DesignMode)
                { return; }

                ControlHelper.FreezeControl(panHead, true);
                panColumns.Controls.Clear();
                panColumns.ColumnStyles.Clear();

                if (_columns == null || !_columns.Any()) return;
                var intColumnsCount = _columns.Count();
                if (_isShowCheckBox)
                {
                    intColumnsCount++;
                }
                panColumns.ColumnCount = intColumnsCount;
                for (var i = 0; i < intColumnsCount; i++)
                {
                    Control c = null;
                    if (i == 0 && _isShowCheckBox)
                    {
                        panColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));

                        var box = new UxCheckBox
                        {
                            TextValue = "",
                            Size = new Size(30, 30)
                        };
                        box.CheckedChangeEvent += (a, b) =>
                        {
                            Rows.ForEach(p => p.IsChecked = box.Checked);
                            if (HeadCheckBoxChangeEvent != null)
                            {
                                HeadCheckBoxChangeEvent(a, b);
                            }
                        };
                        c = box;
                    }
                    else
                    {
                        var item = _columns[i - (_isShowCheckBox ? 1 : 0)];
                        panColumns.ColumnStyles.Add(new ColumnStyle(item.WidthType, item.Width));
                        var lbl = new Label
                        {
                            Name = "dgvColumns_" + i,
                            Text = item.HeadText,
                            Font = HeadFont,
                            ForeColor = HeadTextColor,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Dock = DockStyle.Fill
                        };
                        lbl.MouseDown += (a, b) =>
                        {
                            if (HeadColumnClickEvent != null)
                            {
                                HeadColumnClickEvent(a, b);
                            }
                        };
                        c = lbl;
                    }
                    panColumns.Controls.Add(c, i, 0);
                }
            }
            finally
            {
                ControlHelper.FreezeControl(panHead, false);
            }
        }

        /// <summary>
        /// 功能描述:获取显示个数
        /// 作　　者:HZH
        /// 创建日期:2019-03-05 10:02:58
        /// 任务编号:POS
        /// </summary>
        /// <returns>返回值</returns>
        private void ResetShowCount()
        {
            if (DesignMode)
            { return; }
            ShowCount = panRow.Height / (RowHeight);
            var intCha = panRow.Height % (RowHeight);
            RowHeight += intCha / ShowCount;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void ReloadSource()
        {
            if (DesignMode)
            { return; }
            try
            {
                if (_columns is not { Count: > 0 })
                    return;

                ControlHelper.FreezeControl(panRow, true);
                panRow.Controls.Clear();
                Rows = new List<IDataGridViewRow>();
                if (_dataSource == null) return;
                var intIndex = 0;
                Control lastItem = null;

                var intSourceCount = _dataSource switch
                {
                    DataTable table => table.Rows.Count,
                    IList list => list.Count,
                    _ => 0
                };

                foreach (Control item in panRow.Controls)
                {


                    if (intIndex >= intSourceCount)
                    {
                        item.Visible = false;
                    }
                    else
                    {
                        var row = (item as IDataGridViewRow);
                        row.IsShowCheckBox = _isShowCheckBox;
                        if (_dataSource is DataTable table)
                        {
                            row.DataSource = table.Rows[intIndex];
                        }
                        else
                        {
                            row.DataSource = (_dataSource as IList)[intIndex];
                        }
                        row.BindingCellData();
                        item.Height = RowHeight;
                        item.Visible = true;
                        item.BringToFront();
                        lastItem ??= item;
                        Rows.Add(row);
                    }
                    intIndex++;
                }

                if (intIndex < intSourceCount)
                {
                    for (var i = intIndex; i < intSourceCount; i++)
                    {
                        var row = (IDataGridViewRow)Activator.CreateInstance(_rowType);
                        if (_dataSource is DataTable table)
                        {
                            row.DataSource = table.Rows[i];
                        }
                        else
                        {
                            row.DataSource = (_dataSource as IList)[i];
                        }
                        row.Columns = _columns;
                        row.IsShowCheckBox = _isShowCheckBox;
                        row.ReloadCells();
                        row.BindingCellData();


                        var rowControl = (row as Control);
                        rowControl.Height = RowHeight;
                        panRow.Controls.Add(rowControl);
                        rowControl.Dock = DockStyle.Top;
                        row.CellClick += (a, b) => { SetSelectRow(rowControl, b); };
                        row.CheckBoxChangeEvent += (a, b) => { SetSelectRow(rowControl, b); };
                        row.SourceChanged += RowSourceChanged;
                        rowControl.BringToFront();
                        Rows.Add(row);

                        lastItem ??= rowControl;
                    }
                }
                if (lastItem != null && intSourceCount == _showCount)
                {
                    lastItem.Height = panRow.Height - (_showCount - 1) * RowHeight;
                }
            }
            finally
            {
                ControlHelper.FreezeControl(panRow, false);
            }
        }


        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    Previous();
                    break;
                case Keys.Down:
                    Next();
                    break;
                case Keys.Home:
                    First();
                    break;
                case Keys.End:
                    End();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// 选中第一个
        /// </summary>
        public void First()
        {
            if (Rows is not { Count: > 0 })
                return;
            Control c = null;
            c = (Rows[0] as Control);
            SetSelectRow(c, new DataGridViewEventArgs { RowIndex = 0 });
        }
        /// <summary>
        /// 选中上一个
        /// </summary>
        public void Previous()
        {
            if (Rows is not { Count: > 0 })
                return;

            var index = Rows.IndexOf(SelectRow);
            if (index - 1 < 0) return;
            var c = (Rows[index - 1] as Control);
            SetSelectRow(c, new DataGridViewEventArgs { RowIndex = index - 1 });
        }
        /// <summary>
        /// 选中下一个
        /// </summary>
        public void Next()
        {
            if (Rows is not { Count: > 0 })
                return;

            var index = Rows.IndexOf(SelectRow);
            if (index + 1 >= Rows.Count) return;
            var c = (Rows[index + 1] as Control);
            SetSelectRow(c, new DataGridViewEventArgs { RowIndex = index + 1 });
        }
        /// <summary>
        /// 选中最后一个
        /// </summary>
        public void End()
        {
            if (Rows is not { Count: > 0 })
                return;
            var c = (Rows[Rows.Count - 1] as Control);
            SetSelectRow(c, new DataGridViewEventArgs { RowIndex = Rows.Count - 1 });
        }

        void RowSourceChanged(object sender, DataGridViewEventArgs e)
        {
            if (SourceChanged != null)
                SourceChanged(sender, e);
        }
        private void SetSelectRow(Control item, DataGridViewEventArgs e)
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                if (item == null)
                    return;
                if (item.Visible == false)
                    return;
                FindForm().ActiveControl = this;
                FindForm().ActiveControl = item;
                if (SelectRow != null)
                {
                    if (SelectRow == item)
                        return;
                    SelectRow.SetSelect(false);
                }
                SelectRow = item as IDataGridViewRow;
                SelectRow.SetSelect(true);
                if (ItemClick != null)
                {
                    ItemClick(item, e);
                }

                if (panRow.Controls.Count <= 0) return;
                if (item.Location.Y < 0)
                {
                    panRow.AutoScrollPosition = new Point(0, Math.Abs(panRow.Controls[panRow.Controls.Count - 1].Location.Y) + item.Location.Y);
                }
                else if (item.Location.Y + RowHeight > panRow.Height)
                {
                    panRow.AutoScrollPosition = new Point(0, Math.Abs(panRow.AutoScrollPosition.Y) + item.Location.Y - panRow.Height + RowHeight);
                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }
        private void UxDataGridView_Resize(object sender, EventArgs e)
        {
            ResetShowCount();
            ReloadSource();
        }
    }
}
