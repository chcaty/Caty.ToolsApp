using System.Collections;
using System.ComponentModel;
using System.Data;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.DataGridView;

public partial class UxDataGridView : UserControl
{
    #region 属性
    /// <summary>
    /// The m head pading left
    /// </summary>
    private int _headPaddingLeft;

    /// <summary>
    /// Gets or sets the head pading left.
    /// </summary>
    /// <value>The head pading left.</value>
    [Description("标题左侧间距"), Category("自定义")]
    public int HeadPaddingLeft
    {
        get => _headPaddingLeft;
        set
        {
            _headPaddingLeft = value;
            panHeadLeft.Width = _headPaddingLeft;
        }
    }

    /// <summary>
    /// 标题字体
    /// </summary>
    /// <value>The head font.</value>
    [Description("标题字体"), Category("自定义")]
    public Font HeadFont { get; set; } = new("微软雅黑", 12F);

    /// <summary>
    /// 标题字体颜色
    /// </summary>
    /// <value>The color of the head text.</value>
    [Description("标题文字颜色"), Category("自定义")]
    public Color HeadTextColor { get; set; } = Color.Black;

    /// <summary>
    /// The m is show head
    /// </summary>
    private bool _isShowHead = true;
    /// <summary>
    /// 是否显示标题
    /// </summary>
    /// <value><c>true</c> if this instance is show head; otherwise, <c>false</c>.</value>
    [Description("是否显示标题"), Category("自定义")]
    public bool IsShowHead
    {
        get => _isShowHead;
        set
        {
            if (_isShowHead == value) return;
            _isShowHead = value;
            panHead.Visible = value;
            if (value)
            {
                panRow.Location = new Point(0, panHead.Height);
                panRow.Height -= panHead.Height;
            }
            else
            {
                panRow.Location = new Point(0, 0);
                panRow.Height += panHead.Height;
            }
        }
    }
    /// <summary>
    /// The m head height
    /// </summary>
    private int _headHeight = 40;
    /// <summary>
    /// 标题高度
    /// </summary>
    /// <value>The height of the head.</value>
    [Description("标题高度"), Category("自定义")]
    public int HeadHeight
    {
        get => _headHeight;
        set
        {
            _headHeight = value;
            panHead.Height = value;
            Padding = new Padding(0, value, 0, 0);
        }
    }

    /// <summary>
    /// The m is show CheckBox
    /// </summary>
    private bool _isShowCheckBox;
    /// <summary>
    /// 是否显示复选框
    /// </summary>
    /// <value><c>true</c> if this instance is show CheckBox; otherwise, <c>false</c>.</value>
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
    /// <value>The height of the row.</value>
    [Description("数据行高"), Category("自定义")]
    public int RowHeight { get; set; } = 40;


    /// <summary>
    /// Gets the show count.
    /// </summary>
    /// <value>The show count.</value>
    [Description("当前高度可显示行个数"), Category("自定义")]
    public int ShowCount => panRow.Height / (RowHeight);

    /// <summary>
    /// The m columns
    /// </summary>
    private List<DataGridViewColumnEntity> _columns;
    /// <summary>
    /// 列
    /// </summary>
    /// <value>The columns.</value>
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

    /// <summary>
    /// The m data source
    /// </summary>
    private object _dataSource;
    /// <summary>
    /// 数据源,支持列表或table，如果使用翻页控件，请使用翻页控件的DataSource
    /// </summary>
    /// <value>The data source.</value>
    /// <exception cref="Exception">数据源不是有效的数据类型，请使用Datatable或列表</exception>
    /// <exception cref="System.Exception">数据源不是有效的数据类型，请使用Datatable或列表</exception>
    [Description("数据源,支持列表或table，如果使用翻页控件，请使用翻页控件的DataSource"), Category("自定义")]
    public object DataSource
    {
        get => _dataSource;
        set
        {
            if (value != null)
            {
                if (value is not DataTable && (value is not IList))
                {
                    throw new Exception("数据源不是有效的数据类型，请使用DataTable或列表");
                }
            }
            _dataSource = value;
            if (_columns is { Count: > 0 })
            {
                ReloadSource();
            }
            BindingSourceEvent?.Invoke(this, null);
        }
    }

    /// <summary>
    /// Gets the rows.
    /// </summary>
    /// <value>The rows.</value>
    public List<IDataGridViewRow> Rows { get; private set; }

    /// <summary>
    /// The m row type
    /// </summary>
    private Type _rowType = typeof(UxDataGridViewRow);
    /// <summary>
    /// 行元素类型，默认UCDataGridViewItem
    /// </summary>
    /// <value>The type of the row.</value>
    /// <exception cref="Exception">行控件没有实现IDataGridViewRow接口</exception>
    /// <exception cref="System.Exception">行控件没有实现IDataGridViewRow接口</exception>
    [Description("行控件类型，默认UCDataGridViewRow，如果不满足请自定义行控件实现接口IDataGridViewRow"), Category("自定义")]
    public Type RowType
    {
        get => _rowType;
        set
        {
            if (value == null)
                return;
            if (!typeof(IDataGridViewRow).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("行控件没有实现IDataGridViewRow接口");
            _rowType = value;
            if (_columns is { Count: > 0 })
                ReloadSource();
        }
    }

    /// <summary>
    /// 选中的节点
    /// </summary>
    /// <value>The select row.</value>
    [Description("选中行"), Category("自定义")]
    public IDataGridViewRow SelectRow { get; private set; }


    /// <summary>
    /// 选中的行，如果显示CheckBox，则以CheckBox选中为准
    /// </summary>
    /// <value>The select rows.</value>
    [Description("选中的行，如果显示CheckBox，则以CheckBox选中为准"), Category("自定义")]
    public List<IDataGridViewRow> SelectRows => GetSelectRows();

    /// <summary>
    /// Gets the select rows.
    /// </summary>
    /// <returns>List&lt;IDataGridViewRow&gt;.</returns>
    private List<IDataGridViewRow> GetSelectRows()
    {
        List<IDataGridViewRow> lst = new();
        if (_isShowCheckBox)
        {
            if (Rows is { Count: > 0 })
                lst.AddRange(Rows.FindAll(p => p.IsChecked));
        }
        else
        {
            if (SelectRow != null)
                lst.AddRange(new List<IDataGridViewRow> { SelectRow });
        }

        if (Rows is not { Count: > 0 } || _rowType != typeof(UxDataGridViewTreeRow)) return lst;
        foreach (var row in Rows.Cast<UxDataGridViewTreeRow>())
        {
            lst.AddRange(FindTreeRowSelected(row));
        }
        return lst;
    }

    private static IEnumerable<IDataGridViewRow> FindTreeRowSelected(UxDataGridViewTreeRow row)
    {
        List<IDataGridViewRow> lst = new();
        var _lst = row.ChildrenRows.FindAll(p => p.IsChecked);
        lst.AddRange(_lst);
        foreach (var treeRow in row.ChildrenRows)
        {
            lst.AddRange(FindTreeRowSelected(treeRow));
        }
        return lst;
    }

    /// <summary>
    /// Finds the child grid.
    /// </summary>
    /// <param name="c">The c.</param>
    /// <returns>UCDataGridView.</returns>
    private UxDataGridView FindChildGrid(Control c)
    {
        foreach (Control item in c.Controls)
        {
            if (item is UxDataGridView view)
                return view;
            if (item.Controls.Count <= 0) continue;
            var grid = FindChildGrid(item);
            return grid;
        }
        return null;
    }
    [Bindable(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoScroll
    {
        get => base.AutoScroll;
        set => base.AutoScroll = value;
    }
    #region 事件
    /// <summary>
    /// The head CheckBox change event
    /// </summary>
    [Description("选中标题选择框事件"), Category("自定义")]
    public EventHandler HeadCheckBoxChangeEvent;
    /// <summary>
    /// The head column click event
    /// </summary>
    [Description("标题点击事件"), Category("自定义")]
    public EventHandler HeadColumnClickEvent;
    /// <summary>
    /// Occurs when [item click].
    /// </summary>
    [Description("项点击事件"), Category("自定义")]
    public event DataGridViewEventHandler ItemClick;
    /// <summary>
    /// Occurs when [source changed].
    /// </summary>
    [Description("行数据源改变事件"), Category("自定义")]
    public event DataGridViewEventHandler RowSourceChangedEvent;
    /// <summary>
    /// Occurs when [row custom event].
    /// </summary>
    [Description("预留的自定义的事件，比如你需要在行上放置删改等按钮时，可以通过此事件传递出来"), Category("自定义")]
    public event DataGridViewRowCustomEventHandler RowCustomEvent;
    [Description("绑定数据源后事件"), Category("自定义")]
    public event EventHandler BindingSourceEvent;
    #endregion
    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="UxDataGridView" /> class.
    /// </summary>
    public UxDataGridView()
    {
        InitializeComponent();
    }

    #region 私有方法
    #region 加载column
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

            panRow.Controls.Clear();
            panColumns.Controls.Clear();
            panColumns.ColumnStyles.Clear();


            if (_columns == null || !_columns.Any()) return;
            var width = 0;
            _columns.ForEach(p =>
            {
                switch (p.WidthType)
                {
                    case SizeType.Absolute:
                        width += p.Width;
                        break;
                    case SizeType.Percent:
                        width += (int)((p.Width / 100f) * Width);
                        break;
                }
            });
            if (_isShowCheckBox)
                width += 30;
            if (width > Width)
            {
                //this.panRow.Width = _width;
                panHead.Width = width;
            }
            else
            {
                //this.panRow.Width = this.Width;
                panHead.Width = _columns.Any(p => p.WidthType == SizeType.AutoSize) ? Width :
                    //this.panRow.Width = _width;
                    width;
            }
            _columns.FindAll(p => p.WidthType == SizeType.Absolute);
            _columns.FindAll(p => p.WidthType == SizeType.Percent);
            var intColumnsCount = _columns.Count;
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
                        HeadCheckBoxChangeEvent?.Invoke(a, b);
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
                        HeadColumnClickEvent?.Invoke(a, b);
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
    #endregion

    #endregion



    #region 公共函数
    /// <summary>
    /// 刷新数据
    /// </summary>
    public void ReloadSource()
    {
        if (DesignMode) { return; }
        try
        {
            ControlHelper.FreezeControl(this, true);
            panHead.Location = new Point(0, 0);
            Rows = new List<IDataGridViewRow>();
            if (_columns is not { Count: > 0 })
                return;
            if (_dataSource != null)
            {
                var intIndex = 0;

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
                        if (row.RowHeight != RowHeight)
                            row.RowHeight = RowHeight;
                        item.Visible = true;
                        item.Width = panHead.Width;

                        Rows.Add(row);
                        row.RowIndex = Rows.IndexOf(row);
                    }
                    intIndex++;
                }

                if (intIndex >= intSourceCount) return;
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
                        rowControl.Width = panHead.Width;
                        row.RowHeight = RowHeight;
                        row.CellClick += (a, b) => { FindForm().ActiveControl = this; rowControl.Focus(); SetSelectRow(rowControl, b); };
                        row.CheckBoxChangeEvent += (a, b) => { SetSelectRow(rowControl, b); };
                        row.RowCustomEvent += (a, b) => { RowCustomEvent?.Invoke(a, b); };
                        row.SourceChanged += RowSourceChanged;
                        Rows.Add(row);
                        row.RowIndex = Rows.IndexOf(row);
                        panRow.Controls.Add(rowControl);

                    }
                }

                //this.panRow.Height = intSourceCount * RowHeight;
            }
            else
            {
                foreach (Control item in panRow.Controls)
                {
                    item.Visible = false;
                }
            }
        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }
    }

    /// <summary>
    /// 快捷键
    /// </summary>
    /// <param name="msg">通过引用传递的 <see cref="T:System.Windows.Forms.Message" />，它表示要处理的窗口消息。</param>
    /// <param name="keyData"><see cref="T:System.Windows.Forms.Keys" /> 值之一，它表示要处理的键。</param>
    /// <returns>如果字符已由控件处理，则为 true；否则为 false。</returns>
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
        var c = Rows[0] as Control;
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
        var c = Rows[^1] as Control;
        SetSelectRow(c, new DataGridViewEventArgs { RowIndex = Rows.Count - 1 });
    }

    #endregion

    #region 事件

    /// <summary>
    /// Rows the source changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
    private void RowSourceChanged(object sender, DataGridViewEventArgs e)
    {
        RowSourceChangedEvent?.Invoke(sender, e);
    }

    /// <summary>
    /// Sets the select row.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
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
            if (SelectRow != item)
            {
                SelectRow?.SetSelect(false);
                SelectRow = item as IDataGridViewRow;
                SelectRow.SetSelect(true);

                if (panRow.Controls.Count > 0)
                {
                    if (item.Location.Y < 0)
                    {
                        panRow.AutoScrollPosition = new Point(0, Math.Abs(panRow.Controls[panRow.Controls.Count - 1].Location.Y) + item.Location.Y);
                    }
                    else if (item.Location.Y + RowHeight > panRow.Height)
                    {
                        panRow.AutoScrollPosition = new Point(0, Math.Abs(panRow.AutoScrollPosition.Y) + item.Location.Y - panRow.Height + RowHeight);
                    }
                }
            }


            ItemClick?.Invoke(item, e);
        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }
    }

    #endregion
}