using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_TinhThanh
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy danh sách tỉnh thành
        public List<ent_TinhThanhPho> GetAllTinhThanh()
        {
            string query = "SELECT * FROM TinhThanh";
            List<ent_TinhThanhPho> list = new List<ent_TinhThanhPho>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_TinhThanhPho tinhThanh = new ent_TinhThanhPho
                        {
                            MaTinhThanh = Convert.ToInt32(reader["maTinhThanh"]),
                            TenTinhThanh = reader["TenTinhThanh"].ToString()
                        };

                        list.Add(tinhThanh);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return list;
        }

        // Lấy tên Tỉnh/Thành phố từ MaTinhThanh
        public string GetTenTinhThanh(int maTinhThanh)
        {
            string tenTinhThanh = string.Empty;
            string query = "SELECT dbo.GetTenTinhThanh(@MaTinhThanh) AS TenTinhThanh";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaTinhThanh", maTinhThanh);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tenTinhThanh = reader["TenTinhThanh"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi hàm GetTenTinhThanh: " + ex.Message);
                }
            }

            return tenTinhThanh;
        }

    }
}
