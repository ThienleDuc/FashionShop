using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeDucThien.ProcessData
{
    public class pd_TaiKhoanNganHangDuocLienKet
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy tất cả tài khoản ngân hàng được liên kết
        public List<ent_TaiKhoanNganHangDuocLienKet> GetAllTaiKhoanNganHangDuocLienKet()
        {
            string query = "SELECT * FROM TaiKhoanNganHangDuocLienKet";
            List<ent_TaiKhoanNganHangDuocLienKet> list = new List<ent_TaiKhoanNganHangDuocLienKet>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_TaiKhoanNganHangDuocLienKet account = new ent_TaiKhoanNganHangDuocLienKet
                        {
                            SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                            MaNganHangLienKet = Convert.ToInt32(reader["MaNganHangLienKet"]),
                            TenChuSoHuu = reader["TenChuSoHuu"].ToString()
                        };

                        list.Add(account);
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

        // Phương thức lấy tài khoản ngân hàng được liên kết theo điều kiện
        public List<ent_TaiKhoanNganHangDuocLienKet> GetTaiKhoanNganHangDuocLienKetWhere(string condition)
        {
            string query = "SELECT * FROM TaiKhoanNganHangDuocLienKet WHERE SoTaiKhoan = @SoTaiKhoan";
            List<ent_TaiKhoanNganHangDuocLienKet> list = new List<ent_TaiKhoanNganHangDuocLienKet>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_TaiKhoanNganHangDuocLienKet account = new ent_TaiKhoanNganHangDuocLienKet
                        {
                            SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                            MaNganHangLienKet = Convert.ToInt32(reader["MaNganHangLienKet"]),
                            TenChuSoHuu = reader["TenChuSoHuu"].ToString()
                        };

                        list.Add(account);
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

        // Phương thức lấy TenChuSoHuu từ số tài khoản sử dụng function SQL
        public string GetTenChuSoHuuBySoTaiKhoan(string soTaiKhoan)
        {
            string query = "SELECT dbo.GetTenChuSoHuu(@SoTaiKhoan)"; // Sử dụng function GetTenChuSoHuu
            string tenChuSoHuu = null;

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn với tham số
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan); // Thêm tham số số tài khoản

                    // Thực thi và lấy giá trị trả về
                    tenChuSoHuu = cmd.ExecuteScalar()?.ToString(); // ExecuteScalar để lấy giá trị trả về từ function

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return tenChuSoHuu; // Trả về tên chủ sở hữu
        }
    }
}
