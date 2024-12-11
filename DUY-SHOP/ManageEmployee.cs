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
    public partial class ManageEmployee : Form
    {
        int employeeID;
        public string employeePosition;

        public ManageEmployee(string employeePosition, int employeeId )
        {
            this.employeeID = 0;
            this.employeeID = employeeId;
            this.employeePosition = employeePosition;
            InitializeComponent();
        }
        private bool ValidateData(string employeeCode, 
                                  string employeeName, 
                                  string employeePosition, 
                                  string authorityLevel, 
                                  string username, 
                                  string password)
        {
            bool isVaild = true;
            if (employeeCode == null || employeeCode == string.Empty)
            {
                MessageBox.Show(
                    "employee code cannot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtEmployeeCode.Focus();
                isVaild = false;


            }
            else if (employeeName == null || employeeName == string.Empty)
            {
                MessageBox.Show(
                    "employee name cannot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtEmployeeName.Focus();
                isVaild = false;
            }
            else if (employeePosition == null || employeePosition == string.Empty)
            {
                MessageBox.Show(
                    "employee position canot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                txtEmployeePosition.Focus();
                isVaild = false;

            }
            else if (authorityLevel == null || authorityLevel == string.Empty)
            {
                MessageBox.Show(
                    "employee code  canot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                cbAuthorityLevel.Focus();
                isVaild = false;

            }
            else if (username == null || username == string.Empty)
            {
                MessageBox.Show(
                    "username canot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                txtUsername.Focus();
                isVaild = false;

            }
            else if (password == null || password == string.Empty)
            {
                MessageBox.Show(
                    "password   position canot be blank ",
                    "error ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                txtPassword.Focus();
                isVaild = false;

            }
            return isVaild;
        }
        private void ChangeButtonStatus(bool buttonStatus)
        {
            

        }
        private void FlushEmployeeId()
        {
            this.employeeID = 0;
            ChangeButtonStatus(true);
        }

        private void LoadEmployeeData()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "SELECT * FROM Employee";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dtgEmployee.DataSource = table;
                connection.Close();
            }

        }
        private void ClearData()
        {
            FlushEmployeeId();
            txtEmployeeCode.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtEmployeePosition.Text = string.Empty;
            cbAuthorityLevel.SelectedIndex = 0;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtEmployeeCode.Focus();
        }
        public void InitializeComboBox()
        {
            cbAuthorityLevel.Items.Add("admin");
            cbAuthorityLevel.Items.Add("warehouse");
            cbAuthorityLevel.Items.Add("sale");
            cbAuthorityLevel.SelectedIndex = 0;
        }
        private bool CheckUserExistence(int employeeID)
        {
            bool isExist = false;
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string checkCustomerQuery = "SELECT * FROM Employee WHERE EmployeeID = @employeeID ";
                SqlCommand command = new SqlCommand(checkCustomerQuery, connection);
                command.Parameters.AddWithValue("employeeID",employeeID);
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
                connection.Close();
            }
            return isExist;
        }
        private void AddUser
           (
            string employeeCode,
            string employeeName,
            string employeePosition,
            string authorityLevel,
            string username,
            string password)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "INSERT INTO  Employee VALUES (@employeeCode, " + "@employeeName, @Position , " + "@AuthorityLevel,@username,@password,1  )";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("employeeCode", employeeCode);
                command.Parameters.AddWithValue("employeeName", employeeName);
                command.Parameters.AddWithValue("position", employeePosition);
                command.Parameters.AddWithValue("authorityLevel", authorityLevel);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show(
                        "successfully add new usser ",
                        "information ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    ClearData();
                    LoadEmployeeData();
                }
                else
                {
                    MessageBox.Show(
                        "canot add usser  ",
                        "information ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                connection.Close();
            }
        }
        private void UpdateUser(int employeeID,
                                string employeeCode,
                                string employeeName,
                                string employeePosition,
                                string authoritylevel,
                                string username,
                                string password)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "UPDATE Employee SET EmployeeCode = @employeeCode, " +
        "EmployeeName = @employeeName, " +
        "Position = @employeePosition, " +
        "AuthorityLevel = @authorityLevel, " + // Fixed here
        "Username = @username, " +
        "Password = @password " +
        "WHERE EmployeeID = @employeeID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("employeeCode", employeeCode);
                command.Parameters.AddWithValue("employeeName", employeeName);
                command.Parameters.AddWithValue("employeePosition", employeePosition);
                command.Parameters.AddWithValue("authorityLevel", authoritylevel);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("employeeId", employeeID);
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {

                    MessageBox.Show(
                          "succesfully updata usser    ",
                          "information ",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information
                          );
                    ClearData();
                    LoadEmployeeData();

                }
                else
                {
                    MessageBox.Show(
                          "canot updata usser  ",
                          "information ",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error
                          );
                    connection.Close();

                }
            }
        }
        private void DeleteUser(int employeeID)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = " DELETE Employee WHERE EmployeeID = @employeeId ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("employeeID", employeeID);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {

                    MessageBox.Show(
                          "successfully delete user   ",
                          "information ",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information
                          );
                    ClearData();
                    LoadEmployeeData();
                }
                else
                {

                    MessageBox.Show(
                          "canot delete usser  ",
                          "error  ",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error
                          );
                    connection.Close();
                }
            }
        }
        private void SearchUser(string search)
        {
            if (string.IsNullOrEmpty(search))
            { LoadEmployeeData(); }
            else
            {
                SqlConnection connection = DatabaseConnection.GetConnection();

                if (connection != null)
                {
                    connection.Open();
                    string query = "SELECT * FROM Employee WHERE EmployeeCode LIKE @search " +
                                   "OR  EmployeeName LIKE  @search " +
                                   "OR  Position LIKE  @search " +
                                   "OR  AuthorityLevel LIKE  @search " +
                                   "OR  Username LIKE  @search " +
                                   "OR  Password LIKE  @search ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dtgEmployee.DataSource = table;
                    connection.Close();
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string employeeCode = txtEmployeeCode.Text;
            string employeeName = txtEmployeeName.Text;
            string employeePosition = txtEmployeePosition.Text;
            string authorityLevel = cbAuthorityLevel.SelectedIndex.ToString();
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isVaild = ValidateData(employeeCode,
                employeeName, employeePosition, authorityLevel, username, password);
            if (isVaild)
            {
                AddUser(employeeCode, employeeName, employeePosition, authorityLevel, username, password);
            }

        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            ChangeButtonStatus(false);
            InitializeComboBox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string employeeCode = txtEmployeeCode.Text;
            string employeeName = txtEmployeeName.Text;
            string employeePosition = txtEmployeePosition.Text;
            string authorityLevel = cbAuthorityLevel.SelectedIndex.ToString();
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isVaild = ValidateData(employeeCode,
                                        employeeName,
                                        employeePosition,
                                        authorityLevel,
                                        username,
                                        password);
            if
                (isVaild)
            {
                UpdateUser(employeeID, employeeCode, employeeName, employeePosition, authorityLevel, username, password);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("do you want to delete this usser  ", " warning ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteUser(employeeID);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (employeePosition)
            {
                case "admin":
                    {
                        AdminForm adminForm = new AdminForm(employeePosition, employeeID);
                        this.Hide();
                        adminForm.Show();
                        break;
                    }
                case "warehouse manger ":
                    {
                        WarehouseManagerForm warehouseMangerForm = new WarehouseManagerForm(employeePosition, employeeID);
                        this.Hide();
                        warehouseMangerForm.Show();
                        break;

                    }
                case "sale":
                    {
                        SaleForm saleform = new SaleForm(employeePosition, employeeID);
                        this.Hide();
                        saleform.Show();
                        break;

                    }
                default:
                    {
                        break;

                    }
            }
        }

        private void dtgEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgEmployee.CurrentCell.RowIndex;
            if (index != -1)
            {
                employeeID = Convert.ToInt32(dtgEmployee.Rows[index].Cells[0].Value);
                txtEmployeeCode.Text = dtgEmployee.Rows[index].Cells[1].Value.ToString();
                txtEmployeeName.Text = dtgEmployee.Rows[index].Cells[2].Value.ToString();
                txtEmployeePosition.Text = dtgEmployee.Rows[index].Cells[3].Value.ToString();
                string authorityLevel = dtgEmployee.Rows[index].Cells[4].Value.ToString();
                if (authorityLevel == "admin")
                {
                    cbAuthorityLevel.SelectedIndex = 0;
                }
                else if (authorityLevel == "warehouse")
                {
                    cbAuthorityLevel.SelectedIndex = 1;
                }
                else if (authorityLevel == "sale")
                {
                    cbAuthorityLevel.SelectedIndex = 2;
                }
                txtUsername.Text = dtgEmployee.Rows[index].Cells[5].Value.ToString();
                txtPassword.Text = dtgEmployee.Rows[index].Cells[6].Value.ToString();
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            SearchUser(search);
        }
    }
}
