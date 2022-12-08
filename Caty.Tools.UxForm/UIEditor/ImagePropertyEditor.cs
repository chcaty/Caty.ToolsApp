using System.ComponentModel;
using System.Drawing.Design;

namespace Caty.Tools.UxForm.UIEditor
{
    public class ImagePropertyEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型
            return UITypeEditorEditStyle.Modal;
        }

        public override object? EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据
            var frm = new FrmSelectImage();
            if (value is null or Image)
            {
                if (value != null)
                    frm.SelectImage = (Image)value;
                return frm.ShowDialog() == DialogResult.OK ? frm.SelectImage : value;
            }
            else
            {
                throw new Exception("这不是一个FontIcons类型的属性");
            }
        }
    }
}
