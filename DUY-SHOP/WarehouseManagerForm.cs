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
    public partial class WarehouseManagerForm : Form
    {
        
        string authorityLevel;
        int employeeID;
        public WarehouseManagerForm(string authorityLevel, int employeeID)
        {
            InitializeComponent();
            
            this.authorityLevel = authorityLevel;
            this.employeeID = employeeID;
        }

        private void WarehouseManagerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnManageProduct_Click(object sender, EventArgs e)
        {
            ManageProduct manageProduct = new ManageProduct(this.authorityLevel, this.employeeID);
            this.Hide();
            manageProduct.Show();
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
    }
}
