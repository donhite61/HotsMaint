using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormInhertTest
{
    public partial class FormEditVenProducts : FormInhertTest.FormEditBase
    {
        public FormEditVenProducts(Model _mod) : base(_mod)
        {
            InitializeComponent();
        }

        private void FormEditVenProducts_Load(object sender, EventArgs e)
        {
            SetUpControls();
            originalCode = txtbx_Code.Text;
            dirtyTracker = new DirtyTracker(this);
        }

        private void SetUpControls()
        {
            lbl_Id.DataBindings.Add(new Binding("Text", bs, "Id", false));
            txtbx_Code.DataBindings.Add(new Binding("Text", bs, "Code", false));
            txtbx_Description.DataBindings.Add(new Binding("Text", bs, "Description", false));
            txtbx_Price.DataBindings.Add(new Binding("Text", bs, "Price", false));
            txtbx_Quantity.DataBindings.Add(new Binding("Text", bs, "Quantity", false));
            txtbx_Units.DataBindings.Add(new Binding("Text", bs, "Units", false));
            txtbx_VenCatNum.DataBindings.Add(new Binding("Text", bs, "VendCatNum", false));
            cmbbx_VenId.DataBindings.Add(new Binding("Text", bs, "VenId", false));
            chkbx_Inactive.DataBindings.Add(new Binding("Checked", bs, "Inactive", true));
            lbl_Timestamp.DataBindings.Add(new Binding("Text", bs, "Timestamp", true));

            txtbx_Price.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtbx_Code.Validating += new CancelEventHandler(Txtbx_Code_Validating);
        }
    }
}
