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
    public partial class OrderHistory : Form

    {
       public  int employeeID;
       public string authorityLevel;
        public OrderHistory(string authorityLevel, int employeeID)
        {
            this.authorityLevel = authorityLevel;
            this.employeeID = employeeID;
            InitializeComponent();
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            RedirectPage();
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {
            LoadOrderHistory();
        }
        private void LoadOrderHistory()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT o.SaleID, o.SaleDate, " +
                               "e.EmployeeName, " +
                               "c.CustomerName " +
                               "FROM Sale o " +
                               "INNER JOIN Employee e ON o.EmployeeID  =  e.EmployeeID " +
                               "INNER JOIN Customer c ON o.CustomerID = c.CustomerID ";
                              
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("employeeID", employeeID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dtgOrderHistory.DataSource = dataTable;


            }
        }
        private void RedirectPage()
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

        private void btnDowload_Click(object sender, EventArgs e)
        {
            LoadOrderHistory();
        }

        private void dtgOrderHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn vào dòng hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy dòng hiện tại (dòng được click)
                DataGridViewRow selectedRow = dtgOrderHistory.Rows[e.RowIndex];

                // Lấy dữ liệu từ các cột tương ứng
                string saleDate = selectedRow.Cells["SaleDate"].Value?.ToString(); // Thay "SaleDate" bằng tên cột trong DataGridView
                string employeeName = selectedRow.Cells["EmployeeName"].Value?.ToString();
                string customerName = selectedRow.Cells["CustomerName"].Value?.ToString();

                // Hiển thị thông tin lên MessageBox (hoặc xử lý theo yêu cầu)
                MessageBox.Show($"Sale Date: {saleDate}\nEmployee Name: {employeeName}\nCustomer Name: {customerName}",
                                "Order Details",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            }

        private void dtgOrderHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
