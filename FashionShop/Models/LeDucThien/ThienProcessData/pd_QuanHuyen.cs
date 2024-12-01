using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_QuanHuyen
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy quận huyện theo điều kiện
        public List<ent_QuanHuyen> GetQuanHuyenWhereMaTinhThanh (int condition)
        {
            string query = "SELECT * FROM QuanHuyen WHERE MaTinhThanh = @MaTinhThanh";
            List<ent_QuanHuyen> list = new List<ent_QuanHuyen>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaTinhThanh", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_QuanHuyen quanHuyen = new ent_QuanHuyen
                        {
                            MaQuanHuyen = Convert.ToInt32(reader["maQuanHuyen"]),
                            MaTinhThanh = Convert.ToInt32(reader["maTinhThanh"]),
                            TenQuanHuyen = reader["TenQuanHuyen"].ToString()
                        };

                        list.Add(quanHuyen);
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

        // Lấy tên Quận/Huyện từ MaQuanHuyen
        public string GetTenQuanHuyen(int maQuanHuyen)
        {
            string tenQuanHuyen = string.Empty;
            string query = "SELECT dbo.GetTenQuanHuyen(@MaQuanHuyen) AS TenQuanHuyen";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaQuanHuyen", maQuanHuyen);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tenQuanHuyen = reader["TenQuanHuyen"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi hàm GetTenQuanHuyen: " + ex.Message);
                }
            }

            return tenQuanHuyen;
        }
    }
}
