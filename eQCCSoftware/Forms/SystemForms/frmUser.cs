using System;
using System.Windows.Forms;
using GGLPCPMS.Forms.SystemForms;

namespace eQCCSoftware.Forms.SystemForms
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            frmUserDetails frmUD = new frmUserDetails();
            frmUD.btnSave.Visible = true;
            frmUD.btnUpdate.Visible = false;
            frmUD.btnDelete.Visible = false;
            frmUD.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
