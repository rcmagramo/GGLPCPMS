using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace eQCCSoftware.Modules
{
    public class modProcedures
    {

        public static bool Connection()
        {
            try
            {
                modDeclaration.objconn.Open();
                return true;
            }
            catch (Exception ex)
            {
                modDeclaration.objconn.Close();
                return false;
            }
        }

        public static bool is_Empty(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        MessageBox.Show("Warning: Required missing field", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.BackColor = ProfessionalColors.ButtonPressedBorder;
                        con.Focus();
                        return true;
                    }
                }
            }
            return false;
        }

        public void ProcessTextBoxes(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                TextBox textBox = child as TextBox;
                if (textBox != null) 
                    ProcessTextBoxes(textBox); 
                else
                    ProcessTextBoxes(parent); //recursive
                MessageBox.Show("Warning: Required missing field", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                parent.Focus();
            } 
        }

        public static void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }
    }
}






