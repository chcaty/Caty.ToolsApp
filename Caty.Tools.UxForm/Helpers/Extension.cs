using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caty.Tools.UxForm.Helpers
{
    public static class Extension
    {
        public static T CloneModel<T>(this T classObject) where T : class
        {
            T result;
            if (classObject == null)
            {
                result = default(T);
            }
            else
            {
                var obj = Activator.CreateInstance(typeof(T));
                var properties = typeof(T).GetProperties();
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.CanWrite)
                        propertyInfo.SetValue(obj, propertyInfo.GetValue(classObject, null), null);
                }
                result = obj as T;
            }
            return result;
        }

        #region 转换为base64字符串
        /// <summary>
        /// 转换为base64字符串
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>返回值</returns>
        public static string ToBase64Str(this string data)
        {
            if (data.IsEmpty())
                return string.Empty;
            var buffer = Encoding.Default.GetBytes(data);
            return Convert.ToBase64String(buffer);
        }
        #endregion
        /// <summary>
        /// 转换为坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Point ToPoint(this string data)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(data, @"^\s*\d+(\.\d+)?\s*\,\s*\d+(\.\d+)?\s*$"))
            {
                return Point.Empty;
            }

            var strs = data.Split(',');
            return new Point(strs[0].ToInt(), strs[1].ToInt());
        }

        #region 数值转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            switch (data)
            {
                case null:
                    return 0;
                case bool b:
                    return b ? 1 : 0;
            }

            var success = int.TryParse(data.ToString(), out var result);
            if (success)
                return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
                return null;
            var isValid = int.TryParse(data.ToString(), out var result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == null)
                return 0;
            return double.TryParse(data.ToString(), out var result) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null)
                return null;
            var isValid = double.TryParse(data.ToString(), out var result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == null)
                return 0;
            return decimal.TryParse(data.ToString(), out var result) ? result : 0;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits, System.MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
                return null;
            var isValid = decimal.TryParse(data.ToString(), out var result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits, System.MidpointRounding.AwayFromZero);
        }

        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            try
            {
                if (data == null)
                    return DateTime.MinValue;
                if (!System.Text.RegularExpressions.Regex.IsMatch(data.ToStringExt(), @"^\d{8}$"))
                    return DateTime.TryParse(data.ToString(), out var result) ? result : DateTime.MinValue;
                var strValue = data.ToStringExt();
                return new DateTime(strValue[..4].ToInt(), strValue.Substring(4, 2).ToInt(), strValue.Substring(6, 2).ToInt());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            try
            {
                if (data == null)
                    return null;
                if (System.Text.RegularExpressions.Regex.IsMatch(data.ToStringExt(), @"^\d{8}$"))
                {
                    var strValue = data.ToStringExt();
                    return new DateTime(strValue[..4].ToInt(), strValue.Substring(4, 2).ToInt(), strValue.Substring(6, 2).ToInt());
                }

                var isValid = DateTime.TryParse(data.ToString(), out var result);
                if (isValid)
                    return result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 布尔转换
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
                return false;
            var value = GetBool(data);
            if (value != null)
                return value.Value;
            return bool.TryParse(data.ToString(), out var result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(this object data)
        {
            switch (data.ToString()?.Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == null)
                return null;
            var value = GetBool(data);
            if (value != null)
                return value.Value;
            var isValid = bool.TryParse(data.ToString(), out var result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 字符串转换
        /// <summary>
        /// 字符串转换为byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string data)
        {
            return Encoding.GetEncoding("GBK").GetBytes(data);
        }
        public static byte[] ToBytesDefault(this string data)
        {
            return Encoding.Default.GetBytes(data);
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToStringExt(this object data)
        {
            return data == null ? string.Empty : data.ToString();
        }
        #endregion

        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            return value == null || IsEmpty(value.Value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            return value == null || string.IsNullOrEmpty(value.ToString());
        }

        #region 是否数字
        /// <summary>
        /// 功能描述:是否数字
        /// 作　　者:HZH
        /// 创建日期:2019-03-06 09:03:05
        /// 任务编号:POS
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>返回值</returns>
        public static bool IsNum(this string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+(\.\d*)?$");
        }
        #endregion
    }
}
