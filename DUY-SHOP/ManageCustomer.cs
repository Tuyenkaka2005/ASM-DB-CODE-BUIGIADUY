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
    public partial class ManageCustomer : Form
    {
        private int customerID;
        private int employeeID;
        private string authorityLevel;
        public ManageCustomer(string authorityLevel, int employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
            this.authorityLevel = authorityLevel;
        }
        private void ChangeButtonStatus(bool buttonStatus)
        {
            btnAdd.Enabled = buttonStatus;
            btnUpdate.Enabled = buttonStatus;
            btnClear.Enabled = buttonStatus;
            btnDelete.Enabled = buttonStatus;
        }
        private bool ValidateData(string customerCode, string customerName, string customerAddress, string phonenumber)
        {
            bool isVaild = true;
            if (customerCode == null || customerCode == string.Empty)
            {
                MessageBox.Show(
                    "customer code cannot be blank ",
                    "warning  ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                isVaild = false;
                txtCustomerCode.Focus();

            }
            else if (customerName == null || customerName == string.Empty)
            {
                MessageBox.Show(
                  "customer name  cannot be blank ",
                  "warning  ",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                isVaild = false;
                txtCustomerName.Focus();
            }
            else if (customerAddress == null || customerAddress == string.Empty)
            {
                MessageBox.Show(
                  "customer adddress   cannot be blank ",
                  "warning  ",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                isVaild = false;
                txtCustomerAddress.Focus();

            }
            else if (phonenumber == null || phonenumber == string.Empty)
            {

                MessageBox.Show(
                  "phonenumber  cannot be blank ",
                  "warning  ",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                isVaild = false;
                txtPhonenumber.Focus();
            }
            return isVaild;
        }
        private void FlushCustomerID()
        {
            this.customerID = 0;
            ChangeButtonStatus(false);

        }
        private void LoadCustomerData()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT  * FROM Customer ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dtgCustomer.DataSource = table;
                connection.Close();

            }
        }
        private bool CheckUserExistence(int customerID)
        {
            bool isExist = false;
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string checkCustomerQuery = "SELECT * FROM Customer WHERE CustomerID =@customerID";
                SqlCommand command = new SqlCommand(checkCustomerQuery, connection);
                command.Parameters.AddWithValue("customerID", customerID);
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
                connection.Close();

            }
            return isExist;
        }
        private void AddCustomer(int customerID, string customerCode, string customerName, string customerAddress, string phoneNumber)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "INSERT INTO Customer (CustomerCode,  CustomerName, Phonenumber, Address)" +
                               "VALUES (@customerCode, @customerName, @phoneNumber, @customerAddress)";
                SqlCommand command = new SqlCommand(@query, connection);
                command.Parameters.AddWithValue("customerCode", customerCode);
                command.Parameters.AddWithValue("customerName", customerName);
                command.Parameters.AddWithValue("phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("customerAddress", customerAddress);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show(
                        "suceesfully add new customer ",
                        "success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                       "an error occur while ading  customer ",
                       "error ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);

                }
                connection.Close();
                ClearData();
                LoadCustomerData();
            }
        }
        private void UpdataCustomer(int customerID, string customerCode, string customerName, string customerAddress, string phoneNumber)

        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "UPDATE Customer SET " +
               "CustomerCode = @customerCode, " +
               "CustomerName = @customerName, " +
               "Address = @customerAddress, " +
               "phonenumber = @phoneNumber " +  // Bỏ dấu phẩy cuối
               "WHERE CustomerID = @customerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("customerCode", customerCode);
                command.Parameters.AddWithValue("customerName", customerName);
                command.Parameters.AddWithValue("customerAddress", customerAddress);
                command.Parameters.AddWithValue("phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("customerID", customerID);
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show(
                       "successully update customer ",
                       "succesfully ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                       "an error occur while updaing  customer ",
                       "error ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
                connection.Close();
                ClearData();
                LoadCustomerData();



            }
        }
        private void DeleteCustomer(int customerID)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)

            {
                connection.Open();
                string deleteOrderDetailQuery = "DELECT FROM OrderDetail WHERE  OrderDetailID IN " +
                       "(SELECT  OrderID FROM Orders WHERE CustomerID = @customerID)";
                SqlCommand command = new SqlCommand(deleteOrderDetailQuery, connection);
                command.Parameters.AddWithValue("customerID", customerID);
                command.ExecuteNonQuery();
                string deleteOrderQuery = "DELETE FROM Order WHERE CustomerID = @customerID";
                command = new SqlCommand(deleteOrderQuery, connection);
                command.Parameters.AddWithValue("customerID", customerID);
                command.ExecuteNonQuery();
                string deleteCustomerQuery = "DELETE FROM Customer WHERE  CustomerID = @customerID ";
                command = new SqlCommand(deleteCustomerQuery, connection);
                command.Parameters.AddWithValue("customerID", customerID);
                int deleteCustomerResult = command.ExecuteNonQuery();
                if (deleteCustomerResult > 0)
                {
                    MessageBox.Show(
                       "successully update customer ",
                       "succesfully ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                       "an error occur wwhile deleting   customer ",
                       "error  ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
                connection.Close();
                ClearData();
                LoadCustomerData();

            }
        }
        private void SearchCustomer(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                LoadCustomerData();

            }
            else
            {
                SqlConnection connection = DatabaseConnection.GetConnection();
                if (connection != null)
                {
                    connection.Open();
                    string query = "SELECT * FROM Customer  WHERE CustomerCode LIKE @search OR CustomerName  LIKE @search  OR  Phonenumber LIKE @search OR Address LIKE @search ";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dtgCustomer.DataSource = table;
                    connection.Close();
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerCode = txtCustomerCode.Text;
            string customerName = txtCustomerName.Text;
            string customerAddress = txtCustomerAddress.Text;
            string phoneNumber = txtPhonenumber.Text;
            bool isValid = ValidateData(customerCode, customerName, customerAddress, phoneNumber);
            if (isValid)
            {
                AddCustomer(customerID, customerCode, customerName, customerAddress, phoneNumber);
            }
        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (customerID > 0)
            {
                bool isUserExit = CheckUserExistence(customerID);
                if (isUserExit)
                {
                    string customerCode = txtCustomerCode.Text;
                    string customerName = txtCustomerName.Text;
                    string customerAddress = txtCustomerAddress.Text;
                    string phoneNumber = txtPhonenumber.Text;

                    bool isValid = ValidateData(customerCode, customerName, customerAddress, phoneNumber);
                    if (isValid)
                    {
                        UpdataCustomer(customerID, customerCode, customerName, customerAddress, phoneNumber);


                    }
                }
                else
                {
                    MessageBox.Show(
                       "no customer found  ",
                       "error  ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (customerID > 0)
            {
                DialogResult result = MessageBox.Show(
                       "do you want to delete this customer wwith  all  related data?  ",
                       "warning  ",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Question);
            }
            if (DialogResult == DialogResult.OK)
            {
                bool isUserExist = CheckUserExistence(customerID);
                if (isUserExist)
                {
                    DeleteCustomer(customerID);
                }
                else
                {
                    MessageBox.Show(
                       " no customer found  ",
                       "error  ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
            }
        }
        private void ClearData()
        {
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPhonenumber.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;

            txtSearch.Text = string.Empty;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (authorityLevel)
            {
                case "admin":
                    {
                        AdminForm adminform = new AdminForm(this.authorityLevel, this.employeeID);
                        this.Hide();
                        adminform.Show();
                        break;

                    }
                case "warehouse":
                    {
                        WarehouseManagerForm warehouseManagerForm = new WarehouseManagerForm(this.authorityLevel, this.employeeID);
                        this.Hide();
                        warehouseManagerForm.Show();
                        break;
                    }
                case "sale":
                    {
                        SaleForm saleForm = new SaleForm(this.authorityLevel, this.employeeID);
                        this.Hide();
                        saleForm.Show();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void dtgCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgCustomer.CurrentCell.RowIndex;
            if (index > -1)
            {
                customerID = (int)dtgCustomer.Rows[index].Cells[0].Value;
                txtCustomerCode.Text = dtgCustomer.Rows[index].Cells[1].Value.ToString();
                txtCustomerName.Text = dtgCustomer.Rows[index].Cells[2].Value.ToString();
                txtPhonenumber.Text = dtgCustomer.Rows[index].Cells[3].Value.ToString();
                txtCustomerAddress.Text = dtgCustomer.Rows[index].Cells[4].Value.ToString();
                ChangeButtonStatus(true);
            }
        }

        private void dtgCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgCustomer.CurrentCell.RowIndex;
            if (index > -1)
            {
                customerID = (int)dtgCustomer.Rows[index].Cells[0].Value;
                string customerName = dtgCustomer.Rows[index].Cells[2].Value.ToString();
                SaleForm sale = new SaleForm(authorityLevel, employeeID);
                sale.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            SearchCustomer(search);
        }
    }
}
