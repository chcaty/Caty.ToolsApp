namespace Caty.Tools.UxForm.Controls.DataGridView;

public class DataGridViewCellEntity
{
    public string Title { get; set; }

    public int Width { get; set; }

    public SizeType WidthType { get; set; }
}

public class DataGridViewEventArgs : EventArgs
{
    public  Control CellControl { get; set; }

    public int CellIndex { get; set; }

    public int RowIndex { get; set; }
}

public delegate  void DataGridViewEventHandler(object sender, DataGridViewEventArgs e);

public class DataGridViewColumnEntity
{
    public string HeadText { get; set; }

    public int Width { get; set; }

    public SizeType WidthType { get; set; }

    public string DataField { get; set; }

    public Func<object,string>Format { get; set; }
}