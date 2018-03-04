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
        BindingSource bs;
        DataRow row;

        public FormEdit(BindingSource _bs)
        {
            bs = _bs;
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            
            if (bs.Current is DataRowView drv)
                row = drv.Row as DataRow;

            SetUpControls();
            this.Closing += new CancelEventHandler(FormEditClose);
            btn_Delete.Click += new EventHandler(btn_Delete_Click);
            btn_Save.Click += new EventHandler(Btn_Save_Click);
            btn_Cancel.Click += new EventHandler(btn_Cancel_Click);
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
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            Locations.DeleteRecord(Convert.ToUInt32(row.ItemArray[0]));
            bs.RemoveCurrent();
            ((DataSet)bs.DataSource).AcceptChanges();
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            row.Table.RejectChanges();
            Close();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (row.RowState == DataRowState.Detached)
            {
                //row[1] = txtbx_Code.Text;
                //row[2] = txtbx_Name.Text;
                //row[3] = txtbx_Add1.Text;
                //row[4] = txtbx_Add2.Text;
                //row[5] = txtbx_City.Text;
                //row[6] = txtbx_State.Text;
                //row[7] = txtbx_Zip.Text;
                //row[8] = txtbx_Phone.Text;
                //row[9] = txtbx_Email.Text;
                row[10] = chkbx_Inactive.Checked;
                row[0] = Locations.InsertRecord(row);
                bs.EndEdit();
            }
            bs.EndEdit();
            if (row.RowState == DataRowState.Modified)
                Locations.UpdateRecord(row);

            ((DataSet)bs.DataSource).AcceptChanges();

            Close();
        }

        private void FormEditClose(object sender, CancelEventArgs e)
        {
            bs.EndEdit();
            if (row.RowState == DataRowState.Modified)
            {
                var DialogResult = MessageBox.Show("You have unsaved changes, Do you want to save them?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (DialogResult == DialogResult.No)
                    btn_Cancel_Click(sender, new EventArgs());

                else if (DialogResult == DialogResult.Yes)
                    Btn_Save_Click(sender, new EventArgs());
                else
                    e.Cancel = true;
            }
        }

    }
}
