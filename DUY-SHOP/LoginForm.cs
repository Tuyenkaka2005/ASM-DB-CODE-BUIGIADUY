using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUY_SHOP
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            InitializeCombox();
        }
        public void InitializeCombox()
        {
            cbRole.Items.Add("admin");
            cbRole.Items.Add("warehouse");
            cbRole.Items.Add("sale");
            cbRole.SelectedIndex = 0;
        }
        private bool ValidataData(string username, string password, string role)
        {
            bool isValid = true;
            if (username == null || username == string.Empty)
            {
                MessageBox.Show(
                    " password can'not be blank ",
                    "warning ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                isValid = false;
                txtPassword.Focus();

            }
            else if (password == null || password == string.Empty)
            {
                MessageBox.Show(" no password selected ", " warning ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                isValid = false;
                txtPassword.Focus();

            }

            else if (role == null || role == string.Empty)
            {
                MessageBox.Show(" no role selected ", " warning ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
                cbRole.Focus();

            }
            return isValid;

        }
        private void RedirectPage(string selectRole, int employeeID, bool isChangerPassword)
        {
            if (isChangerPassword)
            {
                if (selectRole != null)
                {
                    if (selectRole == "admin")
                    {
                        AdminForm AdminForm = new AdminForm(selectRole, employeeID);
                        this.Hide();
                        AdminForm.Show();
                    }
                    else if (selectRole == "warehouse")
                    {
                        WarehouseManagerForm WarehouseMangerForm = new WarehouseManagerForm(selectRole, employeeID);
                        this.Hide();
                        WarehouseMangerForm.Show();

                    }
                    else if (selectRole == "sale")
                    {
                        SaleForm SaleForm = new SaleForm(selectRole, employeeID);
                        this.Hide();
                        SaleForm.Show();

                    }
                }

            }

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = cbRole.SelectedItem.ToString();

            bool isValid = ValidataData(username, password, role);
            if (isValid)
            {
                SqlConnection connection = DatabaseConnection.GetConnection();
                if (connection != null)
                {
                    string query = "SELECT EmployeeID,passwordchanged FROM Employee WHERE Username = @username " + " AND Password = @password AND AuthorityLevel = @role ";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@role ", role);

                    SqlDataReader reader = command.ExecuteReader();
                    int employeeID = 0;
                    bool passwordchanger = false;
                    while (reader.Read())
                    {
                        employeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                        passwordchanger = reader.GetBoolean(reader.GetOrdinal("passwordchanged"));


                    }
                    if (employeeID > 0)
                    {
                        MessageBox.Show(
                            "Login success", " Information ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        RedirectPage(role, employeeID, passwordchanger);

                    }
                    else
                    {
                        MessageBox.Show(
                            "Invalid login credential ",
                            "Error ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);


                    }
                    connection.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private void ClearData()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Focus();
        }
    }
}
