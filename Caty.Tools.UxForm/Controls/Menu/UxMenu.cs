using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.Menu;

public partial class UxMenu : UserControl
{
    /// <summary>
    /// 选中项事件
    /// </summary>
    public event EventHandler? SelectedItem;

    private Type _parentItemType = typeof(UxMenuParentItem);
    /// <summary>
    /// 父类节点类型
    /// </summary>
    public Type? ParentItemType
    {
        get => _parentItemType;
        set
        {
            if(value ==null)
                return;
            if(!typeof(IMenuItem).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("节点控件没有实现IMenuItem接口");
            _parentItemType = value;
        }
    }

    private Type _childrenItemType = typeof(UxMenuChildrenItem);
    public Type? ChildrenItemType
    {
        get => _childrenItemType;
        set
        {
            if (value == null)
                return;
            if (!typeof(IMenuItem).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("节点控件没有实现IMenuItem接口");
            _childrenItemType = value;
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

    private List<MenuItemEntity> _dataSource;
    /// <summary>
    /// 数据源
    /// </summary>
    public List<MenuItemEntity> DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
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

    private readonly List<Control> _parentItems = new();
    private IMenuItem? _selectParentItem;
    private IMenuItem? _selectChildrenItem;
    private Panel? _panChildren;

    private void ReloadItems()
    {
        try
        {
            ControlHelper.FreezeControl(this, true);
            Controls.Clear();
            _parentItems.Clear();
            if (_dataSource is { Count: > 0 })
            {
                foreach (var parent in _dataSource)
                {
                    var parentItem = (IMenuItem)Activator.CreateInstance(_parentItemType);
                    parentItem.DataSource = parent;
                    if (ParentItemStyles != null)
                        parentItem.SetStyle(ParentItemStyles);
                    parentItem.SelectedItem += ParentItem_SelectedItem;
                    var c = parentItem as Control;
                    c.Dock = DockStyle.Top;
                    Controls.Add(c);
                    Controls.SetChildIndex(c, 0);
                    _parentItems.Add(c);
                }
            }
            _panChildren = new Panel();
            if (MenuStyle == MenuStyle.Fill)
            {
                _panChildren.Dock = DockStyle.Fill;
                _panChildren.Height = 0;
            }
            else
            {
                _panChildren.Dock = DockStyle.Top;
                _panChildren.Height = 0;
            }
            _panChildren.AutoScroll = true;
            Controls.Add(_panChildren);
        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }

        if (IsShowFirstItem && _parentItems is { Count: > 0 })
        {
            ParentItem_SelectedItem(_parentItems[0], null);
        }
    }

    private void ParentItem_SelectedItem(object? sender, EventArgs e)
    {
        FindForm().ActiveControl = this;
        var item = sender as IMenuItem;
        if (_parentItems.Contains(sender as Control))
        {
            if (_selectParentItem != item)
            {
                _selectParentItem?.SetSelectedStyle(false);
                _selectParentItem = item;
                _selectParentItem?.SetSelectedStyle(true);
                SetChildrenControl(_selectParentItem);
            }
            else
            {
                _selectParentItem.SetSelectedStyle(false);
                _selectParentItem = null;
                SetChildrenControl(_selectParentItem, false);
            }
        }
        else if (_panChildren.Controls.Contains(sender as Control))
        {
            if (_selectChildrenItem != item)
            {
                _selectChildrenItem?.SetSelectedStyle(false);
                _selectChildrenItem = item;
                _selectChildrenItem?.SetSelectedStyle(true);
            }
        }

        SelectedItem?.Invoke(sender, e);
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
                    var index = _parentItems.IndexOf(cMenu);
                    for (var i = 0; i <= index; i++)
                    {
                        _parentItems[i].Dock = DockStyle.Top;
                        Controls.SetChildIndex(_parentItems[i], 1);
                    }

                    for (var i = index + 1; i < _parentItems.Count; i++)
                    {
                        _parentItems[i].Dock = DockStyle.Bottom;
                        Controls.SetChildIndex(_parentItems[i], _parentItems.Count);
                    }

                    _panChildren.Controls.Clear();
                    var intItemHeight = 0;
                    foreach (var item in menuitem.DataSource.Childrens)
                    {
                        var parentItem = (IMenuItem)Activator.CreateInstance(_childrenItemType);
                        parentItem.DataSource = item;
                        if (ChildrenItemStyles != null)
                            parentItem.SetStyle(ChildrenItemStyles);
                        parentItem.SelectedItem += ParentItem_SelectedItem;
                        var c = parentItem as Control;
                        if (intItemHeight == 0)
                            intItemHeight = c.Height;
                        c.Dock = DockStyle.Top;
                        _panChildren.Controls.Add(c);
                        _panChildren.Controls.SetChildIndex(c, 0);
                    }
                }

                else
                {
                    _panChildren.Controls.Clear();
                    foreach (var item in _parentItems)
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
                    var index = _parentItems.IndexOf(cMenu);
                    Controls.SetChildIndex(_panChildren, _parentItems.Count - index - 1);
                    _panChildren.Controls.Clear();
                    var intItemHeight = 0;
                    foreach (var item in menuitem.DataSource.Childrens)
                    {
                        var parentItem = (IMenuItem)Activator.CreateInstance(_childrenItemType);
                        parentItem.DataSource = item;
                        if (ChildrenItemStyles != null)
                            parentItem.SetStyle(ChildrenItemStyles);
                        parentItem.SelectedItem += ParentItem_SelectedItem;
                        var c = parentItem as Control;
                        if (intItemHeight == 0)
                            intItemHeight = c.Height;
                        c.Dock = DockStyle.Top;
                        _panChildren.Controls.Add(c);
                        _panChildren.Controls.SetChildIndex(c, 0);
                    }
                    _panChildren.Height = menuitem.DataSource.Childrens.Count * intItemHeight;
                }
                else
                {
                    _panChildren.Controls.Clear();
                    _panChildren.Height = 0;
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