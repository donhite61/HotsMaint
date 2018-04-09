namespace FormInhertTest
{
    partial class FormMenu
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
            this.btn_Locations = new System.Windows.Forms.Button();
            this.Btn_Vendors = new System.Windows.Forms.Button();
            this.CmbBox_Server = new System.Windows.Forms.ComboBox();
            this.TxtBox_storeNum = new System.Windows.Forms.TextBox();
            this.Btn_DestroyTables = new System.Windows.Forms.Button();
            this.Btn_VenProducts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Locations
            // 
            this.btn_Locations.Location = new System.Drawing.Point(32, 23);
            this.btn_Locations.Name = "btn_Locations";
            this.btn_Locations.Size = new System.Drawing.Size(97, 28);
            this.btn_Locations.TabIndex = 0;
            this.btn_Locations.Text = "Locations";
            this.btn_Locations.UseVisualStyleBackColor = true;
            // 
            // Btn_Vendors
            // 
            this.Btn_Vendors.Location = new System.Drawing.Point(32, 57);
            this.Btn_Vendors.Name = "Btn_Vendors";
            this.Btn_Vendors.Size = new System.Drawing.Size(97, 28);
            this.Btn_Vendors.TabIndex = 0;
            this.Btn_Vendors.Text = "Vendors";
            this.Btn_Vendors.UseVisualStyleBackColor = true;
            // 
            // CmbBox_Server
            // 
            this.CmbBox_Server.FormattingEnabled = true;
            this.CmbBox_Server.Location = new System.Drawing.Point(151, 28);
            this.CmbBox_Server.Name = "CmbBox_Server";
            this.CmbBox_Server.Size = new System.Drawing.Size(121, 21);
            this.CmbBox_Server.TabIndex = 1;
            // 
            // TxtBox_storeNum
            // 
            this.TxtBox_storeNum.Location = new System.Drawing.Point(216, 59);
            this.TxtBox_storeNum.Name = "TxtBox_storeNum";
            this.TxtBox_storeNum.Size = new System.Drawing.Size(55, 20);
            this.TxtBox_storeNum.TabIndex = 2;
            // 
            // Btn_DestroyTables
            // 
            this.Btn_DestroyTables.Location = new System.Drawing.Point(168, 94);
            this.Btn_DestroyTables.Name = "Btn_DestroyTables";
            this.Btn_DestroyTables.Size = new System.Drawing.Size(103, 23);
            this.Btn_DestroyTables.TabIndex = 3;
            this.Btn_DestroyTables.Text = "Destroy Tables";
            this.Btn_DestroyTables.UseVisualStyleBackColor = true;
            // 
            // Btn_VenProducts
            // 
            this.Btn_VenProducts.Location = new System.Drawing.Point(32, 91);
            this.Btn_VenProducts.Name = "Btn_VenProducts";
            this.Btn_VenProducts.Size = new System.Drawing.Size(97, 28);
            this.Btn_VenProducts.TabIndex = 0;
            this.Btn_VenProducts.Text = "Vend Products";
            this.Btn_VenProducts.UseVisualStyleBackColor = true;
            this.Btn_VenProducts.Click += new System.EventHandler(this.Btn_VenProducts_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Btn_DestroyTables);
            this.Controls.Add(this.TxtBox_storeNum);
            this.Controls.Add(this.CmbBox_Server);
            this.Controls.Add(this.Btn_VenProducts);
            this.Controls.Add(this.Btn_Vendors);
            this.Controls.Add(this.btn_Locations);
            this.Name = "FormMenu";
            this.Text = "FormMenu";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Locations;
        private System.Windows.Forms.Button Btn_Vendors;
        private System.Windows.Forms.ComboBox CmbBox_Server;
        private System.Windows.Forms.TextBox TxtBox_storeNum;
        private System.Windows.Forms.Button Btn_DestroyTables;
        private System.Windows.Forms.Button Btn_VenProducts;
    }
}