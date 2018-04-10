using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormInhertTest
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            string[] s = { "Local", "Web" };
            this.CmbBox_Server.DataSource = s;

            btn_Locations.Click += new EventHandler(Btn_Locations_Click);
            Btn_Vendors.Click += new EventHandler(Btn_Vendors_Click);
            Btn_DestroyTables.Click += new EventHandler(Btn_DestroyTables_Click);
        }

        private void Btn_Locations_Click(object sender, EventArgs e)
        {
            var serv =  ReadServer();
            var mod = new LocationsModel(serv);
            var form = new FormGridLocations(mod);
            form.Show();

        }

        private void Btn_Vendors_Click(object sender, EventArgs e)
        {
            var serv = ReadServer();
            var mod = new VendorsModel(serv);
            var form = new FormGridVendors(mod);
            form.Show();
        }

        private void Btn_VenProducts_Click(object sender, EventArgs e)
        {
            //var serv = ReadServer();
            //var mod = new VendProdModel(serv);
            //var form = new FormGridSubBase(mod);
            //form.Show();
        }

        private Server ReadServer()
        {
            Server serv;
            string selectedValue = CmbBox_Server.SelectedValue.ToString();
            if (selectedValue == "Local")
                serv = GV.SerLoc;
            else if (selectedValue == "Web")
                serv = GV.SerWeb;
            else
                throw new NotImplementedException();

            return serv;
        }

        private void Btn_DestroyTables_Click(object sender, EventArgs e)
        {
            var serv = ReadServer();
            var startNum = Convert.ToUInt32(TxtBox_storeNum.Text);
            serv.DeleteAndCreateTablesOnServer(startNum);
        }

    }
}


