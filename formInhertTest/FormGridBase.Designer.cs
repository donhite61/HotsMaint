namespace FormInhertTest
{
    partial class FormGridBase
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
            this.Chkbx_ShowInactive = new System.Windows.Forms.CheckBox();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.Btn_Update = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Chkbx_ShowInactive
            // 
            this.Chkbx_ShowInactive.AutoSize = true;
            this.Chkbx_ShowInactive.Location = new System.Drawing.Point(243, 17);
            this.Chkbx_ShowInactive.Name = "Chkbx_ShowInactive";
            this.Chkbx_ShowInactive.Size = new System.Drawing.Size(94, 17);
            this.Chkbx_ShowInactive.TabIndex = 0;
            this.Chkbx_ShowInactive.Text = "Show Inactive";
            this.Chkbx_ShowInactive.UseVisualStyleBackColor = true;
            // 
            // DGV1
            // 
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV1.Location = new System.Drawing.Point(0, 0);
            this.DGV1.Name = "DGV1";
            this.DGV1.Size = new System.Drawing.Size(904, 449);
            this.DGV1.TabIndex = 1;
            // 
            // Btn_Update
            // 
            this.Btn_Update.Location = new System.Drawing.Point(3, 15);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new System.Drawing.Size(98, 24);
            this.Btn_Update.TabIndex = 2;
            this.Btn_Update.Text = "Update";
            this.Btn_Update.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Last Update";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Btn_Update);
            this.splitContainer1.Panel1.Controls.Add(this.Chkbx_ShowInactive);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DGV1);
            this.splitContainer1.Size = new System.Drawing.Size(904, 503);
            this.splitContainer1.TabIndex = 4;
            // 
            // FormGridBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 503);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "FormGridBase";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Button Btn_Update;
        protected System.Windows.Forms.DataGridView DGV1;
        protected System.Windows.Forms.CheckBox Chkbx_ShowInactive;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

