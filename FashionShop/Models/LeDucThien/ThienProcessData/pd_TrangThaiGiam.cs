using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_TrangThaiGiam
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        // Phương thức lấy trangThaiGiam theo maTrangThaiGiam
        public string GetTrangThaiGiam(int maTrangThaiGiam)
        {
            string trangThaiGiam = string.Empty; // Biến chứa giá trị trangThaiGiam trả về
            string query = "SELECT dbo.fn_GetTrangThaiGiam(@maTrangThaiGiam)"; // Gọi function SQL

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn với tham số đầu vào
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@maTrangThaiGiam", maTrangThaiGiam); // Thêm tham số

                    // Đọc kết quả trả về
                    trangThaiGiam = cmd.ExecuteScalar().ToString(); // Lấy giá trị trả về từ function

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return trangThaiGiam; // Trả về trangThaiGiam
        }

    }
}
