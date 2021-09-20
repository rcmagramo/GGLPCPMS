using System;
using System.Windows.Forms;
using GGLPCPMS.Forms.SystemForms;
using eQCCSoftware.Modules;
using System.Data.SqlClient;
using System.Data;

namespace eQCCSoftware.Forms.SystemForms
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
            fillgrid();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            frmUserDetails frmUD = new frmUserDetails();
            frmUD.txtUsername.Enabled = true;
            frmUD.btnSave.Visible = true;
            frmUD.btnUpdate.Visible = false;
            frmUD.btnDelete.Visible = false;
            frmUD.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void fillgrid()
        {
            try
            {
                dataGridView1.Columns.Clear();
                modDeclaration.objconn.Open();
                SqlCommand objcmd = new SqlCommand("select u.userid, u.username, u.userpassword, r.rolename, COUNT(*) OVER () as RecordCount , r.roleid " +
                            " from tblUsers u inner join tblRoles r on u.userroleid = r.roleid where u.isactive = 1", modDeclaration.objconn);
                SqlDataAdapter objda = new SqlDataAdapter(objcmd);
                DataTable dt = new DataTable();
                objda.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;

                int rowCount = dt.Rows.Count;

                if (rowCount > 0)
                {
                    string counter = dt.Rows[0].ItemArray[4].ToString();
                    if (counter != "")
                        lblCount.Text = "[ " + counter + " ]" + " records found";
                }
                else
                    lblCount.Text = "[ 0 ] records found";

                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;

                dataGridView1.Columns[0].HeaderText = "USER ID";
                
                dataGridView1.Columns[1].HeaderText = "USERNAME";
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
                dataGridView1.Columns[2].HeaderText = "PASSWORD";
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
                dataGridView1.Columns[3].HeaderText = "ROLE";

                objda.Dispose();
                objcmd.Dispose();
                modDeclaration.objconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i;
                i = dataGridView1.CurrentRow.Index;

                frmUserDetails frmUD = new frmUserDetails();

                {
                    var withBlock = dataGridView1;
                    frmUD._userId = (string)(withBlock[0, i].Value.ToString());
                    frmUD.txtUsername.Text = (string)(withBlock[1, i].Value);
                    frmUD.txtPassword.Text = (string)(Crypto.Decrypt(withBlock[2, i].Value.ToString(),Crypto.key));
                    frmUD.txtpassword2.Text = (string)(Crypto.Decrypt(withBlock[2, i].Value.ToString(), Crypto.key));
                    frmUD.cmbRole.SelectedIndex = (int)(withBlock[5, i].Value) -1;
                    frmUD.txtUsername.Enabled = false;
                    frmUD.btnSave.Visible = false;
                    frmUD.btnUpdate.Visible = true;
                    frmUD.btnDelete.Visible = true;
                    frmUD.btnCancel.Visible = false;
                }

                frmUD.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
