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
    public partial class FormEditBase : Form
    {
        protected Model mod;
        protected DataRow row;
        protected BindingSource bs;
        protected string originalCode;
        protected DirtyTracker dirtyTracker;

        public FormEditBase() { }
        public FormEditBase(Model _mod)
        {
            mod = _mod;
            InitializeComponent();

            if (mod.BSource.Current is DataRowView drv)
            {
                row = drv.Row as DataRow;
                if (row.RowState != DataRowState.Detached)// new record
                    mod.CurRecId = Convert.ToUInt32(row.ItemArray[0]);

                bs = mod.BSource;
            }

            Text = "Editing " + mod.BSource.DataMember;
            SetStandardEventHandlers();

        }

        protected void SetStandardEventHandlers()
        {
            Closing += new CancelEventHandler(FormEditClose);
            btn_Delete.Click += new EventHandler(Btn_Delete_Click);
            btn_Save.Click += new EventHandler(Btn_Save_Click);
            btn_Cancel.Click += new EventHandler(Btn_Cancel_Click);
        }

        protected void Txtbx_Code_Validating(object sender, CancelEventArgs e)
        {
            var newCode = ((TextBox)sender).Text;
            if (newCode == originalCode)
                return;

            if (mod.CodeHasBeenUsed(mod, newCode))
            {
                ((TextBox)sender).Text = originalCode;
                e.Cancel = true;
                MessageBox.Show(newCode + " code has been used already");
            }
        }

        protected void Btn_Delete_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            if (mod.DeleteRecord(mod))
            {
                bs.RemoveCurrent();
                ((DataSet)bs.DataSource).AcceptChanges();
            }
            dirtyTracker.IsDirty = false;
            Close();
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            bs.EndEdit();
            ((DataSet)bs.DataSource).RejectChanges();
            dirtyTracker.IsDirty = false;
            Close();
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            if (row.RowState == DataRowState.Detached)// new record
            {
                row[0] = mod.InsertRecord(row);
                bs.EndEdit();
            }
            bs.EndEdit();
            if (row.RowState == DataRowState.Modified)// record changed
                mod.UpdateRecord(row);

            ((DataSet)bs.DataSource).AcceptChanges();
            dirtyTracker.IsDirty = false;
            Close();
        }

        protected void FormEditClose(object sender, CancelEventArgs e)
        {
            if(dirtyTracker.IsDirty)
            {
                bs.EndEdit();
                var DialogResult = MessageBox.Show("You have unsaved changes, Do you want to save them?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (DialogResult == DialogResult.No)
                    Btn_Cancel_Click(sender, new EventArgs());

                else if (DialogResult == DialogResult.Yes)
                    Btn_Save_Click(sender, new EventArgs());

                else
                    e.Cancel = true;
            }
        }
    }

    public class DirtyTracker
    {
        private Form _frmTracked;
        private bool _isDirty = false;

        public DirtyTracker(Form frm)
        {
            _frmTracked = frm;
            AssignHandlersForControlCollection(frm.Controls);
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public void SetAsDirty()
        {
            _isDirty = true;
        }

        public void SetAsClean()
        {
            _isDirty = false;
        }

        private void SimpleDirtyTracker_TextChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void SimpleDirtyTracker_CheckedChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void AssignHandlersForControlCollection(Control.ControlCollection coll)
        {
            foreach (Control c in coll)
            {
                if (c is TextBox)
                    (c as TextBox).TextChanged
                      += new EventHandler(SimpleDirtyTracker_TextChanged);

                if (c is CheckBox)
                    (c as CheckBox).CheckedChanged
                      += new EventHandler(SimpleDirtyTracker_CheckedChanged);

                // ... apply for other desired input types similarly ...

                // recurively apply to inner collections
                if (c.HasChildren)
                    AssignHandlersForControlCollection(c.Controls);
            }
        }
    }
}
