namespace Caty.Tools.Model.Bing
{
    public class BingImage
    {
        public List<ImageInfo> Images { get; set; }
        public ToolTips ToolTips { get; set; }
    }

    public class ImageInfo
    {
        public string Startdate { get; set; }
        public string Fullstartdate { get; set; }
        public string EndDate { get; set; }
        public string Url { get; set; }
        public string Urlbase { get; set; }
        public string Copyright { get; set; }
        public string Copyrightlink { get; set; }
        public string Title { get; set; }
        public string Quiz { get; set; }
        public bool Wp { get; set; }
        public string Hsh { get; set; }
        public int Drk { get; set; }
        public int Top { get; set; }
        public int Bot { get; set; }
    }

    public class ToolTips
    {
        public string Loading { get; set; }
        public string previous { get; set; }
        public string next { get; set; }
        public string walle { get; set; }
        public string walls { get; set; }
    }
}
