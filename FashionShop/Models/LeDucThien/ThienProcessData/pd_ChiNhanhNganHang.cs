using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_ChiNhanhNganHang
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy ngân hàng theo điều kiện (Ví dụ: theo mã ngân hàng)
        public List<ent_ChiNhanhNganHang> GetChiNhanhNganHangWhereMaNganHangLienKet(int maNganHangLienKet)
        {
            string query = "SELECT maChiNhanh, TenChiNhanh FROMChiNhanhNganHang WHERE maNganHangLienKet = @maNganHangLienKet";
            List<ent_ChiNhanhNganHang> list = new List<ent_ChiNhanhNganHang>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_ChiNhanhNganHang chiNhanhNganHang = new ent_ChiNhanhNganHang
                        {
                            MaChiNhanh = Convert.ToInt32(reader["maChiNhanh"]),
                            MaNganHangLienKet = Convert.ToInt32(reader["maNganHangLienKet"]),
                            TenChiNhanh = reader["TenChiNhanh"].ToString()
                        };

                        list.Add(chiNhanhNganHang);
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
    }
}
