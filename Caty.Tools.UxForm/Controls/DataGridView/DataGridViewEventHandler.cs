using System.Runtime.InteropServices;

namespace Caty.Tools.UxForm.Controls.DataGridView
{
    /// <summary>
    /// Delegate DataGridViewEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
    [Serializable]
    [ComVisible(true)]
    public delegate void DataGridViewEventHandler(object sender, DataGridViewEventArgs e);

    /// <summary>
    /// Delegate DataGridViewRowCustomEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="DataGridViewRowCustomEventArgs" /> instance containing the event data.</param>
    [Serializable]
    [ComVisible(true)]
    public delegate void DataGridViewRowCustomEventHandler(object sender, DataGridViewRowCustomEventArgs e);
}
