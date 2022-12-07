using System.Drawing.Drawing2D;

namespace Caty.Tools.UxForm.Controls;

public partial class UxCrumbNavigation : UserControl
{
    private Color _navColor = Color.FromArgb(100, 100, 100);

    public Color NavColor
    {
        get => _navColor;
        set
        {
            if (value == Color.Empty || value == Color.Transparent)
                return;
            _navColor = value;
            Refresh();
        }
    }


    private string[] _navigations = { "目录1", "目录2", "目录3" };
    private GraphicsPath[] _paths;

    public string[] Navigations
    {
        get => _navigations;
        set
        {
            _navigations = value;
            _paths = value == null ? Array.Empty<GraphicsPath>() : new GraphicsPath[value.Length];
            Refresh();
        }
    }

    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font = value;
            Refresh();
        }
    }

    public override Color ForeColor
    {
        get => base.ForeColor;
        set
        {
            base.ForeColor = value;
            Refresh();
        }
    }

    public UxCrumbNavigation()
    {
        InitializeComponent();
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
        MouseDown += UxCrumbNavigation_MouseDown;
    }

    private void UxCrumbNavigation_MouseDown(object sender, MouseEventArgs e)
    {
        if (DesignMode) return;
        if (_paths is not { Length: > 0 }) return;
        for (var i = 0; i < _paths.Length; i++)
        {
            if (_paths[i].IsVisible(e.Location))
            {
                Forms.FrmTips.ShowTipsSuccess(FindForm(), _navigations[i]);
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_navigations is not { Length: > 0 }) return;
        var g = e.Graphics;
        var intLastX = 0;
        var intLength = _navigations.Length;
        for (var i = 0; i < _navigations.Length; i++)
        {
            var path = new GraphicsPath();
            var strText = _navigations[i];
            var sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
            var intTextWidth = (int)sizeF.Width + 1;
            path.AddLine(new Point(intLastX + 1, 1),
                new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth, 1));
            path.AddLine(new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth, 1),
                new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth + 10, Height / 2));
            path.AddLine(new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth + 10, Height / 2),
                new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth - 1, Height - 1));

            path.AddLine(new Point(intLastX + 1 + (i == 0 ? 0 : 10) + intTextWidth, Height - 1),
                new Point(intLastX + 1, Height - 1));

            if (i != 0)
            {
                path.AddLine(new Point(intLastX, Height - 1),
                    new Point(intLastX + 1 + 10, Height / 2));
                path.AddLine(new Point(intLastX + 1 + 10, Height / 2), new Point(intLastX + 1, 1));
            }
            else
            {
                path.AddLine(new Point(intLastX + 1, Height - 1), new Point(intLastX + 1, 1));
            }

            g.FillPath(new SolidBrush(_navColor), path);

            g.DrawString(strText, Font, new SolidBrush(ForeColor),
                new PointF(intLastX + 2 + (i == 0 ? 0 : 10), (Height - sizeF.Height) / 2 + 1));
            _paths[i] = path;
            intLastX += ((i == 0 ? 0 : 10) + intTextWidth + (i == (intLength - 1) ? 0 : 10));
        }

    }
}