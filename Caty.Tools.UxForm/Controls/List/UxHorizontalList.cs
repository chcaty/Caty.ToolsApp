using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.List;

public partial class UxHorizontalList : UserControl
{
    public UxHorizontalListItem SelectedItem { get; set; }

    public event EventHandler? SelectedItemEvent;

    private int _startItemIndex;

    public bool IsAutoSelectFirst { get; set; } = true;

    private List<KeyValuePair<string, string>>? _dataSource;

    public List<KeyValuePair<string, string>>? DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
            ReloadSource();
        }
    }

    public UxHorizontalList()
    {
        InitializeComponent();
    }

    public void ReloadSource()
    {
        try
        {
            ControlHelper.FreezeControl(this, true);
            panList.SuspendLayout();
            panList.Controls.Clear();
            panList.Width = panMain.Width;
            if (DataSource != null)
            {
                foreach (var pair in DataSource)
                {
                    var item = new UxHorizontalListItem
                    {
                        DataSource = pair
                    };
                    item.SelectedItem += Ux_SelectItem;
                    panList.Controls.Add(item);
                }
            }

            panList.ResumeLayout(true);
            if (panList.Controls.Count > 0)
            {
                panList.Width = panMain.Width + panList.Controls[0].Location.X * -1;
            }

            panList.Location = new Point(0, 0);
            _startItemIndex = 0;
            panRight.Visible = panList.Width > panMain.Width;

            panLeft.Visible = false;
            panList.SendToBack();
            panRight.SendToBack();
            if (IsAutoSelectFirst && DataSource is { Count: > 0 })
            {
                SelectItem((UxHorizontalListItem)panList.Controls[0]);
            }
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

    private void Ux_SelectItem(object sender, EventArgs e)
    {
        SelectItem(sender as UxHorizontalListItem);
    }

    private void SelectItem(UxHorizontalListItem item)
    {
        if (SelectedItem is { IsDisposed: false })
        {
            SelectedItem.SetSelect(false);
        }

        SelectedItem = item;
        SelectedItem.SetSelect(true);
        SelectedItemEvent?.Invoke(item, null);
    }

    private void panLeft_MouseDown(object sender, MouseEventArgs e)
    {
        if (panList.Location.X >= 0)
        {
            panList.Location = new Point(0, 0);
            return;
        }

        for (var i = _startItemIndex; i >= 0; i--)
        {
            if (panList.Controls[i].Location.X < panList.Controls[_startItemIndex].Location.X - panMain.Width)
            {
                _startItemIndex = i + 1;
                break;
            }

            if (i == 0)
            {
                _startItemIndex = 0;
            }
        }

        ResetListLocation();
        panRight.Visible = true;
        panLeft.Visible = panList.Location.X < 0;
        panList.SendToBack();
        panRight.SendToBack();
    }

    private void panRight_MouseDown(object sender, MouseEventArgs e)
    {
        if (panList.Location.X + panList.Width <= panMain.Width)
            return;
        if (panList.Controls.Count <= 0)
            return;
        for (var i = _startItemIndex; i < panList.Controls.Count; i++)
        {
            if (panList.Location.X + panList.Controls[i].Location.X + panList.Controls[i].Width <=
                panMain.Width) continue;
            _startItemIndex = i;
            break;
        }

        ResetListLocation();
        panLeft.Visible = true;
        panRight.Visible = panList.Width + panList.Location.X > panMain.Width;
        panList.SendToBack();
        panRight.SendToBack();
    }

    private void ResetListLocation()
    {
        if (panList.Controls.Count > 0)
        {
            panList.Location = new Point(panList.Controls[_startItemIndex].Location.X * -1, 0);
        }
    }

    public void SetSelect(string key)
    {
        foreach (UxHorizontalListItem item in panList.Controls)
        {
            if (item.DataSource.Key != key) continue;
            SelectItem(item);
            return;
        }
    }
}