using System.Collections;
using System.Data;
using Caty.Tools.UxForm.Controls.DataGridView;
using Caty.Tools.UxForm.Helpers;
using Caty.Tools.UxForm.Properties;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxDataGridViewTreeRow : UserControl, IDataGridViewRow
    {

        #region 属性
        /// <summary>
        /// CheckBox选中事件
        /// </summary>
        public event DataGridViewEventHandler? CheckBoxChangeEvent;

        /// <summary>
        /// 点击单元格事件
        /// </summary>
        public event DataGridViewEventHandler? CellClick;

        /// <summary>
        /// 数据源改变事件
        /// </summary>
        public event DataGridViewEventHandler? SourceChanged;
        /// <summary>
        /// Occurs when [row custom event].
        /// </summary>
        public event DataGridViewRowCustomEventHandler? RowCustomEvent;
        /// <summary>
        /// 列参数，用于创建列数和宽度
        /// </summary>
        /// <value>The columns.</value>
        public List<DataGridViewColumnEntity>? Columns { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        /// <value>The data source.</value>
        public object DataSource { get; set; }

        private int _rowLevel;
        /// <summary>
        /// 折叠的第几层，用于缩进
        /// </summary>
        public int RowLevel
        {
            get => _rowLevel;
            set
            {
                _rowLevel = value;
                Padding = new Padding(panLeft.Width / 2 * value, Padding.Top, panLeft.Right, Padding.Bottom);
                uxSplitLineV1.Visible = value > 0;
            }
        }

        /// <summary>
        /// 行号，树状图目前没有给予行号
        /// </summary>
        /// <value>The Index of the row.</value>
        public int RowIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is show CheckBox.
        /// </summary>
        /// <value><c>true</c> if this instance is show CheckBox; otherwise, <c>false</c>.</value>
        public bool IsShowCheckBox { get; set; }
        /// <summary>
        /// The m is checked
        /// </summary>
        private bool _isChecked;
        /// <summary>
        /// 是否选中
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked == value) return;
                _isChecked = value;
                (panCells.Controls.Find("check", false)[0] as UxCheckBox).Checked = value;
            }
        }

        /// <summary>
        /// The m row height
        /// </summary>
        private int _rowHeight = 40;
        /// <summary>
        /// 行高
        /// </summary>
        /// <value>The height of the row.</value>
        public int RowHeight
        {
            get => _rowHeight;
            set
            {
                _rowHeight = value;
                Height = value;
            }
        }

        public List<UxDataGridViewTreeRow> ChildrenRows { get; set; } = new();

        private bool? _isOpened = false;
        /// <summary>
        /// 是否打开状态
        /// </summary>
        public bool? IsOpened
        {
            get => _isOpened;
            set
            {
                _isOpened = value;
                if (value.HasValue)
                {
                    panLeft.Enabled = true;
                    panLeft.BackgroundImage = value.Value ? Resources.caret_down : Resources.caret_right;
                }
                else
                {
                    panLeft.BackgroundImage = null;
                    panLeft.Enabled = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UxDataGridViewTreeRow" /> class.
        /// </summary>
        public UxDataGridViewTreeRow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定数据到Cell
        /// </summary>
        public void BindingCellData()
        {
            foreach (var com in Columns)
            {
                var cs = panCells.Controls.Find("lbl_" + com.DataField, false);
                if (cs is not { Length: > 0 }) continue;
                var pro = DataSource.GetType().GetProperty(com.DataField);
                if (pro == null) continue;
                var value = pro.GetValue(DataSource, null);
                cs[0].Text = com.Format != null ? com.Format(value) : value.ToStringExt();
            }
            foreach (Control item in panCells.Controls)
            {
                if (item is not IDataGridViewCustomCell cell) continue;
                cell.RowCustomEvent += Cell_RowCustomEvent;
                cell.SetBindSource(DataSource);
            }
            IsOpened = false;
            var proChildrens = DataSource.GetType().GetProperty("Childrens");
            if (proChildrens != null)
            {
                var value = proChildrens.GetValue(DataSource, null);
                if (value != null)
                {
                }
                else
                {
                    IsOpened = null;
                }
            }
            else
            {
                IsOpened = null;
            }
        }

        private void Cell_RowCustomEvent(object sender, DataGridViewRowCustomEventArgs e)
        {
            RowCustomEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the MouseDown event of the Item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            CellClick?.Invoke(this, new DataGridViewEventArgs
            {
                CellControl = this,
                CellIndex = (sender as Control).Tag.ToInt()
            });
        }

        /// <summary>
        /// 设置选中状态，通常为设置颜色即可
        /// </summary>
        /// <param name="blnSelected">是否选中</param>
        public void SetSelect(bool blnSelected)
        {
            panMain.BackColor = blnSelected ? Color.FromArgb(255, 247, 245) : Color.Transparent;
        }

        /// <summary>
        /// 添加单元格元素，仅做添加控件操作，不做数据绑定，数据绑定使用BindingCells
        /// </summary>
        public void ReloadCells()
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                panCells.Controls.Clear();
                panCells.ColumnStyles.Clear();

                if (Columns is not { Count: > 0 }) return;
                var intColumnsCount = Columns.Count;
                if (IsShowCheckBox)
                {
                    intColumnsCount++;
                }
                panCells.ColumnCount = intColumnsCount;
                var blnFirst = true;
                for (var i = 0; i < intColumnsCount; i++)
                {
                    Control c = null;
                    if (i == 0 && IsShowCheckBox)
                    {
                        panCells.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));

                        var box = new UxCheckBox
                        {
                            Name = "check",
                            TextValue = "",
                            Size = new Size(30, 30),
                            Dock = DockStyle.Fill
                        };
                        box.CheckedChangeEvent += Box_CheckedChangeEvent;
                        c = box;
                    }
                    else
                    {
                        var item = Columns[i - (IsShowCheckBox ? 1 : 0)];
                        var w = item.Width - (blnFirst ? (panLeft.Width / 2 * _rowLevel) : 0);
                        if (w < 5)
                            w = 5;
                        panCells.ColumnStyles.Add(new ColumnStyle(item.WidthType, w));
                        blnFirst = false;
                        if (item.CustomCellType == null)
                        {
                            var lbl = new Label
                            {
                                Tag = i - (IsShowCheckBox ? 1 : 0),
                                Name = "lbl_" + item.DataField,
                                Font = new Font("微软雅黑", 12),
                                ForeColor = Color.Black,
                                AutoSize = false,
                                Dock = DockStyle.Fill,
                                TextAlign = item.TextAlign
                            };
                            lbl.MouseDown += (a, b) =>
                            {
                                Item_MouseDown(a, b);
                            };
                            c = lbl;
                        }
                        else
                        {
                            var cc = (Control)Activator.CreateInstance(item.CustomCellType);
                            cc.Dock = DockStyle.Fill;
                            c = cc;
                        }
                    }
                    panCells.Controls.Add(c, i, 0);
                }
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }

        private void Box_CheckedChangeEvent(object sender, EventArgs e)
        {
            IsChecked = ((UxCheckBox)sender).Checked;
        }

        /// <summary>
        /// Handles the MouseDown event of the panLeft control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void PanLeft_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!IsOpened.HasValue)
                    return;
                ControlHelper.FreezeControl(FindForm(), true);
                if (!IsOpened.Value)
                {

                    IsOpened = !IsOpened;
                    if (ChildrenRows.Count > 0)
                    {
                        ChildrenRows.ForEach(p =>
                        {
                            p.IsChecked = IsChecked;
                            p.Visible = true;
                        });
                    }
                    else
                    {
                        var proChildrens = DataSource.GetType().GetProperty("Childrens");
                        if (proChildrens == null) return;
                        var value = proChildrens.GetValue(DataSource, null);
                        var intSourceCount = value switch
                        {
                            DataTable table => table.Rows.Count,
                            IList list => list.Count,
                            _ => 0
                        };
                        for (var i = intSourceCount - 1; i >= 0; i--)
                        {
                            var row = new UxDataGridViewTreeRow();
                            if (value is DataTable table)
                            {
                                row.DataSource = table.Rows[i];
                            }
                            else
                            {
                                row.DataSource = (value as IList)?[i];
                            }
                            row.RowLevel = RowLevel + 1;
                            row.Columns = Columns;
                            row.IsShowCheckBox = IsShowCheckBox;
                            row.ReloadCells();
                            row.BindingCellData();

                            Control rowControl = row;
                            row.RowHeight = _rowHeight;
                            rowControl.Width = Width;
                            //rowControl.Dock = DockStyle.Top;
                            row.CellClick += (a, b) => { CellClick?.Invoke(rowControl, b); };
                            row.CheckBoxChangeEvent += (a, b) => { CheckBoxChangeEvent?.Invoke(rowControl, b); };
                            row.RowCustomEvent += (a, b) =>
                            {
                                RowCustomEvent?.Invoke(a, b);
                            };
                            row.SourceChanged += SourceChanged;
                            ChildrenRows.Add(row);
                            row.RowIndex = ChildrenRows.IndexOf(row);

                            Parent.Controls.Add(rowControl);
                            var index = Parent.Controls.GetChildIndex(this);
                            Parent.Controls.SetChildIndex(row, index + 1);

                        }
                    }
                }
                else
                {
                    HideChildrenRows(this);
                    Height = _rowHeight;
                }
            }
            finally
            {
                ControlHelper.FreezeControl(FindForm(), false);
            }
        }

        private static void HideChildrenRows(UxDataGridViewTreeRow row)
        {
            if (row.ChildrenRows.Count <= 0) return;
            foreach (var item in row.ChildrenRows)
            {
                HideChildrenRows(item);
                item.Hide();
            }
            row.IsOpened = false;
        }
    }
}
