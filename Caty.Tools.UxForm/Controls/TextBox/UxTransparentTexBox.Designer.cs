using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.TextBox
{
    partial class UxTransparentTexBox
    {

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        [Category("Appearance"),
         Description("The alpha value used to blend the control's background. Valid values are 0 through 255."),
         Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BackAlpha
        {
            get { return myBackAlpha; }
            set
            {
                int v = value;
                if (v > 255)
                {
                    v = 255;
                }
                myBackAlpha = v;
                myUpToDate = false;
                Invalidate();
            }
        }
    }
}
