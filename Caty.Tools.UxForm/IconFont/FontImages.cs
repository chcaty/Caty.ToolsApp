using Caty.Tools.UxForm.Helpers;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Reflection;

namespace Caty.Tools.UxForm.IconFont
{
    public static class FontImages
    {
        /// <summary>
        /// The m font collection
        /// </summary>
        private static readonly PrivateFontCollection FontCollection = new();

        /// <summary>
        /// The m fonts awesome
        /// </summary>
        private static readonly Dictionary<string, Font> FontsAwesome = new();
        /// <summary>
        /// The m fonts elegant
        /// </summary>
        private static readonly Dictionary<string, Font> FontsElegant = new();

        /// <summary>
        /// The m cache maximum size
        /// </summary>
        private static readonly Dictionary<int, float> CacheMaxSize = new();
        /// <summary>
        /// The minimum font size
        /// </summary>
        private const int MinFontSize = 8;
        /// <summary>
        /// The maximum font size
        /// </summary>
        private const int MaxFontSize = 43;


        /// <summary>
        /// ¹¹Ôìº¯Êý
        /// </summary>
        /// <exception cref="FileNotFoundException">Font file not found</exception>
        static FontImages()
        {
            var strPath = System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower().Replace("file:///", "");
            var strDir = Path.GetDirectoryName(strPath);
            if (!Directory.Exists(Path.Combine(strDir, "IconFont")))
            {
                Directory.CreateDirectory(Path.Combine(strDir, "IconFont"));
            }
            var strFile = Path.Combine(strDir, "IconFont\\FontAwesome.ttf");
            if (!File.Exists(strFile))
            {
                var fs = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("IconFont.FontAwesome.ttf");
                var sw = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                fs.CopyTo(sw);
                sw.Close();
                fs.Close();
            }

            FontCollection.AddFontFile(strFile);

            float size = MinFontSize;
            for (var i = 0; i <= (MaxFontSize - MinFontSize) * 2; i++)
            {
                FontsAwesome.Add(size.ToString("F2"), new Font(FontCollection.Families[0], size, FontStyle.Regular, GraphicsUnit.Point));
                size += 0.5f;
            }
        }

        /// <summary>
        /// Gets the font awesome.
        /// </summary>
        /// <value>The font awesome.</value>
        public static FontFamily FontAwesome
        {
            get
            {
                lock (FontsElegant)
                {
                    foreach (var t in FontCollection.Families)
                    {
                        if (t.Name == "FontAwesome")
                        {
                            return t;
                        }
                    }
                }

                lock (FontsElegant)
                {
                    return FontCollection.Families[0];
                }
            }
        }

        /// <summary>
        /// Gets the elegant icons.
        /// </summary>
        /// <value>The elegant icons.</value>
        /// <exception cref="FileNotFoundException">Font file not found</exception>
        public static FontFamily ElegantIcons
        {
            get
            {
                if (FontsElegant.Count <= 0)
                {
                    lock (FontsElegant)
                    {
                        if (FontsElegant.Count <= 0)
                        {
                            var strPath = Assembly.GetExecutingAssembly().Location.ToLower().Replace("file:///", "");
                            var strDir = Path.GetDirectoryName(strPath);
                            if (!Directory.Exists(Path.Combine(strDir, "IconFont")))
                            {
                                Directory.CreateDirectory(Path.Combine(strDir, "IconFont"));
                            }
                            var strFile = Path.Combine(strDir, "IconFont\\ElegantIcons.ttf");
                            if (!File.Exists(strFile))
                            {
                                var fs = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("HZH_Controls.IconFont.ElegantIcons.ttf");
                                var sw = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                                fs.CopyTo(sw);
                                sw.Close();
                                fs.Close();
                            }
                            FontCollection.AddFontFile(strFile);

                            float size = MinFontSize;
                            for (var i = 0; i <= (MaxFontSize - MinFontSize) * 2; i++)
                            {
                                FontsElegant.Add(size.ToString("F2"), new Font(FontCollection.Families[0], size, FontStyle.Regular, GraphicsUnit.Point));
                                size += 0.5f;
                            }
                        }
                    }
                }
                lock (FontsElegant)
                {
                    foreach (var t in FontCollection.Families)
                    {
                        if (t.Name == "ElegantIcons")
                        {
                            return t;
                        }
                    }
                }
                lock (FontsElegant)
                {
                    return FontCollection.Families[0];
                }
            }
        }
        /// <summary>
        /// »ñÈ¡Í¼±ê
        /// </summary>
        /// <param name="iconText">Í¼±êÃû³Æ</param>
        /// <param name="imageSize">Í¼±ê´óÐ¡</param>
        /// <param name="foreColor">Ç°¾°É«</param>
        /// <param name="backColor">±³¾°É«</param>
        /// <returns>Í¼±ê</returns>
        public static Icon? GetIcon(FontIcons iconText, int imageSize = 32, Color? foreColor = null, Color? backColor = null)
        {
            var image = GetImage(iconText, imageSize, foreColor, backColor);
            return ToIcon(image, imageSize);
        }
        /// <summary>
        /// »ñÈ¡Í¼±ê.
        /// </summary>
        /// <param name="iconText">Í¼±êÃû³Æ.</param>
        /// <param name="imageSize">Í¼±ê´óÐ¡.</param>
        /// <param name="foreColor">Ç°¾°É«</param>
        /// <param name="backColor">±³¾°É«.</param>
        /// <returns>Bitmap.</returns>
        /// <exception cref="FileNotFoundException">Font file not found</exception>
        public static Bitmap GetImage(FontIcons iconText, int imageSize = 32, Color? foreColor = null, Color? backColor = null)
        {
            Dictionary<string, Font> _fs;
            if (iconText.ToString().StartsWith("A_"))
                _fs = FontsAwesome;
            else
            {
                if (FontsElegant.Count <= 0)
                {
                    lock (FontsElegant)
                    {
                        if (FontsElegant.Count <= 0)
                        {
                            var strPath = Assembly.GetExecutingAssembly().Location.ToLower().Replace("file:///", "");
                            var strDir = Path.GetDirectoryName(strPath);
                            if (!Directory.Exists(Path.Combine(strDir, "IconFont")))
                            {
                                Directory.CreateDirectory(Path.Combine(strDir, "IconFont"));
                            }
                            var strFile = Path.Combine(strDir, "IconFont\\ElegantIcons.ttf");
                            if (!File.Exists(strFile))
                            {
                                var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("HZH_Controls.IconFont.ElegantIcons.ttf");
                                var sw = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                                fs.CopyTo(sw);
                                sw.Close();
                                fs.Close();
                            }
                            FontCollection.AddFontFile(strFile);

                            float size = MinFontSize;
                            for (var i = 0; i <= (MaxFontSize - MinFontSize) * 2; i++)
                            {
                                FontsElegant.Add(size.ToString("F2"), new Font(FontCollection.Families[0], size, FontStyle.Regular, GraphicsUnit.Point));
                                size += 0.5f;
                            }
                        }
                    }
                }
                _fs = FontsElegant;
            }

            foreColor ??= Color.Black;
            var imageFont = _fs[MinFontSize.ToString("F2")];
            var textSize = new SizeF(imageSize, imageSize);
            using (var bitmap = new Bitmap(48, 48))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                //float size = MaxFontSize;
                float fltMaxSize = MaxFontSize;
                if (CacheMaxSize.ContainsKey(imageSize))
                {
                    fltMaxSize = Math.Max(MaxFontSize, CacheMaxSize[imageSize] + 5);
                }
                while (fltMaxSize >= MinFontSize)
                {
                    var font = _fs[fltMaxSize.ToString("F2")];
                    SizeF sf = GetIconSize(iconText, graphics, font);
                    if (sf.Width < imageSize && sf.Height < imageSize)
                    {
                        imageFont = font;
                        textSize = sf;
                        break;
                    }

                    fltMaxSize -= 0.5f;
                }

                if (!CacheMaxSize.ContainsKey(imageSize) || (CacheMaxSize.ContainsKey(imageSize) && CacheMaxSize[imageSize] < fltMaxSize))
                {
                    CacheMaxSize[imageSize] = fltMaxSize;
                }
            }

            var srcImage = new Bitmap(imageSize, imageSize);
            using (var graphics = Graphics.FromImage(srcImage))
            {
                if (backColor.HasValue && backColor.Value != Color.Empty && backColor.Value != Color.Transparent)
                    graphics.Clear(backColor.Value);
                var s = char.ConvertFromUtf32((int)iconText);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SetGDIHigh();
                using Brush brush2 = new SolidBrush(foreColor.Value);
                graphics.DrawString(s, imageFont, brush2, new PointF((imageSize - textSize.Width) / 2.0f + 1, (imageSize - textSize.Height) / 2.0f + 1));
            }

            return srcImage;
        }

        /// <summary>
        /// Gets the size of the icon.
        /// </summary>
        /// <param name="iconText">The icon text.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="font">The font.</param>
        /// <returns>Size.</returns>
        private static Size GetIconSize(FontIcons iconText, Graphics graphics, Font font)
        {
            var text = char.ConvertFromUtf32((int)iconText);
            return graphics.MeasureString(text, font).ToSize();
        }

        /// <summary>
        /// Converts to icon.
        /// </summary>
        /// <param name="srcBitmap">The source bitmap.</param>
        /// <param name="size">The size.</param>
        /// <returns>Icon.</returns>
        /// <exception cref="ArgumentNullException">srcBitmap</exception>
        private static Icon? ToIcon(Image srcBitmap, int size)
        {
            if (srcBitmap == null)
            {
                throw new ArgumentNullException("srcBitmap");
            }

            using var memoryStream = new MemoryStream();
            new Bitmap(srcBitmap, new Size(size, size)).Save(memoryStream, ImageFormat.Png);
            Stream stream = new MemoryStream();
            var binaryWriter = new BinaryWriter(stream);
            if (stream.Length <= 0L)
            {
                return null;
            }

            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((short)1);
            binaryWriter.Write((short)1);
            binaryWriter.Write((byte)size);
            binaryWriter.Write((byte)size);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((byte)0);
            binaryWriter.Write((short)0);
            binaryWriter.Write((short)32);
            binaryWriter.Write((int)memoryStream.Length);
            binaryWriter.Write(22);
            binaryWriter.Write(memoryStream.ToArray());
            binaryWriter.Flush();
            binaryWriter.Seek(0, SeekOrigin.Begin);
            var icon = new Icon(stream);
            stream.Dispose();

            return icon;
        }

    }
}
