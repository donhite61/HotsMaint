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
        private BindingSource bs;
        private ITable locTable;

        public FormLocationsGrid(ITable _table)
        {
            locTable = _table;
            var dataSet = locTable.DataSet;
            bs = new BindingSource()
            {
                DataSource = dataSet,
                DataMember = dataSet.Tables[0].TableName,
                Filter = "Inactive = 0"
            };
            InitializeComponent();
        }

        private void FormLocationsGrid_Load(object sender, EventArgs e)
        {
            dgv1.DataSource = bs;
            SetDataGridviewProperties();
            dgv1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(DgvCell_DoubleClick);
            chkbx_Inactive.CheckedChanged += new EventHandler(Chkbx_Inactive_CheckedChanged);
        }

        private void SetDataGridviewProperties()
        {
            dgv1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv1.AllowUserToDeleteRows = false;
            chkbx_Inactive.Checked = false;
            dgv1.Columns[0].Visible = false;
            dgv1.Columns[4].Visible = false;
            dgv1.Columns[7].Visible = false;
            dgv1.Columns[8].Visible = false;
            dgv1.Columns[9].Visible = false;
        }

        private void Chkbx_Inactive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbx_Inactive.Checked)
                bs.Filter = null;
            else
                bs.Filter = "Inactive = 0";
        }

        private void DgvCell_DoubleClick(object sender, EventArgs e)
        {
            

            Form formEdit = new FormEdit(locTable, bs);
            formEdit.Show();
        }
    }
}
