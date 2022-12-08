namespace Caty.Tools.UxForm.Controls.DataGridView;

public class DataGridViewCellEntity
{
    public string? Title { get; set; }

    public int Width { get; set; }

    public SizeType WidthType { get; set; }
}

public class DataGridViewEventArgs : EventArgs
{
    public  Control? CellControl { get; set; }

    public int CellIndex { get; set; }

    public int RowIndex { get; set; }
}

public class DataGridViewColumnEntity
{
    /// <summary>
    /// Gets or sets the head text.
    /// </summary>
    /// <value>The head text.</value>
    public string? HeadText { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>The width.</value>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the type of the width.
    /// </summary>
    /// <value>The type of the width.</value>
    public System.Windows.Forms.SizeType WidthType { get; set; }

    /// <summary>
    /// Gets or sets the data field.
    /// </summary>
    /// <value>The data field.</value>
    public string? DataField { get; set; }

    /// <summary>
    /// Gets or sets the format.
    /// </summary>
    /// <value>The format.</value>
    public Func<object, string>? Format { get; set; }

    /// <summary>
    /// Gets or sets the text align.
    /// </summary>
    /// <value>The text align.</value>
    public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;

    /// <summary>
    /// 自定义的单元格控件，一个实现IDataGridViewCustomCell的Control
    /// </summary>
    /// <value>The custom cell.</value>
    private Type? _customCellType = null;

    public Type? CustomCellType
    {
        get => _customCellType;
        set
        {
            if (!typeof(IDataGridViewCustomCell).IsAssignableFrom(value) ||
                !value.IsSubclassOf(typeof(System.Windows.Forms.Control)))
                throw new Exception("行控件没有实现IDataGridViewCustomCell接口");
            _customCellType = value;
        }
    }
}