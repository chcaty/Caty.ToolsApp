using Caty.Tools.UxForm.Controls;
using Caty.Tools.UxForm.Controls.TextBox;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Forms;

public partial class FrmInputs : FrmWithOKCancel1
{
    public string[] Values { get; }
    private readonly Dictionary<int,string> _inputDictionary = new();

    public FrmInputs(string title, string[] inputLabels, Dictionary<string, TextInputType> inputTypeDic = null, 
        Dictionary<string, string?> regexDic = null, Dictionary<string, KeyBoardType> keyBoardDic = null, 
        List<string> inputs = null, Dictionary<string, string> defaultValueDic= null) 
    {
        InitializeComponent();
        Title = title;
        if (inputLabels.Length <= 0)
        {
            throw new Exception("输入数量不能为空");
        }

        try
        {
            Values = new string[inputLabels.Length];
            ControlHelper.FreezeControl(this, true);
            for (var i = inputLabels.Length - 1; i >= 0; i--)
            {
                var panel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 62,
                    Padding = new Padding(10)
                };

                var textBox = new UxTextBox
                {
                    Dock = DockStyle.Fill,
                    IsShowKeyboard = true,
                    IsShowClearBtn = true,
                    Name = $"txt_{i}",
                    TabIndex = i
                };
                if (inputTypeDic != null && inputTypeDic.ContainsKey(inputLabels[i]))
                {
                    textBox.InputType = inputTypeDic[inputLabels[i]];
                    if (textBox.InputType == TextInputType.Regex && regexDic != null && regexDic.ContainsKey(inputLabels[i]))
                    {
                        textBox.RegexPattern = regexDic[inputLabels[i]];
                    }
                }

                if (keyBoardDic != null && keyBoardDic.ContainsKey(inputLabels[i]))
                {
                    textBox.KeyBoardType = keyBoardDic[inputLabels[i]];
                }

                if (inputs != null && inputs.Contains(inputLabels[i]))
                {
                    _inputDictionary[i] = inputLabels[i];
                }

                if (defaultValueDic != null && defaultValueDic.ContainsKey(inputLabels[i]))
                {
                    textBox.InputText = defaultValueDic[inputLabels[i]];
                }
                panel.Controls.Add(textBox);

                var label = new Label
                {
                    Text = inputLabels[i],
                    Padding = new Padding(0, 0, 5, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Width = 120,
                    Dock = DockStyle.Left,
                    Font = new Font("微软雅黑", 12)
                };
                panel.Controls.Add(label);

                var labelText = new Label
                {
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Width = 25,
                    Dock = DockStyle.Right,
                    Font = new Font("微软雅黑", 12),
                    ForeColor = Color.Red
                };
                if (inputs != null && inputs.Contains(inputLabels[i]))
                {
                    labelText.Text = @"*";
                }
                else
                {
                    labelText.Text = "";
                }
                panel.Controls.Add(labelText);
                panel3.Controls.Add(panel);
                ActiveControl = textBox;
            }

            Height = 124 + inputLabels.Length * 62;
        }
        finally
        {
            ControlHelper.FreezeControl(this,false);
        }
    }

    protected override void DoEnter()
    {
        for (var i = 0; i < Values.Length; i++)
        {
            var controls = panel3.Controls.Find("txt_" + i, true);
            if (controls.Length <= 0) continue;
            var textBox = controls[0] as UxTextBox;
            Values[i] = textBox?.InputText;
            if (!_inputDictionary.ContainsKey(i) || !string.IsNullOrWhiteSpace(textBox.InputText)) continue;
            FrmTips.ShowTipsInfo(this, $"[{_inputDictionary[i]}]必须输入。");
            return;
        }
        base.DoEnter();
    }
}