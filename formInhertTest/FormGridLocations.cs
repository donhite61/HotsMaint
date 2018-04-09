using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormInhertTest
{
    public partial class FormGridLocations : FormGridBase
    {
        public FormGridLocations(Model _mod) : base(_mod)
        {
            InitializeComponent();
        }

        private void FormGridLocations_Load(object sender, EventArgs e)
        {
            SetColumns();
            Size = new Size(SetWindowWidth(DGV1), 400);
        }

        private void SetColumns()
        {
            DGV1.Columns[0].Visible = false;
            DGV1.Columns[4].Visible = false;
            DGV1.Columns[7].Visible = false;
            DGV1.Columns[8].Visible = false;
            DGV1.Columns[9].Visible = false;
            DGV1.Columns[11].Visible = false;
        }

        protected override void DGV1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new FormEditLocations(mod).Show();
        }
    }
}
