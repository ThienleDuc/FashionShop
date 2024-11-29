using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FashionShop.Models
{
    public class ConnectionDatabase
    {
        // Chuỗi kết nối
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        // Phương thức trả về đối tượng SqlConnection
        public SqlConnection GetConnection()
        {
            try
            {
                // Tạo một kết nối mới và trả về
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và có thể ném lỗi hoặc trả về null
                Console.WriteLine("Lỗi khi tạo kết nối: " + ex.Message);
                return null;
            }
        }
    }
}   