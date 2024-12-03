using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeMinhToan.ProcessData
{
    public class dk_KhachHang
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        public void RegisterCustomer(ent_KhachHang KhachHangDK)
        {
            string query = "INSERT INTO KhachHang (username, matKhau, firstName, lastName, day, moth, year, gender, anh) " +
                           "VALUES (@Username, @MatKhau, @FirstName, @LastName, @Day, @Month, @Year, @Gender, @Anh)";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", KhachHangDK.Username);
                    cmd.Parameters.AddWithValue("@MatKhau", KhachHangDK.MatKhau);
                    cmd.Parameters.AddWithValue("@FirstName", KhachHangDK.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", KhachHangDK.LastName);
                    cmd.Parameters.AddWithValue("@Day", KhachHangDK.Day);
                    cmd.Parameters.AddWithValue("@Month", KhachHangDK.Moth); // Corrected typo
                    cmd.Parameters.AddWithValue("@Year", KhachHangDK.Year);
                    cmd.Parameters.AddWithValue("@Gender", KhachHangDK.Gender);
                    cmd.Parameters.AddWithValue("@Anh", "~img/anh1");

                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh INSERT
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm dữ liệu: " + ex.Message);
                }
            }
        }
    }
}