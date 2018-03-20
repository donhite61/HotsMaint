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
        Server selServer;
        UInt32 startNum;
        public FormMain()
        {
            InitializeComponent();
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] s = { "Local", "Web" };
            this.CmbBox_Server.DataSource = s;

            btn_Locations.Click += new EventHandler(Btn_Locations_Click);
        }

        private void Btn_Locations_Click(object sender, EventArgs e)
        {
            ReadServer();
            var date1 = DateTime.Now;
            Form venGridForm = new FormLocationsGrid(new LocationsModel(selServer));
            venGridForm.Show();
            DateTime date2 = DateTime.Now;
            MessageBox.Show((date1 - date2).ToString());
        }

        private void btn_Vendors_Click(object sender, EventArgs e)
        {
            ReadServer();
            var date1 = DateTime.Now;
            Form locGridForm = new FormLocationsGrid(new VendorsModel(selServer));
            locGridForm.Show();
            DateTime date2 = DateTime.Now;
            MessageBox.Show((date1 - date2).ToString());

        }

        private void Btn_Create_Loc_Table_Click(object sender, EventArgs e)
        {
            ReadServer();
            startNum = Convert.ToUInt32(txtBox_storeNum.Text) + 100000000;
            var result = MessageBox.Show("Are you sure you want to erase the locations table?", "Delete all locations", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                LocationsModel.DeleteAndCreateLocationsTableOnServer(selServer, startNum);
        }

        private void Btn_Create_Vend_Table_Click(object sender, EventArgs e)
        {
            ReadServer();
            startNum = Convert.ToUInt32(txtBox_storeNum.Text) + 100000000;
            var result = MessageBox.Show("Are you sure you want to erase the vendors table?", "Delete all vendors", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                VendorsModel.DeleteAndCreateVendorsTableOnServer(selServer, startNum);
        }

        private void ReadServer()
        {
            string selectedValue = CmbBox_Server.SelectedValue.ToString();
            if (selectedValue == "Local")
                selServer = new ServerLocal();
            else if (selectedValue == "Web")
                selServer = new ServerWeb();
        }

    }
}
