namespace Caty.Tools.UxForm.Controls
{
    partial class UxWaveProcess
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxWave1 = new Caty.Tools.UxForm.Controls.UxWave();
            this.SuspendLayout();
            // 
            // ucWave1
            // 
            this.uxWave1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxWave1.Location = new System.Drawing.Point(0, 140);
            this.uxWave1.Name = "ucWave1";
            this.uxWave1.Size = new System.Drawing.Size(150, 10);
            this.uxWave1.TabIndex = 0;
            this.uxWave1.Text = "ucWave1";
            this.uxWave1.WaveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.uxWave1.WaveHeight = 15;
            this.uxWave1.WaveSleep = 100;
            this.uxWave1.WaveWidth = 100;
            // 
            // UCProcessWave
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.Controls.Add(this.uxWave1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Name = "UCProcessWave";
            this.Size = new System.Drawing.Size(150, 150);
            this.ResumeLayout(false);
        }

        #endregion

        private UxWave uxWave1;
    }
}
