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
    public partial class AdminForm : Form
    {
        int employeeID;
        string authorityLevel;

        public AdminForm(string authorityLevel, int employeeID)
        {
            InitializeComponent();
            this.authorityLevel = authorityLevel;
            this.employeeID = employeeID;

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void btnManageEmployee_Click(object sender, EventArgs e)
        {
            ManageEmployee manageEmployee = new ManageEmployee(this.authorityLevel, this.employeeID);
            this.Hide();
            manageEmployee.Show();
        }

        private void btnManageCategory_Click(object sender, EventArgs e)
        {
            ManageCategory manageCategory = new ManageCategory();
            this.Hide();
            manageCategory.Show();
        }

        private void btnManageProduct_Click(object sender, EventArgs e)
        {
            ManageProduct manageProduct = new ManageProduct(this.authorityLevel,this.employeeID);
            this.Hide();
            manageProduct.Show();
        }

        private void btnManageOrder_Click(object sender, EventArgs e)
        {
            OrderHistory orderHistory = new OrderHistory(this.authorityLevel, this.employeeID);
            this.Hide();
            orderHistory.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(); 
            loginForm.Show(); 
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnManageImport_Click(object sender, EventArgs e)
        {
            ManageImport manageImport = new ManageImport();
            manageImport.Show();
            this.Hide();
        }
    }
}
