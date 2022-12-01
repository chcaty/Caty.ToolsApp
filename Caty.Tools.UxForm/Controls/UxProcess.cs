namespace Caty.Tools.UxForm.Controls
{
    public partial class UxProcess : UxButtonBase
    {
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                if(value <0)
                    return;
                this._value = value;
                SetValue();
            }
        }

        private int _maxValue = 100;

        public int MaxValue
        {
            get => _maxValue;
            set
            {
                if(value <= 0)
                    return;
                _maxValue = value;
                SetValue();
            }
        }

        public UxProcess()
        {
            InitializeComponent();
        }

        private void SetValue()
        {
            var percent = _value / (double)_maxValue;
            panel1.Width = (int)(Width * percent);
        }

        private void UxProcess_SizeChanged(object sender, EventArgs e)
        {
            SetValue();
        }

        public void Step()
        {
            Value++;
        }
    }
}
