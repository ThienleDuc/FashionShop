using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeDucThien.ProcessData
{
    public class pd_ChiNhanhNganHang
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy danh sách chi nhánh ngân hàng
        public List<ent_ChiNhanhNganHang> GetAllChiNhanhNganHang()
        {
            string query = "SELECT * FROM ChiNhanhNganHang";
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
                        ent_ChiNhanhNganHang branch = new ent_ChiNhanhNganHang
                        {
                            MaChiNhanh = Convert.ToInt32(reader["maChiNhanh"]),
                            MaNganHangLienKet = Convert.ToInt32(reader["maNganHangLienKet"]),
                            TenChiNhanh = reader["TenChiNhanh"].ToString()
                        };

                        list.Add(branch);
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

        // Phương thức lấy chi nhánh ngân hàng theo điều kiện
        public List<ent_ChiNhanhNganHang> GetChiNhanhNganHangWhere(string condition)
        {
            string query = "SELECT * FROM ChiNhanhNganHang WHERE TenChiNhanh = @TenChiNhanh";
            List<ent_ChiNhanhNganHang> list = new List<ent_ChiNhanhNganHang>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenChiNhanh", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_ChiNhanhNganHang branch = new ent_ChiNhanhNganHang
                        {
                            MaChiNhanh = Convert.ToInt32(reader["maChiNhanh"]),
                            MaNganHangLienKet = Convert.ToInt32(reader["maNganHangLienKet"]),
                            TenChiNhanh = reader["TenChiNhanh"].ToString()
                        };

                        list.Add(branch);
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
