using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_XaPhuong
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy xã phường theo điều kiện
        public List<ent_XaPhuong> GetXaPhuongWhereMaQuanHuyen (int condition)
        {
            string query = "SELECT * FROM XaPhuong WHERE MaQuanHuyen = @MaQuanHuyen";
            List<ent_XaPhuong> list = new List<ent_XaPhuong>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaQuanHuyen", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_XaPhuong xaPhuong = new ent_XaPhuong
                        {
                            MaXaPhuong = Convert.ToInt32(reader["maXaPhuong"]),
                            MaQuanHuyen = Convert.ToInt32(reader["maQuanHuyen"]),
                            TenXaPhuong = reader["TenXaPhuong"].ToString()
                        };

                        list.Add(xaPhuong);
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

        // Lấy tên Xã/Phường từ MaXaPhuong
        public string GetTenXaPhuong(int maXaPhuong)
        {
            string tenXaPhuong = string.Empty;
            string query = "SELECT dbo.GetTenXaPhuong(@MaXaPhuong) AS TenXaPhuong";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaXaPhuong", maXaPhuong);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tenXaPhuong = reader["TenXaPhuong"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi hàm GetTenXaPhuong: " + ex.Message);
                }
            }

            return tenXaPhuong;
        }
    }
}
