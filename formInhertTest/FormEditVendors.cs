using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormInhertTest
{
    public partial class FormEditVendors : FormInhertTest.FormEditBase
    {
        public FormEditVendors(Model _mod) : base(_mod)
        {
            InitializeComponent();
        }

        private void FormEditVendors_Load(object sender, EventArgs e)
        {
            SetUpControls();
            originalCode = txtbx_Code.Text;
            dirtyTracker = new DirtyTracker(this);
        }

        private void SetUpControls()
        {
            if (row.RowState != DataRowState.Detached)// new record
                row.ItemArray[10] = false;

            chkbx_Inactive.Checked = false;
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
            chkbx_Inactive.DataBindings.Add(new Binding("CheckState", bs, "Inactive", true));
            lbl_Timestamp.DataBindings.Add(new Binding("Text", bs, "Timestamp", true));

            txtbx_Phone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtbx_Code.Validating += new CancelEventHandler(Txtbx_Code_Validating);
            btn_VenProducts.Click += new EventHandler(Btn_VenProducts_Click);
        }

        private void Btn_VenProducts_Click(object sender, EventArgs e)
        {
            var childMod = new VendProdModel(mod.Serv);
            var form = new FormGridSubBase(childMod, mod);
            form.Show();
        }
    }
}
