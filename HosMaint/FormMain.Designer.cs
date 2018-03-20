namespace HotsMaint
{
    partial class FormMain
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
            this.but_SetUpServer = new System.Windows.Forms.Button();
            this.btn_Vendors = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Locations
            // 
            this.btn_Locations.Location = new System.Drawing.Point(34, 35);
            this.btn_Locations.Name = "btn_Locations";
            this.btn_Locations.Size = new System.Drawing.Size(110, 29);
            this.btn_Locations.TabIndex = 0;
            this.btn_Locations.Text = "Locations";
            this.btn_Locations.UseVisualStyleBackColor = true;
            // 
            // but_SetUpServer
            // 
            this.but_SetUpServer.Location = new System.Drawing.Point(194, 35);
            this.but_SetUpServer.Name = "but_SetUpServer";
            this.but_SetUpServer.Size = new System.Drawing.Size(104, 28);
            this.but_SetUpServer.TabIndex = 1;
            this.but_SetUpServer.Text = "Create Locations";
            this.but_SetUpServer.UseVisualStyleBackColor = true;
            this.but_SetUpServer.Click += new System.EventHandler(this.Btn_Create_Loc_Table_Click);
            // 
            // btn_Vendors
            // 
            this.btn_Vendors.Location = new System.Drawing.Point(34, 70);
            this.btn_Vendors.Name = "btn_Vendors";
            this.btn_Vendors.Size = new System.Drawing.Size(110, 29);
            this.btn_Vendors.TabIndex = 0;
            this.btn_Vendors.Text = "Vendors";
            this.btn_Vendors.UseVisualStyleBackColor = true;
            this.btn_Vendors.Click += new System.EventHandler(this.btn_Vendors_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create Vendors";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Btn_Create_Vend_Table_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.but_SetUpServer);
            this.Controls.Add(this.btn_Vendors);
            this.Controls.Add(this.btn_Locations);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Locations;
        private System.Windows.Forms.Button but_SetUpServer;
        private System.Windows.Forms.Button btn_Vendors;
        private System.Windows.Forms.Button button1;
    }
}

