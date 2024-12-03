using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeMinhToan.Entity;

namespace FashionShop.Models.LeMinhToan.ProcessData
{
    public class pd_SanPham
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        public List<ent_SanPham> GetTopSanPham()
        {
            List<ent_SanPham> topSanPham = new List<ent_SanPham>();

            string query = "SELECT TOP 8 * FROM SanPham ORDER BY soLuongBanRa DESC";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ent_SanPham sanPham = new ent_SanPham
                            {
                                IdSanPham = reader["id"].ToString(),
                                MaPhanLoai = Convert.ToInt32(reader["maphanLoai"]),
                                Anh = reader["anh"].ToString(),
                                TenSanPham = reader["tenSanPham"].ToString(),
                                Price = Convert.ToSingle(reader["price"]),
                                MoTa = reader["moTa"].ToString(),
                                SoLuongHienCo = Convert.ToInt32(reader["SoLuongHienCo"]),
                                SoLuongBanRa = Convert.ToInt32(reader["soLuongBanRa"])
                            };

                            topSanPham.Add(sanPham);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return topSanPham;
        }

        public List<ent_SanPham> GetNewSanPham()
        {
            List<ent_SanPham> latestSanPham = new List<ent_SanPham>();

            string query = @"SELECT * 
                             FROM SanPham
                             ORDER BY id DESC
                             OFFSET 0 ROWS
                             FETCH NEXT 8 ROWS ONLY;";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ent_SanPham sanPham = new ent_SanPham
                            {
                                IdSanPham = reader["id"].ToString(),
                                MaPhanLoai = Convert.ToInt32(reader["maphanLoai"]),
                                Anh = reader["anh"].ToString(),
                                TenSanPham = reader["tenSanPham"].ToString(),
                                Price = Convert.ToSingle(reader["price"]),
                                MoTa = reader["moTa"].ToString(),
                                SoLuongHienCo = Convert.ToInt32(reader["SoLuongHienCo"]),
                                SoLuongBanRa = Convert.ToInt32(reader["soLuongBanRa"])
                            };

                            latestSanPham.Add(sanPham);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return latestSanPham;
        }
    }
}