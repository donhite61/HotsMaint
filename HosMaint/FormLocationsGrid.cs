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
    public partial class FormLocationsGrid : Form
    {
        BindingSource bs = new BindingSource();
        Locations loc;
        public FormLocationsGrid()
        {
            InitializeComponent();
        }

        private void FormLocationsGrid_Load(object sender, EventArgs e)
        {
            loc = new Locations();
            bs.DataSource = loc.tbl;
            bs.Filter = "Inactive = 0";

            dgv1.DataSource = bs;

            dgv1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dgvCell_DoubleClick);
            chkbx_Inactive.CheckedChanged += new EventHandler(chkbx_Inactive_CheckedChanged);
            chkbx_Inactive.Checked = false;
            dgv1.Columns[0].Visible = false;
            dgv1.Columns[4].Visible = false;
            dgv1.Columns[9].Visible = false;
        }

        private void chkbx_Inactive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbx_Inactive.Checked)
                bs.Filter = null;
            else
                bs.Filter = "Inactive = 0";
        }

        private void dgvCell_DoubleClick(object sender, EventArgs e)
        {
            if (bs.Current is DataRowView drv)
            {
                var row = drv.Row as DataRow;
                Form formEdit = new FormEdit(row, loc);
                formEdit.Show();
            }
        }
    }
}
