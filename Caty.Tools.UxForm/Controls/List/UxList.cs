using System.ComponentModel;
using Caty.Tools.UxForm.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace Caty.Tools.UxForm.Controls.List
{
    [DefaultEvent("ItemClick")]
    public partial class UxList : UserControl
    {
        [Description("标题字体"), Category("自定义")]
        public Font TitleFont { get; set; } = new("微软雅黑", 15F);

        [Description("副标题字体"), Category("自定义")]
        public Font SubTitleFont { get; set; } = new("微软雅黑", 14F);

        [Description("标题背景色"), Category("自定义")]
        public Color TitleBackColor { get; set; } = Color.White;

        [Description("标题选中背景色"), Category("自定义")]
        public Color TitleSelectedBackColor { get; set; } = Color.FromArgb(73, 119, 232);

        [Description("标题文本色"), Category("自定义")]
        public Color TitleForeColor { get; set; } = Color.Black;

        [Description("标题选中文本色"), Category("自定义")]
        public Color TitleSelectedForeColor { get; set; } = Color.White;

        [Description("副标题文本色"), Category("自定义")]
        public Color SubTitleForeColor { get; set; } = Color.FromArgb(73, 119, 232);

        [Description("副标题选中文本色"), Category("自定义")]
        public Color SubTitleSelectedForeColor { get; set; } = Color.White;

        [Description("项高度"), Category("自定义")]
        public int ItemHeight { get; set; } = 60;

        [Description("自动选中第一项"), Category("自定义")]

        public bool AutoSelectFirst { get; set; } = true;

        public delegate void ItemClickEvent(UxListItem item);

        [Description("选中项事件"), Category("自定义")]
        public event ItemClickEvent? ItemClick;

        [Description("选中后是否可以再次触发点击事件"), Category("自定义")]
        public bool SelectedCanClick { get; set; } = false;

        /// <summary>
        /// 选中的节点
        /// </summary>
        public UxListItem? SelectItem { get; private set; }

        public UxList()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer,true);
            UpdateStyles();
        }

        public void SetList(List<ListEntity> list)
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                Controls.Clear();
                SuspendLayout();
                UxListItem? first = null;
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var item = list[i];
                    var now = new UxListItem
                    {
                        Height = ItemHeight,
                        TitleFont = TitleFont,
                        SubTitleFont = SubTitleFont,
                        ItemBackColor = TitleBackColor,
                        TitleForeColor = TitleForeColor,
                        SubTitleForeColor = SubTitleForeColor,
                        Dock = DockStyle.Top
                    };
                    now.SetData(item);
                    now.ItemClick += (s, _) =>
                    {
                        SelectLabel((UxListItem)s);
                    };
                    Controls.Add(now);
                    first = now;
                }
                if(AutoSelectFirst)
                    SelectLabel(first);
                ResumeLayout(false);

                var timer = new Timer
                {
                    Interval = 10
                };
                timer.Tick += (_, _) =>
                {
                    timer.Enabled = false;
                    VerticalScroll.Value = 1;
                    VerticalScroll.Value = 0;
                    Refresh();
                };
                timer.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }

        private void SelectLabel(UxListItem? item)
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                FindForm().ActiveControl = this;
                if (SelectItem != null)
                {
                    if(SelectItem == item && !SelectedCanClick)
                        return;
                    SelectItem.ItemBackColor = TitleBackColor;
                    SelectItem.TitleForeColor = TitleForeColor;
                    SelectItem.SubTitleForeColor = SubTitleForeColor;
                }

                item.ItemBackColor = TitleSelectedBackColor;
                item.TitleForeColor = TitleSelectedForeColor;
                item.SubTitleForeColor = SubTitleForeColor;

                SelectItem = item;
                ItemClick?.Invoke(item);
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }
    }
}
