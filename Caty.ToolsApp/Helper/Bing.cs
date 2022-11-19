using Caty.ToolsApp.Model.Bing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Caty.ToolsApp.Helper;

internal static class Bing
{
    /// <summary>
    /// 获取必应图片
    /// </summary>
    /// <returns>必应图片URL</returns>
    public static string GetBingImageUrlAsync()
    {
        using var client = new HttpClient();
        var json = client.GetStringAsync("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1").Result;

        var bingImage =   json.ToObject<BingImage>(new Json.OptionConfig());
        //得到背景图片URL
        return $"https://cn.bing.com{bingImage?.Images[0].Url}";
    }

    /// <summary>
    /// 下载图片并存储到临时文件夹下
    /// </summary>
    /// <param name="url">图片URL</param>
    /// <returns>保存下载图片文件的路径</returns>
    public static string DownloadImageAndSaveFile(string url)
    {
        using var client = new HttpClient();
        //创建临时文件目录下的存储必应图片的绝对路径
        var filePath = Path.Combine(Path.GetTempPath(), "bing.jpg");
        //将图片下载到这个路径下
        var responseMessage = client.GetAsync(url).Result;
        if (!responseMessage.IsSuccessStatusCode) return string.Empty;
        using var fs = File.Create(filePath);
        var streamFromService = responseMessage.Content.ReadAsStreamAsync().Result;
        streamFromService.CopyTo(fs);
        //返回当前图片路径
        return filePath;
    }

    /// <summary>
    /// Windows系统函数 - DllImport：using System.Runtime.InteropServices;
    /// </summary>
    [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
    public static extern int SystemParametersInfo(
        int uAction,
        int uParam,
        string lpvParam,
        int fuWinIni
    );

}