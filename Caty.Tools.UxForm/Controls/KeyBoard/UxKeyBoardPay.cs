using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.KeyBoard
{
    public partial class UxKeyBoardPay : UserControl
    {
        [Description("数字点击事件"), Category("自定义")]
        public event EventHandler? NumClick;

        [Description("取消点击事件"), Category("自定义")]
        public event EventHandler? CancelClick;

        [Description("确定点击事件"), Category("自定义")]
        public event EventHandler? OkClick;

        [Description("删除点击事件"), Category("自定义")]
        public event EventHandler? BackspaceClick;

        [Description("金额点击事件"), Category("自定义")]
        public event EventHandler? MoneyClick;
        public UxKeyBoardPay()
        {
            InitializeComponent();
        }

        #region 设置快速付款金额
        /// <summary>
        /// 功能描述:设置快速付款金额
        /// </summary>
        /// <param name="sourceMoney">sourceMoney</param>
        public void SetPayMoney(decimal sourceMoney)
        {
            var list = new List<decimal>();
            var d = Math.Ceiling(sourceMoney);
            if (sourceMoney > 0m)
            {
                switch (sourceMoney)
                {
                    case < 5m:
                        list.Add(5m);
                        list.Add(10m);
                        list.Add(20m);
                        list.Add(50m);
                        break;
                    case < 10m:
                        list.Add(10m);
                        list.Add(20m);
                        list.Add(50m);
                        list.Add(100m);
                        break;
                    default:
                    {
                        var num = Convert.ToInt32(d % 10m);
                        var num2 = Convert.ToInt32(Math.Floor(d / 10m) % 10m);
                        var num3 = Convert.ToInt32(Math.Floor(d / 100m));
                        int num4;
                        if (num < 5)
                        {
                            num4 = num2 * 10 + 5;
                            list.Add(num4 + num3 * 100);
                            num4 = (num2 + 1) * 10;
                            list.Add(num4 + num3 * 100);
                        }
                        else
                        {
                            num4 = (num2 + 1) * 10;
                            list.Add(num4 + num3 * 100);
                        }
                        switch (num4)
                        {
                            case >= 0 and < 10:
                            {
                                num4 = 10;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 20;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 50;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 100;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }

                                break;
                            }
                            case >= 10 and < 20:
                            {
                                num4 = 20;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 50;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 100;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }

                                break;
                            }
                            case >= 20 and < 50:
                            {
                                num4 = 50;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }
                                num4 = 100;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }

                                break;
                            }
                            case < 100:
                            {
                                num4 = 100;
                                if (list.Count < 4)
                                {
                                    list.Add(num4 + num3 * 100);
                                }

                                break;
                            }
                        }

                        break;
                    }
                }
            }
            SetFastMoneyToControl(list);
        }
        #endregion

        private void SetFastMoneyToControl(IReadOnlyList<decimal> values)
        {
            var lbl = new List<Label> { lblFast1, lblFast2, lblFast3, lblFast4 };
            lblFast1.Tag = lblFast1.Text = "";
            lblFast2.Tag = lblFast2.Text = "";
            lblFast3.Tag = lblFast3.Text = "";
            lblFast4.Tag = lblFast4.Text = "";
            for (var i = 0; i < lbl.Count && i < values.Count; i++)
            {
                if (values[i].ToString("0.##").Length < 4)
                {
                    lbl[i].Font = new Font("Arial Unicode MS", 30F);
                }
                else
                {
                    var graphics = lbl[i].CreateGraphics();
                    for (var j = 0; j < 5; j++)
                    {
                        var sizeF = graphics.MeasureString(values[i].ToString("0.##"), new Font("Arial Unicode MS", 30 - j * 5), 100, StringFormat.GenericTypographic);
                        if (!(sizeF.Width <= lbl[i].Width - 20)) continue;
                        lbl[i].Font = new Font("Arial Unicode MS", 30 - j * 5);
                        break;
                    }
                    graphics.Dispose();
                }
                lbl[i].Tag = lbl[i].Text = values[i].ToString("0.##");
            }
        }
        private void Num_MouseDown(object sender, MouseEventArgs e)
        {
            NumClick?.Invoke((sender as Label)?.Tag, e);
        }

        private void Backspace_MouseDown(object sender, MouseEventArgs e)
        {
            BackspaceClick?.Invoke((sender as Label)?.Tag, e);
        }

        private void Cancel_MouseDown(object sender, MouseEventArgs e)
        {
            CancelClick?.Invoke((sender as Label)?.Tag, e);
        }

        private void OK_MouseDown(object sender, MouseEventArgs e)
        {
            OkClick?.Invoke((sender as Label)?.Tag, e);
        }

        private void Money_MouseDown(object sender, MouseEventArgs? e)
        {
            MoneyClick?.Invoke((sender as Label)?.Tag, e);
        }

        public void Money1Click()
        {
            Money_MouseDown(lblFast1, null);
        }

        public void Money2Click()
        {
            Money_MouseDown(lblFast2, null);
        }

        public void Money3Click()
        {
            Money_MouseDown(lblFast3, null);
        }

        public void Money4Click()
        {
            Money_MouseDown(lblFast4, null);
        }
    }
}
