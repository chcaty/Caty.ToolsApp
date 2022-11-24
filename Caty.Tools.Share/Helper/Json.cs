using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Caty.Tools.Share.Helper;

public static class Json
{
    /// <summary>
    /// 将对象转换为Json字符串
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="config">转换配置参数</param>
    /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
    public static string ToJson(this object target,
        OptionConfig config,
       bool isConvertToSingleQuotes = false)
    {
        if (target == null)
            return string.Empty;
        var result = JsonSerializer.Serialize(target, config.GetOptions());
        if (isConvertToSingleQuotes)
            result = result.Replace("\"", "'");
        return result;
    }

    public static T? ToObject<T>(this string json, OptionConfig config) where T : class
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;
        var result = JsonSerializer.Deserialize<T>(json, config.GetOptions());
        return result;
    }

    /// <summary>
    /// 转换配置参数
    /// </summary>
    public class OptionConfig
    {
        /// <summary>
        /// 是否忽略null值
        /// </summary>
        public bool IgnoreNullValues { get; set; } = true;

        /// <summary>
        /// 是否忽略只读属性
        /// </summary>
        public bool IgnoreReadOnlyProperties { get; set; } = false;

        /// <summary>
        /// 是否允许和忽略对象或数组中列表末尾多余的逗号
        /// </summary>
        public bool AllowTrailingCommas { get; set; } = true;

        /// <summary>
        /// 允许的最大深度
        /// </summary>
        public int MaxDepth { get; set; } = 0;
        /// <summary>
        /// 属性名称是否使用不区分大小写的比较
        /// </summary>
        public bool PropertyNameCaseInsensitive { get; set; } = false;

        /// <summary>
        /// 是否将属性名称转换为camel格式
        /// </summary>
        public bool FormatToCamelCase { get; set; } = true;

        /// <summary>
        /// 处理注释策略（true允许;false不允许;null忽略/跳过）
        /// </summary>
        public bool? AllowedCommentHandle { get; set; } = false;
        /// <summary>
        /// 是否应使用整齐打印（填充空格和换行符）
        /// </summary>
        public bool WriteIndented { get; set; } = false;

        /// <summary>
        /// 是否使用不安全编码策略（不转义）
        /// </summary>
        public bool UseUnsafeRelaxedJsonEscaping { get; set; } = true;

        public JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = AllowTrailingCommas,
                DefaultIgnoreCondition = IgnoreNullValues
                    ? JsonIgnoreCondition.WhenWritingNull
                    : JsonIgnoreCondition.WhenWritingDefault,
                IgnoreReadOnlyProperties = IgnoreReadOnlyProperties,
                MaxDepth = MaxDepth,
                PropertyNameCaseInsensitive = PropertyNameCaseInsensitive,
                PropertyNamingPolicy = FormatToCamelCase ? JsonNamingPolicy.CamelCase : null,
                ReadCommentHandling = AllowedCommentHandle switch
                {
                    null => JsonCommentHandling.Skip,
                    true => JsonCommentHandling.Allow,
                    false => JsonCommentHandling.Disallow
                },
                WriteIndented = WriteIndented,
                DictionaryKeyPolicy = FormatToCamelCase ? JsonNamingPolicy.CamelCase : null,
                Encoder = UseUnsafeRelaxedJsonEscaping
                    ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    : JavaScriptEncoder.Default
            };
        }
    }

}

