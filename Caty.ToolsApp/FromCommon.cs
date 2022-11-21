using System.Diagnostics;
using System.Windows.Forms;
using Caty.ToolsApp.Model.Rss;

namespace Caty.ToolsApp;

internal static class FromCommon
{
    public static void AddLabel(this IEnumerable<RssItem> list, Control control)
    {
        const string name = "lb";
        var i = 0;
        var panelList = new List<Panel>();
        foreach (var index in list)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                Margin = new Padding(3)
            };
            var lb = new LinkLabel
            {
                Name = $"{name}_{i}",
                Text = $@"{index.Title} 发布时间：{index.PublishDate}",
                Dock = DockStyle.Fill,
                AutoSize = false
            };
            i++;
            lb.Links.Add(0, index.Title.Length, index.ContentLink);
            lb.LinkClicked += delegate (object sender, LinkLabelLinkClickedEventArgs args)
            {
                if (args.Link.LinkData is string target && (target.StartsWith("http://") || target.StartsWith("https://")))
                {
                    Process.Start(new ProcessStartInfo { FileName = target, UseShellExecute = true });
                }
            };
            panel.Controls.Add(lb);
            panelList.Add(panel);
        }
        control.Controls.AddRange(panelList.ToArray());
    }
}