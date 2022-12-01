using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls
{
    [ToolboxItem(false)]
    public partial class UxPageBase : UserControl,IPageControl
    {
        public UxPageBase()
        {
            InitializeComponent();
        }

        public event PageControlEventHandler? ShowSourceChanged;
        public List<object> DataSource { get; set; }
        public int PageSize { get; set; }
        public int StartIndex { get; set; }
        public void FirstPage()
        {
            throw new NotImplementedException();
        }

        public void PreviousPage()
        {
            throw new NotImplementedException();
        }

        public void NextPage()
        {
            throw new NotImplementedException();
        }

        public void EndPage()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public List<object> GetCurrentSource()
        {
            throw new NotImplementedException();
        }

        public int PageCount { get; set; }
        public int PageIndex { get; set; }
    }
}
