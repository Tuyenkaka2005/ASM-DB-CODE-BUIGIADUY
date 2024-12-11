using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUY_SHOP
{
    public class DatabaseConnection
    {
        private static string _connectionString = " Data Source=LAPTOP-LO8CF4TD;Initial Catalog=DUYSHOP2;Integrated Security=True";
        // khoi tao bien SQLconnecton
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = null;
            try
            {
                // lay ket noi toi csdl su dung chuoi ket noi
                connection = new SqlConnection(_connectionString);
            }
            //neu khong lay duoc ket noi hoac co loi say ra thi thong bao cho nguoi dung
            catch (SqlException)
            {
                MessageBox.Show(
                    "Error while connecting to the datadase",
                    "Waring",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            // tra lai ve connection
            return connection;
        }
    }
}
