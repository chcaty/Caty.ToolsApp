using Caty.Tools.UxForm.Controls.DataGridView;
using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxComboGrid : UxComboBox
    {
        /// <summary>
        /// 表格行类型
        /// </summary>
        /// <value>The type of the grid row.</value>
        [Description("表格行类型"), Category("自定义")]
        public Type GridRowType { get; set; } = typeof(UxDataGridViewRow);

        /// <summary>
        /// The int width
        /// </summary>
        int intWidth;

        /// <summary>
        /// The m columns
        /// </summary>
        private List<DataGridViewColumnEntity> m_columns = null;
        /// <summary>
        /// 表格列
        /// </summary>
        /// <value>The grid columns.</value>
        [Description("表格列"), Category("自定义")]
        public List<DataGridViewColumnEntity> GridColumns
        {
            get => m_columns;
            set
            {
                m_columns = value;
                if (value != null)
                    intWidth = value.Sum(p => p.WidthType == SizeType.Absolute ? p.Width : (p.Width < 80 ? 80 : p.Width));
            }
        }

        /// <summary>
        /// 表格数据源
        /// </summary>
        /// <value>The grid data source.</value>
        [Description("表格数据源"), Category("自定义")]
        public List<object> GridDataSource { get; set; } = null;

        /// <summary>
        /// The m text field
        /// </summary>
        private string _textField;
        /// <summary>
        /// 显示值字段名称
        /// </summary>
        /// <value>The text field.</value>
        [Description("显示值字段名称"), Category("自定义")]
        public string TextField
        {
            get => _textField;
            set
            {
                _textField = value;
                SetText();
            }
        }

        /// <summary>
        /// The select source
        /// </summary>
        private object _selectSource = null;
        /// <summary>
        /// 选中的数据源
        /// </summary>
        /// <value>The select source.</value>
        [Description("选中的数据源"), Category("自定义")]
        public object SelectSource
        {
            get => _selectSource;
            set
            {
                _selectSource = value;
                SetText();
                SelectedChangedEvent?.Invoke(value, null);
            }
        }


        /// <summary>
        /// 选中数据源改变事件
        /// </summary>
        [Description("选中数据源改变事件"), Category("自定义")]
        public new event EventHandler SelectedChangedEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="UxComboGrid" /> class.
        /// </summary>
        public UxComboGrid()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The m uc panel
        /// </summary>
        private UxComboGridPanel _ucPanel = null;
        /// <summary>
        /// The FRM anchor
        /// </summary>
        private Forms.FrmAnchor _frmAnchor;
        /// <summary>
        /// Handles the MouseDown event of the click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        protected override void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_columns is not { Count: > 0 })
                return;
            if (_ucPanel == null)
            {
                var p = this.Parent.PointToScreen(this.Location);
                var intScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                var intHeight = Math.Max(p.Y, intScreenHeight - p.Y - this.Height);
                intHeight -= 100;
                _ucPanel = new UxComboGridPanel();
                _ucPanel.ItemClick += UcPanel_ItemClick;
                _ucPanel.Height = intHeight;
                _ucPanel.Width = intWidth;
                _ucPanel.Columns = m_columns;
                _ucPanel.RowType = GridRowType;
                if (GridDataSource is { Count: > 0 })
                {
                    var height = Math.Min(110 + GridDataSource.Count * 36, _ucPanel.Height);
                    if (height <= 0)
                        height = 100;
                    _ucPanel.Height = height;
                }
            }
            _ucPanel.DataSource = GridDataSource;
            if (_frmAnchor is { IsDisposed: false, Visible: true }) return;
            _frmAnchor = new Forms.FrmAnchor(this, _ucPanel, isNotFocus: false);
            _frmAnchor.Show(this.FindForm());

        }

        /// <summary>
        /// Handles the ItemClick event of the m_ucPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
        private void UcPanel_ItemClick(object sender, DataGridViewEventArgs e)
        {
            _frmAnchor.Hide();
            SelectSource = sender;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            if (string.IsNullOrEmpty(_textField) || _selectSource == null) return;
            var pro = _selectSource.GetType().GetProperty(_textField);
            if (pro != null)
            {
                TextValue = pro.GetValue(_selectSource, null).ToStringExt();
            }
        }
    }
}
