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
        public DataRow row;
        Locations loc;
        BindingSource bs;

        public FormEdit(BindingSource _bs, Locations _loc)
        {
            bs = _bs;
            if (bs.Current is DataRowView drv)
                row = drv.Row as DataRow;

            loc = _loc;
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            if(row.RowState == DataRowState.Unchanged)
                SetUpControls();
            else
               // chkbx_Inactive.Checked = false;

            this.Closing += new CancelEventHandler(FormEditClose);
            //btn_Delete.Click += new EventHandler(btn_Delete_Click);
            btn_Save.Click += new EventHandler(btn_Save_Click);
            btn_Cancel.Click += new EventHandler(btn_Cancel_Click);
        }


        private void FormEditClose(object sender, CancelEventArgs e)
        {
            //ReadFieldsToRow();
            //var tbl = row.Table;
            //var tblMod = tbl.GetChanges(DataRowState.Modified);
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
            lbl_Id.DataBindings.Add(new Binding("Text", bs, "Id", true));
            txtbx_Code.DataBindings.Add(new Binding("Text", bs, "Code", true));
            txtbx_Name.DataBindings.Add(new Binding("Text", bs, "Name", true));
            txtbx_Add1.DataBindings.Add(new Binding("Text", bs, "Address", true));
            txtbx_Add2.DataBindings.Add(new Binding("Text", bs, "Address2", true));
            txtbx_City.DataBindings.Add(new Binding("Text", bs, "City", true));
            txtbx_State.DataBindings.Add(new Binding("Text", bs, "State", true));
            txtbx_Zip.DataBindings.Add(new Binding("Text", bs, "Zip", true));
            txtbx_Phone.DataBindings.Add(new Binding("Text", bs, "Phone", true));
            txtbx_Email.DataBindings.Add(new Binding("Text", bs, "Email", true));
            chkbx_Inactive.DataBindings.Add(new Binding("Checked", bs, "Inactive", true));


            //lbl_Id.Text = row.ItemArray[0].ToString();
            //txtbx_Code.Text = row.ItemArray[1].ToString();
            //txtbx_Name.Text = row.ItemArray[2].ToString();
            //txtbx_Add1.Text = row.ItemArray[3].ToString();
            //txtbx_Add2.Text = row.ItemArray[4].ToString();
            //txtbx_City.Text = row.ItemArray[5].ToString();
            //txtbx_State.Text = row.ItemArray[6].ToString();
            //txtbx_Zip.Text = row.ItemArray[7].ToString();
            //txtbx_Phone.Text = row.ItemArray[8].ToString();
            //txtbx_Email.Text = row.ItemArray[9].ToString();
            //chkbx_Inactive.Checked = Convert.ToBoolean(row.ItemArray[10]);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            if (row.RowState == DataRowState.Unchanged)
            {
                this.Close();
            }

            if (row.RowState == DataRowState.Added)
            {
                var tbl = ((DataTable)bs.DataSource);
                var tblAdd = ((DataTable)bs.DataSource).GetChanges(DataRowState.Added);
                row.BeginEdit();
                //row[0] = loc.InsertRecord(row);
                row.EndEdit();
                tbl.AcceptChanges();
            }

            if (row.RowState == DataRowState.Modified)
            {
              
                var tbl = ((DataTable)bs.DataSource);
                var tblMod = ((DataTable)bs.DataSource).GetChanges(DataRowState.Modified);
                var rowMod = tblMod.Rows[0];
                //loc.UpdateRecord(row);
                tbl.AcceptChanges();
            }
            // ReadFieldsToRow();
            //int Id;
            //if (int.TryParse(lbl_Id.Text, out Id))
            //{
            //    loc.UpdateRecord(row);
            //}
            //else
            //{

            //}
            this.Close();
        }

        private void ReadFieldsToRow()
        {
            row.BeginEdit();
            row[1] = txtbx_Code.Text;
            row[2] = txtbx_Name.Text;
            row[3] = txtbx_Add1.Text;
            row[4] = txtbx_Add2.Text;
            row[5] = txtbx_City.Text;
            row[6] = txtbx_State.Text;
            row[7] = txtbx_Zip.Text;
            row[8] = txtbx_Phone.Text;
            row[9] = txtbx_Email.Text;
            row[10] = chkbx_Inactive.Checked;
            row.EndEdit();
        }

        //    var tblAdd = ((DataTable)bs.DataSource).GetChanges(DataRowState.Added);
        //    if (tblAdd != null)
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            row.Table.RejectChanges();
        }
    }
}
