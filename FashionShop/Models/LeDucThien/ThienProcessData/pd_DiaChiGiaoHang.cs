using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_DiaChiGiaoHang
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy địa chỉ giao hàng theo điều kiện
        public List<ent_DiaChiGiaoHang> GetDiaChiGiaoHangWhere(string maAccount)
        {
            string query = "SELECT * FROM DiaChiGiaoHang WHERE MaAccount = @MaAccount";
            List<ent_DiaChiGiaoHang> list = new List<ent_DiaChiGiaoHang>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaAccount", maAccount);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ent_DiaChiGiaoHang diaChi = new ent_DiaChiGiaoHang
                            {
                                MaDiaChi = Convert.ToInt32(reader["MaDiaChi"]),
                                MaAccount = reader["MaAccount"].ToString(),
                                MaTinhThanh = Convert.ToInt32(reader["MaTinhThanh"]),
                                MaQuanHuyen = Convert.ToInt32(reader["MaQuanHuyen"]),
                                MaXaPhuong = Convert.ToInt32(reader["MaXaPhuong"]),
                                TenKhachHang = reader["TenKhachHang"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                DiaChiGiaoHang = reader["DiaChiGiaoHang"].ToString()
                            };

                            list.Add(diaChi);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return list;
        }
    }
}
