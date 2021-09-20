using System;
using System.Linq;
using System.Windows.Forms;
using eQCCSoftware.Modules;
using System.Data;
using System.Data.SqlClient;

namespace eQCCSoftware.Forms.ProductionForms
{
    public partial class frmCustomerDetails : Form
    {

        public string _custId;
        public frmCustomerDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clear()
        {
            modProcedures.ClearTextBoxes(this.Controls);
            txtCompany.Focus();
        }

        public void requiredField(String str) 
        {
            MessageBox.Show("Warning : " + str +" is required", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text == "") { this.ActiveControl = txtCompany; requiredField(txtCompany.Tag.ToString()); return; }
                if (txtAddress.Text == "") { this.ActiveControl = txtAddress; requiredField(txtAddress.Tag.ToString()); return; }
                if (txtContactPerson.Text == "") { this.ActiveControl = txtContactPerson; requiredField(txtContactPerson.Tag.ToString()); return; }
                if (txtContactNo.Text == "") { this.ActiveControl = txtContactNo; requiredField(txtContactNo.Tag.ToString()); return; }
                if (txtEmail.Text == "") { this.ActiveControl = txtEmail; requiredField(txtEmail.Tag.ToString()); return; }

                DialogResult dialogResult = MessageBox.Show("Save this record", "GGLPC Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    modDeclaration.objconn.Open();
                    string strsql = "insert into tblcustomer (company,address,contactperson,contactno,email,isactive)" +
                            "values (@val1, @val2,@val3,@val4,@val5,@val6)";
                    modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    modDeclaration.objcmd.Parameters.AddWithValue("val1", txtCompany.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val2", txtAddress.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val3", txtContactPerson.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val4", txtContactNo.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val5", txtEmail.Text);
                    modDeclaration.objcmd.Parameters.AddWithValue("val6", 1);
                    modDeclaration.objcmd.ExecuteNonQuery();
                    modDeclaration.objcmd.Dispose();
                    MessageBox.Show("Record has been successfully saved!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Cancelled by user!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                modDeclaration.objconn.Close();
                clear();
            }           
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text == "") { this.ActiveControl = txtCompany; requiredField(txtCompany.Tag.ToString()); return; }
                if (txtAddress.Text == "") { this.ActiveControl = txtAddress; requiredField(txtAddress.Tag.ToString()); return; }
                if (txtContactPerson.Text == "") { this.ActiveControl = txtContactPerson; requiredField(txtContactPerson.Tag.ToString()); return; }
                if (txtContactNo.Text == "") { this.ActiveControl = txtContactNo; requiredField(txtContactNo.Tag.ToString()); return; }
                if (txtEmail.Text == "") { this.ActiveControl = txtEmail; requiredField(txtEmail.Tag.ToString()); return; }

                DialogResult dialogResult = MessageBox.Show("Update this record", "GGLPC Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    modDeclaration.objconn.Open();
                    string strsql = "update tblcustomer set company=@val1,address=@val2,contactperson=@val3,contactno=@val4,email=@val5 where custId=@val0";
                    modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    modDeclaration.objcmd.Parameters.AddWithValue("val1", txtCompany.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val2", txtAddress.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val3", txtContactPerson.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val4", txtContactNo.Text.ToUpper());
                    modDeclaration.objcmd.Parameters.AddWithValue("val5", txtEmail.Text);
                    modDeclaration.objcmd.Parameters.AddWithValue("val0", _custId);
                    modDeclaration.objcmd.ExecuteNonQuery();
                    modDeclaration.objcmd.Dispose();
                    MessageBox.Show("Record has been successfully updated!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Cancelled by user!", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                modDeclaration.objconn.Close();
                clear();
            }
            catch (Exception ex)
            {
                throw ex;
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
                    string strsql = "update tblcustomer set isactive = 0 where custId=@val0";
                    modDeclaration.objcmd = new SqlCommand(strsql, modDeclaration.objconn);
                    modDeclaration.objcmd.Parameters.AddWithValue("val0", _custId);
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
