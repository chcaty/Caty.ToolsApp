using Caty.Tools.UxForm.Properties;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Caty.Tools.UxForm.Controls;

public partial class UxTreeView : TreeView
{
    private const int WS_VSCROLL = 2097152;

    private const int GWL_STYLE = -16;

    private int _nodeHeight = 50;

    private SizeF _treeFontSize = SizeF.Empty;

    private bool _blnHasVBar;

    public Dictionary<string, string> LstTips { get; set; } = new();

    [Category("自定义属性"), Description("角标文字字体")]
    public Font TipFont { get; set; } = new Font("Arial Unicode MS", 12f);

    [Category("自定义属性"), Description("是否显示角标")]
    public Image TipImage { get; set; } = Resources.tips;

    [Category("自定义属性"), Description("是否显示角标")]
    public bool IsShowTip { get; set; } = false;

    [Category("自定义属性"), Description("使用自定义模式")]
    public bool IsShowByCustomModel { get; set; } = true;

    [Category("自定义属性"), Description("节点高度（IsShowByCustomModel=true时生效）")]
    public int NodeHeight
    {
        get => _nodeHeight;
        set
        {
            _nodeHeight = value;
            ItemHeight = value;
        }
    }

    [Category("自定义属性"), Description("下翻图标（IsShowByCustomModel=true时生效）")]
    public Image NodeDownPic { get; set; } = Resources.list_add;

    [Category("自定义属性"), Description("上翻图标（IsShowByCustomModel=true时生效）")]
    public Image NodeUpPic { get; set; } = Resources.list_subtract;

    [Category("自定义属性"), Description("节点背景颜色（IsShowByCustomModel=true时生效）")]
    public Color NodeBackgroundColor { get; set; } = Color.FromArgb(61, 60, 66);

    [Category("自定义属性"), Description("节点字体颜色（IsShowByCustomModel=true时生效）")]
    public Color NodeForeColor { get; set; } = Color.White;

    [Category("自定义属性"), Description("节点是否显示分割线（IsShowByCustomModel=true时生效）")]
    public bool NodeIsShowSplitLine { get; set; }

    [Category("自定义属性"), Description("节点分割线颜色（IsShowByCustomModel=true时生效）")]
    public Color NodeSplitLineColor { get; set; } = Color.FromArgb(54, 53, 58);

    [Category("自定义属性"), Description("选中节点背景颜色（IsShowByCustomModel=true时生效）")]
    public Color NodeSelectedColor { get; set; } = Color.FromArgb(255, 121, 74);

    [Category("自定义属性"), Description("选中节点字体颜色（IsShowByCustomModel=true时生效）")]
    public Color NodeSelectedForeColor { get; set; } = Color.White;

    [Category("自定义属性"), Description("父节点是否可选中")]
    public bool ParentNodeCanSelect { get; set; } = true;

    public UxTreeView()
    {
        HideSelection = false;
        DrawMode = TreeViewDrawMode.OwnerDrawAll;
        DrawNode += TreeView_DrawNode;
        NodeMouseClick += UxTreeView_NodeMouseClick;
        SizeChanged += UxTreeView_SizeChanged;
        AfterSelect += UxTreeView_AfterSelect;
        FullRowSelect = true;
        ShowLines = false;
        ShowPlusMinus = false;
        ShowRootLines = false;
        BackColor = Color.FromArgb(61, 60, 66);
        DoubleBuffered = true;
    }

    protected override void WndProc(ref Message m)
    {
        // 禁掉清除背景消息WM_ERASEBKGND
        if (m.Msg == 0x0014)
            return;
        base.WndProc(ref m);
    }

    private void UxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        try
        {
            if (e.Node == null) return;
            if (ParentNodeCanSelect) return;
            if (e.Node.Nodes.Count <= 0) return;
            e.Node.Expand();
            SelectedNode = e.Node.FirstNode;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UxTreeView_SizeChanged(object sender, EventArgs e)
    {
        Refresh();
    }

    private void UxTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        try
        {
            if (e.Node == null) return;
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }
            }

            if (SelectedNode == null) return;
            if (SelectedNode != e.Node || !e.Node.IsExpanded) return;
            if (ParentNodeCanSelect) return;
            if (e.Node.Nodes.Count > 0)
            {
                SelectedNode = e.Node.FirstNode;
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
        try
        {
            if (e.Node == null || !IsShowByCustomModel || (e.Node.Bounds.Width <= 0 &&
                                                           e.Node.Bounds.Height <= 0 && e.Node.Bounds.X <= 0 &&
                                                           e.Node.Bounds.Y <= 0))
            {
                e.DrawDefault = true;
            }
            else
            {
                if (Nodes.IndexOf(e.Node) == 0)
                {
                    _blnHasVBar = IsVerticalScrollBarVisible();
                }

                var font = e.Node.NodeFont ?? ((TreeView)sender).Font;

                if (_treeFontSize == SizeF.Empty)
                {
                    _treeFontSize = GetFontSize(font, e.Graphics);
                }

                var flag = false;
                var num = 0;
                if (ImageList != null && ImageList.Images.Count > 0 && e.Node.ImageIndex >= 0 &&
                    e.Node.ImageIndex < ImageList.Images.Count)
                {
                    flag = true;
                    num = (e.Bounds.Height - ImageList.ImageSize.Height) / 2;
                }

                if ((e.State is TreeNodeStates.Selected or TreeNodeStates.Focused ||
                     e.State == (TreeNodeStates.Focused | TreeNodeStates.Selected)) &&
                    (ParentNodeCanSelect || e.Node.Nodes.Count <= 0))
                {
                    e.Graphics.FillRectangle(new SolidBrush(NodeSelectedColor),
                        new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, font, new SolidBrush(NodeSelectedForeColor),
                        e.Bounds.X,
                        e.Bounds.Y + (_nodeHeight - _treeFontSize.Height) / 2f);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(NodeBackgroundColor),
                        new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(Width, e.Node.Bounds.Height)));
                    e.Graphics.DrawString(e.Node.Text, font, new SolidBrush(NodeForeColor), e.Bounds.X,
                        e.Bounds.Y + (_nodeHeight - _treeFontSize.Height) / 2f);
                }

                if (flag)
                {
                    var num2 = e.Bounds.X - num - ImageList.ImageSize.Width;
                    if (num2 < 0)
                    {
                        num2 = 3;
                    }

                    e.Graphics.DrawImage(ImageList.Images[e.Node.ImageIndex],
                        new Rectangle(new Point(num2, e.Bounds.Y + num), ImageList.ImageSize));
                }

                if (NodeIsShowSplitLine)
                {
                    e.Graphics.DrawLine(new Pen(NodeSplitLineColor, 1f),
                        new Point(0, e.Bounds.Y + _nodeHeight - 1),
                        new Point(Width, e.Bounds.Y + _nodeHeight - 1));
                }

                var flag2 = false;
                if (e.Node.Nodes.Count > 0)
                {
                    if (e.Node.IsExpanded && NodeUpPic != null)
                    {
                        e.Graphics.DrawImage(NodeUpPic,
                            new Rectangle(Width - (_blnHasVBar ? 50 : 30),
                                e.Bounds.Y + (_nodeHeight - 20) / 2, 20, 20));
                    }
                    else if (NodeDownPic != null)
                    {
                        e.Graphics.DrawImage(NodeDownPic,
                            new Rectangle(Width - (_blnHasVBar ? 50 : 30),
                                e.Bounds.Y + (_nodeHeight - 20) / 2, 20, 20));
                    }

                    flag2 = true;
                }

                if (!IsShowTip || !LstTips.ContainsKey(e.Node.Name) ||
                    string.IsNullOrWhiteSpace(LstTips[e.Node.Name])) return;
                var num3 = Width - (_blnHasVBar ? 50 : 30) - (flag2 ? 20 : 0);
                var num4 = e.Bounds.Y + (_nodeHeight - 20) / 2;
                e.Graphics.DrawImage(TipImage, new Rectangle(num3, num4, 20, 20));
                var sizeF = e.Graphics.MeasureString(LstTips[e.Node.Name], TipFont, 100,
                    StringFormat.GenericTypographic);
                e.Graphics.DrawString(LstTips[e.Node.Name], TipFont, new SolidBrush(Color.White),
                    num3 + 10 - sizeF.Width / 2f - 3f, num4 + 10 - sizeF.Height / 2f);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private SizeF GetFontSize(Font font, Graphics graphics = null)
    {
        SizeF result;
        try
        {
            var flag = false;
            if (graphics == null)
            {
                graphics = CreateGraphics();
                flag = true;
            }

            var sizeF = graphics.MeasureString("a", font, 100, StringFormat.GenericTypographic);

            if (flag)
            {
                graphics.Dispose();
            }

            result = sizeF;
        }
        catch (Exception e)
        {
            throw e;
        }

        return result;
    }

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

    private bool IsVerticalScrollBarVisible()
    {
        return base.IsHandleCreated && (GetWindowLong(base.Handle, -16) & 2097152) != 0;
    }
}