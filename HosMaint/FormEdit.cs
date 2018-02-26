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
    public partial class FormEdit : Form
    {
        DataRow row;
        Locations loc;
        public FormEdit(DataRow _row, Locations _loc)
        {
            row = _row;
            loc = _loc;
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            if (row.ItemArray[0].ToString() != "")
                SetUpControls();

            this.Closing += new CancelEventHandler(formEditClose);
            //btn_Delete.Click += new EventHandler(btn_Delete_Click);
            //btn_Save.Click += new EventHandler(btn_Save_Click);
            //btn_Cancel.Click += new EventHandler(btn_Cancel_Click);
        }


        private void formEditClose(object sender, CancelEventArgs e)
        {
            //bs.EndEdit();
            //var tblMod = ((DataTable)bs.DataSource).GetChanges(DataRowState.Modified);
            //if (tblMod != null)
            //{
            //    var DialogResult = MessageBox.Show("You have unsaved changes, Do you want to save them?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            //    if (DialogResult == DialogResult.No)
            //        btn_Cancel_Click(sender, new EventArgs());

            //    else if (DialogResult == DialogResult.Yes)
            //        btn_Save_Click(sender, new EventArgs());

            //    else
            //        e.Cancel = true;
            //}
        }

        private void SetUpControls()
        {
            lbl_Id.Text = row.ItemArray[0].ToString();
            txtbx_Code.Text = row.ItemArray[1].ToString();
            txtbx_Name.Text = row.ItemArray[2].ToString();
            txtbx_Add1.Text = row.ItemArray[3].ToString();
            txtbx_Add2.Text = row.ItemArray[4].ToString();
            txtbx_City.Text = row.ItemArray[5].ToString();
            txtbx_State.Text = row.ItemArray[6].ToString();
            txtbx_Zip.Text = row.ItemArray[7].ToString();
            txtbx_Phone.Text = row.ItemArray[8].ToString();
            txtbx_Email.Text = row.ItemArray[9].ToString();
            chkbx_Inactive.Checked = Convert.ToBoolean(row.ItemArray[10]);
        }

        //private void btn_Save_Click(object sender, EventArgs e)
        //{
        //    bs.EndEdit();
        //    var tblAdd = ((DataTable)bs.DataSource).GetChanges(DataRowState.Added);
        //    if(tblAdd != null)
        //    {
        //        var sqlId = loc.InsertRecord(tblAdd.Rows[0]);
        //        var tbl = ((DataTable)bs.DataSource);
        //        var row = (DataRow)bs.Current;
        //        ((DataTable)bs.DataSource).AcceptChanges();
        //    }
        //    var tblMod = ((DataTable)bs.DataSource).GetChanges(DataRowState.Modified);
        //    if (lbl_Id.Text != "Id")
        //    {
        //        if (loc.UpdateRecord(tblMod.Rows[0]))
        //            ((DataTable)bs.DataSource).AcceptChanges();
        //        else
        //            ((DataTable)bs.DataSource).RejectChanges();
        //    }
        //    else
        //    {
        //        var sqlId = loc.InsertRecord(tblMod.Rows[0]);
        //        ((DataTable)bs.DataSource).AcceptChanges();
        //    }
        //}

        //private void btn_Delete_Click(object sender, EventArgs e)
        //{
        //    var id = Convert.ToUInt32(lbl_Id.Text);
        //    if (loc.DeleteRecord(id))
        //    {
        //        bs.RemoveCurrent();
        //        bs.EndEdit();
        //        ((DataTable)bs.DataSource).AcceptChanges();
        //        this.Close();
        //    }
        //    else
        //        ((DataTable)bs.DataSource).RejectChanges();
        //}

        //private void btn_Cancel_Click(object sender, EventArgs e)
        //{
        //    bs.EndEdit();
        //    ((DataTable)bs.DataSource).RejectChanges();
        //}
    }
}
