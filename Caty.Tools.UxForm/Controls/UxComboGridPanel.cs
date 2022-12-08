using System.ComponentModel;
using Caty.Tools.UxForm.Controls.DataGridView;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    [ToolboxItem(false)]
    public partial class UxComboGridPanel : UserControl
    {
        /// <summary>
        /// 项点击事件
        /// </summary>
        [Description("项点击事件"), Category("自定义")]
        public event DataGridViewEventHandler? ItemClick;
        /// <summary>
        /// The m row type
        /// </summary>
        private Type _rowType = typeof(UxDataGridViewRow);
        /// <summary>
        /// 行类型
        /// </summary>
        /// <value>The type of the row.</value>
        [Description("行类型"), Category("自定义")]
        public Type RowType
        {
            get => _rowType;
            set
            {
                _rowType = value;
                ucDataGridView1.RowType = _rowType;
            }
        }

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
                ucDataGridView1.Columns = value;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        /// <value>The data source.</value>
        [Description("数据源"), Category("自定义")]
        public List<object> DataSource { get; set; } = null;

        /// <summary>
        /// The string last search text
        /// </summary>
        private string _lastSearchText = string.Empty;
        public UxComboGridPanel()
        {
            InitializeComponent();
            txtSearch.txtInput.TextChanged += TxtInput_TextChanged;
            ucDataGridView1.ItemClick += UcDataGridView1_ItemClick;
            m_page.ShowSourceChanged += Page_ShowSourceChanged;
        }

        /// <summary>
        /// Handles the ItemClick event of the ucDataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
        private void UcDataGridView1_ItemClick(object sender, DataGridViewEventArgs e)
        {
            ItemClick?.Invoke((sender as IDataGridViewRow).DataSource, null);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        /// <summary>
        /// Handles the Load event of the UCComboxGridPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UxComboGridPanel_Load(object sender, EventArgs e)
        {
            m_page.DataSource = DataSource;
            ucDataGridView1.DataSource = m_page.GetCurrentSource();
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_lastSearchText == txtSearch.InputText)
            {
                timer1.Enabled = false;
            }
            else
            {
                _lastSearchText = txtSearch.InputText;
                Search(txtSearch.InputText);
            }
        }

        /// <summary>
        /// Searches the specified string text.
        /// </summary>
        /// <param name="strText">The string text.</param>
        private void Search(string strText)
        {
            m_page.StartIndex = 0;
            if (!string.IsNullOrEmpty(strText))
            {
                strText = strText.ToLower().Trim();
                var lst = DataSource.FindAll(p => _columns.Any(c => (c.Format == null ? (p.GetType().GetProperty(c.DataField).GetValue(p, null).ToStringExt()) : c.Format(p.GetType().GetProperty(c.DataField).GetValue(p, null))).ToLower().Contains(strText)));
                m_page.DataSource = lst;
            }
            else
            {
                m_page.DataSource = DataSource;
            }
            m_page.Reload();
        }

        private void Page_ShowSourceChanged(object currentSource)
        {
            ucDataGridView1.DataSource = currentSource;
        }
    }
}
