using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUY_SHOP
{
    public partial class ManageProduct : Form
    {
        private int productID;
        private string authorityLevel;
        
        public ManageProduct(string authorityLevel, int productID)
        {
            this.authorityLevel = authorityLevel;
           this. productID =productID;
            productID = 0;

            InitializeComponent();
        }
        private void LoadCategoryCombox()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string query = "SELECT CategoryID, CategoryName FROM Category";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                cbCategory.DataSource = dataTable;
                cbCategory.DisplayMember = "CategoryName";
                cbCategory.ValueMember = "CategoryID";

            }
        }
        private bool ValidateData(String productCode, string productName, String productPrice, String productQuanity)
        {
            double temp;
            int temp2;
            if (String.IsNullOrEmpty(productName)) { return false; }
            if (String.IsNullOrEmpty(productPrice)) { return false; }
            if (!Double.TryParse(productPrice, out temp)) { return false; }
            if (String.IsNullOrEmpty(productQuanity)) { return false; }
            return int.TryParse(productQuanity, out temp2);
        }
        private void UploadFile(String filter, String path)
        {
            /// hinh anh 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Title = "Select a file to upload ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { string soureFilePath = openFileDialog.FileName;
                string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
                string targetFilePath = Path.Combine(targetDirectory, Path.GetFileName(soureFilePath));
                try
                {
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    File.Copy(soureFilePath, targetFilePath, overwrite: true);
                    txtProductImg.Text = targetFilePath;
                    MessageBox.Show("File upLoaded successfully !", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("error uploading file:" + ex.Message, "error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                
            }

        }
        private void LoadProductData()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null) 
            {
                connection.Open();
                string querry = "SELECT * FROM Product";
                SqlDataAdapter adapter = new SqlDataAdapter(querry, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dtgProduct.DataSource = dataTable;
                connection.Close();

            }

        }
        private void ClearData()
        {
            FlushProductId();
            txtProductCode.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductImg.Text = string.Empty;
            txtProductPrice.Text = string.Empty;
            txtProductQuantity.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }
        private void ChangeButtonStatus(bool buttonStatus)
        {
            
        }
        private void FlushProductId()
        {
            this.productID = 0;
            ChangeButtonStatus(false);
        }
        private void AddProduct()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string productCode = txtProductCode.Text;
                string productName = txtProductName.Text;
                string productImg = txtProductImg.Text;
                string price = txtProductPrice.Text;
                string quantity = txtProductQuantity.Text;
                int  categoryId = Convert.ToInt32(cbCategory.SelectedValue);
                if (ValidateData(productCode, productName, price, quantity))
                {
                    string sql = "INSERT INTO Product VALUES (" +
                        "@productCode, @productName, @productPrice, @productQuantity, @productImg, @categoryId)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("productCode", productCode);
                    command.Parameters.AddWithValue("productName", productName);
                    command.Parameters.AddWithValue("productPrice", Convert.ToDouble(price));
                    command.Parameters.AddWithValue("productQuantity", Convert.ToInt32(quantity));
                    command.Parameters.AddWithValue("productImg", productImg);
                    command.Parameters.AddWithValue("categoryId", categoryId);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show(
                            "Successfully add new product",
                            "information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearData();
                        LoadProductData();
                    }
                    else
                    {
                        MessageBox.Show(
                            "cannot add the product ",
                            "error ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                connection.Close();
            }
        }
        private void UpdataProduct()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string productCode = txtProductCode.Text;
                string productName = txtProductName.Text;
                string productImg = txtProductImg.Text;
                string price = txtProductPrice.Text;
                string quantity = txtProductQuantity.Text;
                int categoryId = Convert.ToInt32(cbCategory.SelectedValue);

                if (ValidateData(productCode, productName, price, quantity))
                {
                    string sql = "UPDATE Product SET ProductCode = @productCode, " +
                         "ProductName = @productName, " +
                         "Price = @productPrice, " +
                         "InventoryQuantity = @productQuantity, " +
                         "ProductImage = @productImg, " +
                         "CategoryId = @categoryId " +
                         "WHERE ProductID = @productId";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("productCode", productCode);
                    command.Parameters.AddWithValue("productName", productName);
                    command.Parameters.AddWithValue("productPrice", Convert.ToDouble(price));
                    command.Parameters.AddWithValue("productQuantity", Convert.ToInt32(quantity));
                    command.Parameters.AddWithValue("productImg", productImg);
                    command.Parameters.AddWithValue("categoryId", categoryId);
                    command.Parameters.AddWithValue("productId", this.productID);
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show(
                            "Successfully update new product",
                            "information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearData();
                        LoadProductData();
                    }
                    else
                    {
                        MessageBox.Show(
                            "cannot updtae the product ",
                            "error ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                connection.Close();
            }
        }
        private void DeleteProduct()
        {
            DialogResult dialogResult = MessageBox.Show("do you want to delete the product ",
                "waring ",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (!IsProductInOrder(this.productID))
                {
                    SqlConnection connection = DatabaseConnection.GetConnection();
                    if (connection != null)
                    {
                        connection.Open();
                        string sql = "DELETE Product WHERE ProductID = @productId";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("productId", this.productID);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show(
                            "Successfully delete product",
                            "information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            ClearData();
                            LoadProductData();


                        }
                        else
                        {
                            MessageBox.Show(
                            "cannot delelete product",
                            "error ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);


                        }
                        connection.Close() ;

                    }
                }
                else
                {
                    MessageBox.Show(
                            " product is in another  order  cannot delete ",
                            "error ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                   
                }
            }
        }
        private bool IsProductInOrder(int productID)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = " SELECT COUNT(*) FROM OderDetail WHERE ProductID = @productId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("productId", productID);
                int result = (int)command.ExecuteScalar();
                connection.Close();
                return result > 0;
            }
            return false;
        }
        private void SearchProduct(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                LoadProductData();

            }
            else
            {
                SqlConnection connection = DatabaseConnection.GetConnection();
                if (connection != null)
                {
                    connection.Open();
                    string sql = "SELECT p.ProductID, p.ProductCode, p.ProductName, p.Price, " +
                                "p.InventoryQuantity, p.ProductImage, c.CategoryName " +
                                    "FROM Product p " +
                                   "INNER JOIN Category c ON p.CategoryID = c.CategoryID " +
                                    "WHERE p.ProductCode LIKE @search " +
                                     "OR p.ProductName LIKE @search " +
                                     "OR c.CategoryName LIKE @search";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    dtgProduct.DataSource = data;
                    connection.Close();
                }
            }

            }
        
            
    


        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void ManageProduct_Load(object sender, EventArgs e)
        {
            LoadProductData();
            LoadCategoryCombox();
            ChangeButtonStatus(false);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           UpdataProduct();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WarehouseManagerForm warehouseMangerForm = new WarehouseManagerForm(this.authorityLevel, this.productID);
            this.Hide();
            warehouseMangerForm.Show();
           
        }


        //sai phần này
        private void dtgProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dtgProduct.CurrentCell.RowIndex;
            if (index != -1 && !dtgProduct.Rows[index].IsNewRow)
            {
                productID = Convert.ToInt32(dtgProduct.Rows[index].Cells[0].Value);
                txtProductCode.Text = dtgProduct.Rows[index].Cells[1].Value.ToString();
                txtProductName.Text = dtgProduct.Rows[index].Cells[2].Value.ToString();
                txtProductPrice.Text = dtgProduct.Rows[index].Cells[3].Value.ToString();
                txtProductQuantity.Text = dtgProduct.Rows[index].Cells[4].Value.ToString();
                txtProductImg.Text = dtgProduct.Rows[index].Cells[5].Value.ToString();
                string categoryName = dtgProduct.Rows[index].Cells[6].Value.ToString();
                for (int i = 0; i < cbCategory.Items.Count; i++)
                {
                    if (cbCategory.SelectedText == categoryName)
                    {
                        cbCategory.SelectedIndex = i;
                        break;
                    }
                }

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            SearchProduct(search);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            
        }
    }
    }

