using eQCCSoftware.Modules;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GGLPCPMS.Forms.SystemForms
{
    public partial class frmUserDetails : Form
    {
        public frmUserDetails()
        {
            InitializeComponent();
            fillrolecombo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear()
        {
            modProcedures.ClearTextBoxes(this.Controls);
            txtUsername.Focus();
        }

        public void fillrolecombo()
        {
            try
            {
                modDeclaration.objconn.Open();
                string strsql = "select distinct r.rolename,r.roleid from tblRoles r where r.isActive = 1 order by r.rolename asc";
                modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                modDeclaration.objdr = modDeclaration.objcmd.ExecuteReader();
                cmbRole.Items.Clear();
                while (modDeclaration.objdr.Read())
                {
                    cmbRole.Items.Add(modDeclaration.objdr["rolename"]);
                }
                modDeclaration.objdr.Close();
                modDeclaration.objcmd.Dispose();
                modDeclaration.objconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!... Unble to fill rolename");
                modDeclaration.objconn.Close();
            }
        }

        private void txtpassword2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtpassword2.Text)
                {
                    MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = txtpassword2; 
                    return;
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpassword2.Text != "") 
                {
                    if (txtPassword.Text != txtpassword2.Text)
                    {
                        MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ActiveControl = txtPassword;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
