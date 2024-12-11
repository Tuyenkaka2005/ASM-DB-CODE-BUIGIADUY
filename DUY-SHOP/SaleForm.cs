using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUY_SHOP
{
    public partial class SaleForm : Form
    {
        private int employeeID;
        private string authorityLevel;
        public SaleForm(string authorityLevel, int employeeID)
        {
            InitializeComponent();
            this.authorityLevel = authorityLevel;
            this.employeeID = employeeID;
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnManageCustomer_Click(object sender, EventArgs e)
        {
            ManageCustomer manageCustomer = new ManageCustomer(authorityLevel, employeeID);   
            this.Hide();
            manageCustomer.Show();
        }

        private void btnManageOrder_Click(object sender, EventArgs e)
        {
          OrderHistory orderHistory = new OrderHistory (authorityLevel, employeeID);
            orderHistory.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
