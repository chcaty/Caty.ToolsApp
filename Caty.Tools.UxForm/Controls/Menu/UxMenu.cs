using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.Menu;

public partial class UxMenu : UserControl
{
    /// <summary>
    /// 选中项事件
    /// </summary>
    public event EventHandler SelectedItem;

    private Type m_parentItemType = typeof(UxMenuParentItem);
    /// <summary>
    /// 父类节点类型
    /// </summary>
    public Type ParentItemType
    {
        get => m_parentItemType;
        set
        {
            if(value ==null)
                return;
            if(!typeof(IMenuItem).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("节点控件没有实现IMenuItem接口");
            m_parentItemType = value;
        }
    }

    private Type m_childrenItemType = typeof(UxMenuChildrenItem);
    public Type ChildrenItemType
    {
        get => m_childrenItemType;
        set
        {
            if (value == null)
                return;
            if (!typeof(IMenuItem).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("节点控件没有实现IMenuItem接口");
            m_childrenItemType = value;
        }
    }

    /// <summary>
    /// 父类节点样式设置，key：属性名称，value：属性值
    /// </summary>
    public Dictionary<string, object> ParentItemStyles { get; set; }

    /// <summary>
    /// 子类节点样式设置，key：属性名称，value：属性值
    /// </summary>
    public Dictionary<string, object> ChildrenItemStyles { get; set; }

    private List<MenuItemEntity> m_dataSource;
    /// <summary>
    /// 数据源
    /// </summary>
    public List<MenuItemEntity> DataSource
    {
        get => m_dataSource;
        set
        {
            m_dataSource = value;
            ReloadItems();
        }
    }

    /// <summary>
    /// 是否自动展开第一个节点
    /// </summary>
    public bool IsShowFirstItem { get; set; } = true;

    /// <summary>
    /// 菜单样式
    /// </summary>
    public MenuStyle MenuStyle { get; set; } = MenuStyle.Fill;

    private List<Control> m_lstParentItems = new();

    private IMenuItem m_selectParentItem = null;
    private IMenuItem m_selectChildrenItem = null;
    private Panel m_panChildren = null;

    private void ReloadItems()
    {
        try
        {
            ControlHelper.FreezeControl(this, true);
            this.Controls.Clear();
            m_lstParentItems.Clear();
            if (m_dataSource is { Count: > 0 })
            {
                foreach (var parent in m_dataSource)
                {
                    var parentItem = (IMenuItem)Activator.CreateInstance(m_parentItemType);
                    parentItem.DataSource = parent;
                    if (ParentItemStyles != null)
                        parentItem.SetStyle(ParentItemStyles);
                    parentItem.SelectedItem += parentItem_SelectedItem;
                    var c = parentItem as Control;
                    c.Dock = DockStyle.Top;
                    Controls.Add(c);
                    Controls.SetChildIndex(c, 0);
                    m_lstParentItems.Add(c);
                }
            }
            m_panChildren = new Panel();
            if (MenuStyle == MenuStyle.Fill)
            {
                m_panChildren.Dock = DockStyle.Fill;
                m_panChildren.Height = 0;
            }
            else
            {
                m_panChildren.Dock = DockStyle.Top;
                m_panChildren.Height = 0;
            }
            m_panChildren.AutoScroll = true;
            Controls.Add(m_panChildren);
        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }

        if (IsShowFirstItem && m_lstParentItems is { Count: > 0 })
        {
            parentItem_SelectedItem(m_lstParentItems[0], null);
        }
    }

    private void parentItem_SelectedItem(object sender, EventArgs e)
    {
        FindForm().ActiveControl = this;
        var item = sender as IMenuItem;
        if (m_lstParentItems.Contains(sender as Control))
        {
            if (m_selectParentItem != item)
            {
                if (m_selectParentItem != null)
                {
                    m_selectParentItem.SetSelectedStyle(false);
                }
                m_selectParentItem = item;
                m_selectParentItem.SetSelectedStyle(true);
                SetChildrenControl(m_selectParentItem);
            }
            else
            {
                m_selectParentItem.SetSelectedStyle(false);
                m_selectParentItem = null;
                SetChildrenControl(m_selectParentItem, false);
            }
        }
        else if (m_panChildren.Controls.Contains(sender as Control))
        {
            if (m_selectChildrenItem != item)
            {
                if (m_selectChildrenItem != null)
                {
                    m_selectChildrenItem.SetSelectedStyle(false);
                }
                m_selectChildrenItem = item;
                m_selectChildrenItem.SetSelectedStyle(true);
            }
        }
        if (SelectedItem != null)
        {
            SelectedItem(sender, e);
        }
    }

    private void SetChildrenControl(IMenuItem menuitem, bool blnChildren = true)
    {
        try
        {
            ControlHelper.FreezeControl(this, true);
            if (MenuStyle == MenuStyle.Fill)
            {
                if (blnChildren)
                {
                    var cMenu = menuitem as Control;
                    var index = m_lstParentItems.IndexOf(cMenu);
                    for (var i = 0; i <= index; i++)
                    {
                        m_lstParentItems[i].Dock = DockStyle.Top;
                        Controls.SetChildIndex(m_lstParentItems[i], 1);
                    }

                    for (var i = index + 1; i < m_lstParentItems.Count; i++)
                    {
                        m_lstParentItems[i].Dock = DockStyle.Bottom;
                        Controls.SetChildIndex(m_lstParentItems[i], m_lstParentItems.Count);
                    }

                    m_panChildren.Controls.Clear();
                    var intItemHeight = 0;
                    foreach (var item in menuitem.DataSource.Childrens)
                    {
                        var parentItem = (IMenuItem)Activator.CreateInstance(m_childrenItemType);
                        parentItem.DataSource = item;
                        if (ChildrenItemStyles != null)
                            parentItem.SetStyle(ChildrenItemStyles);
                        parentItem.SelectedItem += parentItem_SelectedItem;
                        var c = parentItem as Control;
                        if (intItemHeight == 0)
                            intItemHeight = c.Height;
                        c.Dock = DockStyle.Top;
                        m_panChildren.Controls.Add(c);
                        m_panChildren.Controls.SetChildIndex(c, 0);
                    }
                }

                else
                {
                    m_panChildren.Controls.Clear();
                    foreach (var item in m_lstParentItems)
                    {
                        item.Dock = DockStyle.Top;
                        Controls.SetChildIndex(item, 1);
                    }
                }
            }
            else
            {
                if (blnChildren)
                {
                    var cMenu = menuitem as Control;
                    var index = m_lstParentItems.IndexOf(cMenu);
                    Controls.SetChildIndex(m_panChildren, m_lstParentItems.Count - index - 1);
                    m_panChildren.Controls.Clear();
                    var intItemHeigth = 0;
                    foreach (var item in menuitem.DataSource.Childrens)
                    {
                        var parentItem = (IMenuItem)Activator.CreateInstance(m_childrenItemType);
                        parentItem.DataSource = item;
                        if (ChildrenItemStyles != null)
                            parentItem.SetStyle(ChildrenItemStyles);
                        parentItem.SelectedItem += parentItem_SelectedItem;
                        var c = parentItem as Control;
                        if (intItemHeigth == 0)
                            intItemHeigth = c.Height;
                        c.Dock = DockStyle.Top;
                        m_panChildren.Controls.Add(c);
                        m_panChildren.Controls.SetChildIndex(c, 0);
                    }
                    m_panChildren.Height = menuitem.DataSource.Childrens.Count * intItemHeigth;
                }
                else
                {
                    m_panChildren.Controls.Clear();
                    m_panChildren.Height = 0;
                }
            }

        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }
    }
        
    public UxMenu()
    {
        InitializeComponent();
    }
}