using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotsMaint
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btn_Locations.Click += new EventHandler(Btn_Locations_Click);
        }

        private void Btn_Locations_Click(object sender, EventArgs e)
        {
            Form venGridForm = new FormLocationsGrid(new LocationsModel(GV.SerLoc));
            venGridForm.Show();
        }


        private void btn_Vendors_Click(object sender, EventArgs e)
        {
            Form locGridForm = new FormLocationsGrid(new VendorsModel(GV.SerLoc));
            locGridForm.Show();
        }

        private void Btn_Create_Loc_Table_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to erase the locations table?", "Delete all locations", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                LocationsModel.DeleteAndCreateLocationsTableOnServer(GV.SerLoc, 100000001);
        }

        private void Btn_Create_Vend_Table_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to erase the vendors table?", "Delete all vendors", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                VendorsModel.DeleteAndCreateVendorsTableOnServer(GV.SerLoc, 100000001);
        }
    }
}
