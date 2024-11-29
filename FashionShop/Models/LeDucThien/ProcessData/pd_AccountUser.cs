using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeDucThien.ProcessData
{
    public class pd_AccountUser
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        // Phương thức lấy danh sách người dùng từ cơ sở dữ liệu
        public List<ent_AccountUser> GetAccountUsers()
        {
            string query = "SELECT * FROM KhachHang"; // Câu truy vấn SQL để lấy tất cả người dùng
            List<ent_AccountUser> list = new List<ent_AccountUser>();

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader(); // Đọc kết quả trả về

                    // Lặp qua các bản ghi trả về và chuyển đổi thành đối tượng ent_AccountUser
                    while (reader.Read())
                    {
                        // Tạo đối tượng ent_AccountUser từ dữ liệu trong mỗi bản ghi
                        ent_AccountUser user = new ent_AccountUser
                        {
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Day = Convert.ToInt32(reader["Day"]),
                            Month = Convert.ToInt32(reader["Month"]),
                            Year = Convert.ToInt32(reader["Year"]),
                            Gender = reader["Gender"].ToString(),
                            Password = reader["Password"].ToString()
                        };

                        // Thêm đối tượng vào danh sách
                        list.Add(user);
                    }

                    reader.Close(); // Đóng SqlDataReader
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return list; // Trả về danh sách người dùng
        }
    }
}
