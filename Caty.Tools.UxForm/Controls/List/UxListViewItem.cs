namespace Caty.Tools.UxForm.Controls.List;

public partial class UxListViewItem : UxControlBase,IListViewItem
{
    private object _dataSource;

    public object DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
            lblTitle.Text = value.ToString();
        }
    }

    public event EventHandler? SelectedItemEvent;
    public UxListViewItem()
    {
        InitializeComponent();
        lblTitle.MouseDown += lblTitle_MouseDown;
    }

    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
        SelectedItemEvent?.Invoke(this, e);
    }

    public void SetSelected(bool blnSelected)
    {
        FillColor = blnSelected ? Color.FromArgb(255, 247, 245) : Color.White;
        Refresh();
    }
}