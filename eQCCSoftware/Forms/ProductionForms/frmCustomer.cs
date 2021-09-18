using System;
using System.Windows.Forms;
using eQCCSoftware.Modules;
using System.Data.SqlClient;
using System.Data;

namespace eQCCSoftware.Forms.ProductionForms
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
            fillgrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void fillgrid()
        {
            try
            {
                DataGridView1.Columns.Clear();
                modDeclaration.objconn.Open();
                string strsql = "SELECT c.*, COUNT(*) OVER () FROM tblCustomer c WHERE c.isActive = 1 ORDER BY c.company asc";
                modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                modDeclaration.objda = new SqlDataAdapter(modDeclaration.objcmd);
                modDeclaration.dt = new DataTable();

                modDeclaration.objda.Fill(modDeclaration.dt);
                DataGridView1.DataSource = modDeclaration.dt;

                int rowCount = modDeclaration.dt.Rows.Count;

                if (rowCount > 0)
                {
                    string counter = modDeclaration.dt.Rows[0].ItemArray[7].ToString();
                    if (counter != "")
                        lblCount.Text = "[ " + counter + " ]" + " records found";
                }
                else
                    lblCount.Text = "[ 0 ] records found";

                {
                    var withBlock = DataGridView1;
                    DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    DataGridView1.RowHeadersVisible = false;

                    withBlock.Columns[0].HeaderText = "User ID";
                    withBlock.Columns[0].Visible = false;

                    withBlock.Columns[1].HeaderText = "COMPANY";
                    withBlock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    withBlock.Columns[2].HeaderText = "ADDRESS";
                    withBlock.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    withBlock.Columns[3].HeaderText = "CONTACT PERSON";
                    withBlock.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    withBlock.Columns[4].HeaderText = "CONTACT NUMBER";
                    withBlock.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    withBlock.Columns[5].HeaderText = "EMAIL ADDRESS";
                    withBlock.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    withBlock.Columns[6].Visible = false; // This is the count of record
                    withBlock.Columns[7].Visible = false; // This is active/inactive status
                }

                modDeclaration.objda.Dispose();
                modDeclaration.objcmd.Dispose();
                modDeclaration.objconn.Close();
            }
            catch (Exception ex)
            {
                modDeclaration.objconn.Close();
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCustomerDetails fcd = new frmCustomerDetails();
            fcd.btnSave.Visible = true;
            fcd.btnCancel.Visible = true;
            fcd.btnUpdate.Visible = false;
            fcd.btnDelete.Visible = false;
            fcd.ShowDialog();
        }

        
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i;
                i = DataGridView1.CurrentRow.Index;

                frmCustomerDetails frmCustomerDetails = new frmCustomerDetails();

                {
                    var withBlock = DataGridView1;
                    frmCustomerDetails._custId =(string)(withBlock[0, i].Value.ToString());
                    frmCustomerDetails.txtCompany.Text =(string)(withBlock[1, i].Value);
                    frmCustomerDetails.txtAddress.Text = (string)(withBlock[2, i].Value);
                    frmCustomerDetails.txtContactPerson.Text = (string)(withBlock[3, i].Value);
                    frmCustomerDetails.txtContactNo.Text = (string)(withBlock[4, i].Value);
                    frmCustomerDetails.txtEmail.Text = (string)(withBlock[5, i].Value);
                    frmCustomerDetails.btnSave.Visible = false;
                    frmCustomerDetails.btnUpdate.Visible = true;
                    frmCustomerDetails.btnDelete.Visible = true;
                    frmCustomerDetails.btnCancel.Visible = false;
                }

                frmCustomerDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i;
                i = DataGridView1.CurrentRow.Index;

                frmCustomerDetails frmCustomerDetails = new frmCustomerDetails();

                {
                    var withBlock = DataGridView1;
                    frmCustomerDetails._custId = (string)(withBlock[0, i].Value.ToString());
                    frmCustomerDetails.txtCompany.Text = (string)(withBlock[1, i].Value);
                    frmCustomerDetails.txtAddress.Text = (string)(withBlock[2, i].Value);
                    frmCustomerDetails.txtContactPerson.Text = (string)(withBlock[3, i].Value);
                    frmCustomerDetails.txtContactNo.Text = (string)(withBlock[4, i].Value);
                    frmCustomerDetails.txtEmail.Text = (string)(withBlock[5, i].Value);
                    frmCustomerDetails.btnSave.Visible = false;
                    frmCustomerDetails.btnUpdate.Visible = true;
                    frmCustomerDetails.btnDelete.Visible = true;
                    frmCustomerDetails.btnCancel.Visible = false;
                }

                frmCustomerDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
