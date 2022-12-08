using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Forms;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    [DefaultEvent("BtnClick")]
    public partial class UxDropDownButton : UxButtonImage
    {

        /// <summary>
        /// The FRM anchor
        /// </summary>
        private FrmAnchor _frmAnchor;

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public new event EventHandler? BtnClick;
        /// <summary>
        /// 下拉框高度
        /// </summary>
        /// <value>The height of the drop panel.</value>
        [Description("下拉框高度"), Category("自定义")]
        public int DropPanelHeight { get; set; } = -1;

        /// <summary>
        /// 需要显示的按钮文字
        /// </summary>
        /// <value>The BTNS.</value>
        [Description("需要显示的按钮文字"), Category("自定义")]
        public string[] Btns { get; set; }

        /// <summary>
        /// 按钮字体颜色
        /// </summary>
        /// <value>The color of the BTN fore.</value>
        [Description("按钮字体颜色"), Category("自定义")]
        public override Color BtnForeColor
        {
            get => base.BtnForeColor;
            set
            {
                base.BtnForeColor = value;
                var bit = new Bitmap(12, 10);
                var g = Graphics.FromImage(bit);
                g.SetGDIHigh();
                var path = new GraphicsPath();
                path.AddLines(new[]
                {
                    new Point(1,1),
                    new Point(11,1),
                    new Point(6,10),
                    new Point(1,1)
                });
                g.FillPath(new SolidBrush(value), path);
                g.Dispose();
                lbl.Image = bit;
            }
        }
        public UxDropDownButton()
        {
            InitializeComponent();
            IsShowTips = false;
            lbl.ImageAlign = ContentAlignment.MiddleRight;
            base.BtnClick += UxDropDownBtn_BtnClick;
        }

        /// <summary>
        /// Handles the BtnClick event of the UCDropDownBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UxDropDownBtn_BtnClick(object sender, EventArgs e)
        {
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {
                if (Btns is not { Length: > 0 }) return;
                int intRow;
                var intCom = 1;
                var p = PointToScreen(Location);
                while (true)
                {
                    var intScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if ((p.Y + Height + Btns.Length / intCom * 50 < intScreenHeight || p.Y - Btns.Length / intCom * 50 > 0)
                        && (DropPanelHeight <= 0 || Btns.Length / intCom * 50 <= DropPanelHeight))
                    {
                        intRow = Btns.Length / intCom + (Btns.Length % intCom != 0 ? 1 : 0);
                        break;
                    }
                    intCom++;
                }
                var intWidth = Width / intCom;
                var size = new Size(intCom * intWidth, intRow * 50);
                var ucTime = new UxTimePanel
                {
                    IsShowBorder = true,
                    Size = size,
                    FirstEvent = true,
                    Row = intRow,
                    Column = intCom
                };
                ucTime.SelectSourceEvent += UcTime_SelectSourceEvent;

                var lst = Btns.Select(item => new KeyValuePair<string, string>(item, item)).ToList();
                ucTime.Source = lst;

                _frmAnchor = new FrmAnchor(this, ucTime);
                _frmAnchor.Load += (a, b) => { (a as Form).Size = size; };

                _frmAnchor.Show(FindForm());
            }
            else
            {
                _frmAnchor.Close();
            }
        }
        /// <summary>
        /// Handles the SelectSourceEvent event of the ucTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UcTime_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor is not { IsDisposed: false, Visible: true }) return;
            _frmAnchor.Close();

            BtnClick?.Invoke(sender.ToString(), e);
        }
    }
}
