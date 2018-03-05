namespace HotsMaint
{
    partial class FormLocationsGrid
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.chkbx_Inactive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv1.Location = new System.Drawing.Point(0, 51);
            this.dgv1.Name = "dgv1";
            this.dgv1.Size = new System.Drawing.Size(690, 284);
            this.dgv1.TabIndex = 0;
            // 
            // chkbx_Inactive
            // 
            this.chkbx_Inactive.AutoSize = true;
            this.chkbx_Inactive.Location = new System.Drawing.Point(586, 27);
            this.chkbx_Inactive.Name = "chkbx_Inactive";
            this.chkbx_Inactive.Size = new System.Drawing.Size(94, 17);
            this.chkbx_Inactive.TabIndex = 1;
            this.chkbx_Inactive.Text = "Show Inactive";
            this.chkbx_Inactive.UseVisualStyleBackColor = true;
            // 
            // FormLocationsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 335);
            this.Controls.Add(this.chkbx_Inactive);
            this.Controls.Add(this.dgv1);
            this.Name = "FormLocationsGrid";
            this.Text = "Locations";
            this.Load += new System.EventHandler(this.FormLocationsGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.CheckBox chkbx_Inactive;
    }
}