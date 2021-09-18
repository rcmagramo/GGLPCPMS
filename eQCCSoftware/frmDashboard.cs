using System;
using System.Windows.Forms;
using eQCCSoftware.Forms.ProductionForms;
using eQCCSoftware.Modules;

namespace eQCCSoftware
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            LoadThis();
        }

        private void LoadThis()
        {
            try
            {
                if (modProcedures.Connection() == true)
                {
                    modDeclaration.objconn.Close();
                    hideSubmenu();
                   // MessageBox.Show("AYOS", "GGLPC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Unable to connect to Server", "QCC Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hideSubmenu()
        {
            panelSystemsSubMenu.Visible = false;
            panelProductionSubMenu.Visible = false;
            panelToolsSubmenu.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private Form currentForm = null;

        private void openChildForm(Form childForm)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            currentForm = childForm;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.TopLevel = false;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            showSubmenu(panelProductionSubMenu);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            showSubmenu(panelToolsSubmenu);
        }

        private void btnSystems_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSystemsSubMenu);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            openChildForm(new frmCustomer());
            hideSubmenu();
        }
    }
}
