using System.Diagnostics;
using Caty.ToolsApp.Rss;

namespace Caty.ToolsApp
{
    internal static class FromCommon
    {
        public static void AddLabel(this IEnumerable<RssItem> list, Control control)
        {
            const string name = "lb";
            var i = 0;
            foreach (var index in list)
            {
                var lb = new LinkLabel
                {
                    Name = $"{name}_{i}",
                    Text = $@"{index.Title}",
                    Dock = DockStyle.Top,
                };
                i++;
                lb.Links.Add(0, 100, index.ContentLink);
                lb.LinkClicked += delegate (object sender, LinkLabelLinkClickedEventArgs args)
                {
                    if (args.Link.LinkData is string target && (target.StartsWith("http://") || target.StartsWith("https://")))
                    {
                        Process.Start(new ProcessStartInfo { FileName = target, UseShellExecute = true });
                    }
                };
                control.Controls.Add(lb);
            }
        }
    }
}
