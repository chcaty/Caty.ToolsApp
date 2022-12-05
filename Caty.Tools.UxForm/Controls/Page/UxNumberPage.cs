using System.ComponentModel;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.Page;

[ToolboxItem(true)]
public partial class UxNumberPage : UxPageBase
{
    public UxNumberPage()
    {
        InitializeComponent();
        txtPage.txtInput.KeyDown += txtInput_KeyDown;
    }

    private void txtInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        btnToPage_BtnClick(null, null);
        txtPage.InputText = "";
    }

    public override event PageControlEventHandler ShowSourceChanged;

    private List<object> _mDataSource;
    public override List<object> DataSource
    {
        get => _mDataSource;
        set
        {
            _mDataSource = value ?? new List<object>();
            ResetPageCount();
        }
    }
    private int _mIntPageSize = 0;
    public override int PageSize
    {
        get => _mIntPageSize;
        set
        {
            _mIntPageSize = value;
            ResetPageCount();
        }
    }

    public override void FirstPage()
    {
        if (PageIndex == 1)
            return;
        PageIndex = 1;
        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();
        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }

    public override void PreviousPage()
    {
        if (PageIndex <= 1)
        {
            return;
        }
        PageIndex--;

        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();
        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }

    public override void NextPage()
    {
        if (PageIndex >= PageCount)
        {
            return;
        }
        PageIndex++;
        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();
        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }

    public override void EndPage()
    {
        if (PageIndex == PageCount)
            return;
        PageIndex = PageCount;
        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();
        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }

    private void ResetPageCount()
    {
        if (PageSize > 0)
        {
            PageCount = _mDataSource.Count / _mIntPageSize + (_mDataSource.Count % _mIntPageSize > 0 ? 1 : 0);
        }
        txtPage.MaxValue = PageCount;
        txtPage.MinValue = 1;
        ReloadPage();
    }

    private void ReloadPage()
    {
        try
        {
            ControlHelper.FreezeControl(tableLayoutPanel1, true);
            var lst = new List<int>();

            if (PageCount <= 9)
            {
                for (var i = 1; i <= PageCount; i++)
                {
                    lst.Add(i);
                }
            }
            else
            {
                if (PageIndex <= 6)
                {
                    for (var i = 1; i <= 7; i++)
                    {
                        lst.Add(i);
                    }
                    lst.Add(-1);
                    lst.Add(PageCount);
                }
                else if (PageIndex > PageCount - 6)
                {
                    lst.Add(1);
                    lst.Add(-1);
                    for (var i = PageCount - 6; i <= PageCount; i++)
                    {
                        lst.Add(i);
                    }
                }
                else
                {
                    lst.Add(1);
                    lst.Add(-1);
                    var begin = PageIndex - 2;
                    var end = PageIndex + 2;
                    if (end > PageCount)
                    {
                        end = PageCount;
                        begin = end - 4;
                        if (PageIndex - begin < 2)
                        {
                            begin -= 1;
                        }
                    }
                    else if (end + 1 == PageCount)
                    {
                        end = PageCount;
                    }
                    for (var i = begin; i <= end; i++)
                    {
                        lst.Add(i);
                    }
                    if (end != PageCount)
                    {
                        lst.Add(-1);
                        lst.Add(PageCount);
                    }
                }
            }

            for (var i = 0; i < 9; i++)
            {
                var c = (UxButtonBase)tableLayoutPanel1.Controls.Find("p" + (i + 1), false)[0];
                if (i >= lst.Count)
                {
                    c.Visible = false;
                }
                else
                {
                    if (lst[i] == -1)
                    {
                        c.BtnText = "...";
                        c.Enabled = false;
                    }
                    else
                    {
                        c.BtnText = lst[i].ToString();
                        c.Enabled = true;
                    }
                    c.Visible = true;
                    c.RectColor = lst[i] == PageIndex ? Color.FromArgb(255, 77, 59) : Color.FromArgb(223, 223, 223);
                }
            }
            ShowBtn(PageIndex > 1, PageIndex < PageCount);
        }
        finally
        {
            ControlHelper.FreezeControl(tableLayoutPanel1, false);
        }
    }

    private void page_BtnClick(object sender, EventArgs e)
    {
        PageIndex = Convert.ToInt32((sender as UxButtonBase).BtnText);
        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();

        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }

    protected override void ShowBtn(bool blnLeftBtn, bool blnRightBtn)
    {
        btnFirst.Enabled = btnPrevious.Enabled = blnLeftBtn;
        btnNext.Enabled = btnEnd.Enabled = blnRightBtn;
    }

    private void btnFirst_BtnClick(object sender, EventArgs e)
    {
        FirstPage();
    }

    private void btnPrevious_BtnClick(object sender, EventArgs e)
    {
        PreviousPage();
    }

    private void btnNext_BtnClick(object sender, EventArgs e)
    {
        NextPage();
    }

    private void btnEnd_BtnClick(object sender, EventArgs e)
    {
        EndPage();
    }

    private void btnToPage_BtnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPage.InputText)) return;
        PageIndex = Convert.ToInt32(txtPage.InputText);
        StartIndex = (PageIndex - 1) * PageSize;
        ReloadPage();
        var s = GetCurrentSource();
        if (ShowSourceChanged != null)
        {
            ShowSourceChanged(s);
        }
    }
}