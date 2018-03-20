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
        private Model loc;
        private DataRow row;
        private BindingSource bs;
        private string OriginalCode;

        public FormEdit(Model _loc)
        {
            loc = _loc;
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            if (loc.BSource.Current is DataRowView drv)
            {
                row = drv.Row as DataRow;
                bs = loc.BSource;
                
            }

            SetUpControls();
            OriginalCode = txtbx_Code.Text;
            Closing += new CancelEventHandler(FormEditClose);
            btn_Delete.Click += new EventHandler(btn_Delete_Click);
            btn_Save.Click += new EventHandler(Btn_Save_Click);
            btn_Cancel.Click += new EventHandler(btn_Cancel_Click);
            txtbx_Code.Validating += new CancelEventHandler(txtbx_Code_Validating);
        }

        private void SetUpControls()
        {
            lbl_Id.DataBindings.Add(new Binding("Text", bs, "Id", false));
            txtbx_Code.DataBindings.Add(new Binding("Text", bs, "Code", false));
            txtbx_Name.DataBindings.Add(new Binding("Text", bs, "Name", false));
            txtbx_Add1.DataBindings.Add(new Binding("Text", bs, "Address", false));
            txtbx_Add2.DataBindings.Add(new Binding("Text", bs, "Address2", false));
            txtbx_City.DataBindings.Add(new Binding("Text", bs, "City", false));
            txtbx_State.DataBindings.Add(new Binding("Text", bs, "State", false));
            txtbx_Zip.DataBindings.Add(new Binding("Text", bs, "Zip", false));
            txtbx_Phone.DataBindings.Add(new Binding("Text", bs, "Phone", false));
            txtbx_Email.DataBindings.Add(new Binding("Text", bs, "Email", false));
            chkbx_Inactive.DataBindings.Add(new Binding("Checked", bs, "Inactive", true));
            lbl_Timestamp.DataBindings.Add(new Binding("Text", bs, "Timestamp", true));

            if (row.RowState == DataRowState.Detached)// new record
                btn_Delete.Enabled = false;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            loc.CurRecId = Convert.ToUInt32(row.ItemArray[0]);
            if (loc.DeleteRecord(loc))
            {
                bs.RemoveCurrent();
                ((DataSet)bs.DataSource).AcceptChanges();
            }
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
            if (row.RowState == DataRowState.Detached)// new record
            {
                row[10] = chkbx_Inactive.Checked;
                row[0] = loc.InsertRecord(row);
                bs.EndEdit();
            }
            bs.EndEdit();
            if (row.RowState == DataRowState.Modified)// record changed
                loc.UpdateRecord(row);

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

        private void txtbx_Code_Validating(object sender, CancelEventArgs e)
        {
            var newCode = ((TextBox)sender).Text;
            if (newCode == OriginalCode)
                return;

            if (loc.CodeHasBeenUsed(loc, newCode))
            {
                txtbx_Code.Text = OriginalCode;
                e.Cancel = true;
                MessageBox.Show(newCode + " code has been used already");
            }
        }
    }
}
