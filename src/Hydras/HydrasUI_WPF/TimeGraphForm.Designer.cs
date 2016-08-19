namespace HydrasUI_WPF
{
    partial class TimeGraphForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.multiDimensionDynamicChartControl1 = new HydrasUI_WPF.AssistantUserControl.MultiDimensionDynamicChartControl();
            this.SuspendLayout();
            // 
            // multiDimensionDynamicChartControl1
            // 
            this.multiDimensionDynamicChartControl1.Location = new System.Drawing.Point(2, 15);
            this.multiDimensionDynamicChartControl1.Name = "multiDimensionDynamicChartControl1";
            this.multiDimensionDynamicChartControl1.Size = new System.Drawing.Size(654, 323);
            this.multiDimensionDynamicChartControl1.TabIndex = 0;
            // 
            // TimeGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 350);
            this.Controls.Add(this.multiDimensionDynamicChartControl1);
            this.Name = "TimeGraphForm";
            this.Text = "TimeGraphForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeGraphForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private AssistantUserControl.MultiDimensionDynamicChartControl multiDimensionDynamicChartControl1;

    }
}