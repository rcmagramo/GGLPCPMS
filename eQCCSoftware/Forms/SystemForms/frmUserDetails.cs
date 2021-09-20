using eQCCSoftware.Modules;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GGLPCPMS.Forms.SystemForms
{
    public partial class frmUserDetails : Form
    {
        public string _userId;
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

        private void fillrolecombo()
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

        private void requiredField(String str)
        {
            MessageBox.Show("Warning : " + str + " is required", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "") { this.ActiveControl = txtUsername; requiredField(txtUsername.Tag.ToString()); return; }
                if (txtPassword.Text == "") { this.ActiveControl = txtPassword; requiredField(txtPassword.Tag.ToString()); return; }
                if (txtpassword2.Text == "") { this.ActiveControl = txtpassword2; requiredField(txtpassword2.Tag.ToString()); return; }

                if (txtPassword.Text != txtpassword2.Text)
                {
                    MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = txtpassword2;
                    return;
                }

                if (txtPassword.Text != txtpassword2.Text)
                {
                    MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = txtPassword;
                    return;
                }

                int comboIndex = cmbRole.SelectedIndex + 1;
                if (comboIndex == -1)
                {
                    MessageBox.Show("Please choose a role", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRole.Focus();
                    return;
                }
                else
                {
                    modDeclaration.objconn.Open();
                    string strsql = "insert into tblUsers (username, userpassword,userroleid,isactive)" +
                         " values (@val1,@val2,@val3,1)";
                    SqlCommand objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    objcmd.Parameters.AddWithValue("@val1", txtUsername.Text);
                    objcmd.Parameters.AddWithValue("@val2", Crypto.Encrypt(txtPassword.Text, Crypto.key));
                    objcmd.Parameters.AddWithValue("@val3", comboIndex);
                    objcmd.ExecuteNonQuery();
                    objcmd.Dispose();
                    modDeclaration.objconn.Close();
                    MessageBox.Show("User has been successfully saved!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "") { this.ActiveControl = txtUsername; requiredField(txtUsername.Tag.ToString()); return; }
                if (txtPassword.Text == "") { this.ActiveControl = txtPassword; requiredField(txtPassword.Tag.ToString()); return; }
                if (txtpassword2.Text == "") { this.ActiveControl = txtpassword2; requiredField(txtpassword2.Tag.ToString()); return; }

                if (txtPassword.Text != txtpassword2.Text)
                {
                    MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = txtpassword2;
                    return;
                }

                if (txtPassword.Text != txtpassword2.Text)
                {
                    MessageBox.Show("PASSWORD does not match", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ActiveControl = txtPassword;
                    return;
                }

                int comboIndex = cmbRole.SelectedIndex + 1;
                if (comboIndex == -1)
                {
                    MessageBox.Show("Please choose a role", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRole.Focus();
                    return;
                }
                else
                {
                    modDeclaration.objconn.Open();
                    string strsql = "update tblUsers set userpassword=@val1, userroleid=@val2 where userid=@val0";
                    SqlCommand objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    objcmd.Parameters.AddWithValue("@val0", _userId);
                    objcmd.Parameters.AddWithValue("@val1", Crypto.Encrypt(txtPassword.Text, Crypto.key));
                    objcmd.Parameters.AddWithValue("@val2", comboIndex);
                    objcmd.ExecuteNonQuery();
                    objcmd.Dispose();
                    modDeclaration.objconn.Close();
                    MessageBox.Show("Record has been successfully updated!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record", "GGLPC Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    modDeclaration.objconn.Open();
                    string strsql = "update tblUsers set isactive = 0 where userId=@val0";
                    modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    modDeclaration.objcmd.Parameters.AddWithValue("val0", _userId);
                    modDeclaration.objcmd.ExecuteNonQuery();
                    modDeclaration.objcmd.Dispose();
                    MessageBox.Show("Record has been successfully deleted!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Cancelled by user!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                modDeclaration.objconn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
