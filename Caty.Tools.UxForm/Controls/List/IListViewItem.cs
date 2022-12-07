using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caty.Tools.UxForm.Controls.List;

public interface IListViewItem
{
    /// 
    /// 数据源
    /// 
    object DataSource { get; set; }

    /// 
    /// 选中项事件
    /// 
    event EventHandler SelectedItemEvent;

    /// 
    /// 选中处理，一般用以更改选中效果
    /// 
    /// 是否选中
    void SetSelected(bool blnSelected);
}