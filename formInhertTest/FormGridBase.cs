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
    public partial class FormGridBase : Form
    {
        protected Model mod;
        protected FormGridBase() {}

        public FormGridBase(Model _mod)
        {
            mod = _mod;
            InitializeComponent();
            CreateStandardEventHandlers();
            DGV1.DataSource = mod.BSource;
            Text = "List of " + mod.BSource.DataMember;
            SetUpGrid();
        }

        private void SetUpGrid()
        {
            DGV1.EditMode = DataGridViewEditMode.EditProgrammatically;
            DGV1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DGV1.AllowUserToDeleteRows = false;
            Chkbx_ShowInactive.Checked = false;
        }

        protected int SetWindowWidth(DataGridView dgv)
        {
            int totWidth = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible)
                    totWidth += col.Width;

            return totWidth + 75;
        }

        private void CreateStandardEventHandlers()
        {
            Chkbx_ShowInactive.CheckedChanged += new EventHandler(Chkbx_ShowInactive_CheckedChanged);
            DGV1.CellDoubleClick += new DataGridViewCellEventHandler(DGV1_CellDoubleClick);
            Btn_Update.Click += new EventHandler(Btn_Update_Click);
        }

        protected void Chkbx_ShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (Chkbx_ShowInactive.Checked)
                mod.BSource.Filter = null;
            else
                mod.BSource.Filter = "Inactive = 0";
        }

        protected virtual void DGV1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            mod.FillTable(mod.Dset.Tables[0]);
        }

    }
}
