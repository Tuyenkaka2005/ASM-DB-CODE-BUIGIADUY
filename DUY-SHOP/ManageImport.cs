using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DUY_SHOP
{
    public partial class ManageImport : Form

    {
        string authorityLevel;
        int employeeID;
        string connectionString = "Data Source=LAPTOP-LO8CF4TD;Initial Catalog=DUYSHOP2;Integrated Security=True";
        SqlConnection connection;

        public ManageImport()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            dgvImport.CellClick += dgvImport_CellClick;
        }

        private void ManageImport_Load(object sender, EventArgs e)
        {
            LoadImportData();
        }
        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

        }
        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        private void LoadImportData()
        {
            string query = "SELECT * FROM Import";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvImport.DataSource = dt;
        }
        private void LoadImportDetailData(int importID)
        {
            try
            {
                OpenConnection();

                string query = "SELECT * FROM ImportDetail WHERE ImportID = @ImportID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImportID", importID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvImportDetail.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho ImportID: " + importID, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvImportDetail.DataSource = null; // Xóa dữ liệu cũ nếu không có kết quả
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ImportDetail: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void dgvImport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu hàng được chọn là hợp lệ
            {
                int importID = Convert.ToInt32(dgvImport.Rows[e.RowIndex].Cells["ImportID"].Value);
                LoadImportDetailData(importID);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnback_Click(object sender, EventArgs e)
        {

            AdminForm adminform = new AdminForm(this.authorityLevel, this.employeeID);
            this.Hide();
            adminform.Show();
        }
    }
    }

