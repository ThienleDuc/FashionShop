using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeDucThien.ProcessData
{
    public class pd_NganHangDuocLienKet
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        // Phương thức lấy danh sách ngân hàng được liên kết
        public List<ent_NganHangDuocLienKet> GetAllNganHangDuocLienKet()
        {
            string query = "SELECT * FROM NganHangDuocLienKet"; // Câu truy vấn SQL để lấy tất cả ngân hàng
            List<ent_NganHangDuocLienKet> list = new List<ent_NganHangDuocLienKet>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_NganHangDuocLienKet bank = new ent_NganHangDuocLienKet
                        {
                            MaNganHangLienKet = Convert.ToInt32(reader["MaNganHangLienKet"]),
                            TenNganHang = reader["TenNganHang"].ToString(),
                            AnhNganHang = reader["anhnganhang"].ToString()
                        };

                        list.Add(bank);
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

        // Phương thức lấy ngân hàng theo điều kiện (Ví dụ: theo mã ngân hàng)
        public List<ent_NganHangDuocLienKet> GetNganHangDuocLienKetWhereMaNganHangLienKet (int condition)
        {
            string query = "SELECT * FROM NganHangDuocLienKet WHERE MaNganHangLienKet = @MaNganHangLienKet";
            List<ent_NganHangDuocLienKet> list = new List<ent_NganHangDuocLienKet>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaNganHangLienKet", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_NganHangDuocLienKet bank = new ent_NganHangDuocLienKet
                        {
                            MaNganHangLienKet = Convert.ToInt32(reader["MaNganHangLienKet"]),
                            TenNganHang = reader["TenNganHang"].ToString(),
                            AnhNganHang = reader["anhnganhang"].ToString()
                        };

                        list.Add(bank);
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

        // Phương thức lấy MaNganHangLienKet từ tên ngân hàng
        public int GetMaNganHangLienKetByTenNganHang(string tenNganHang)
        {
            int maNganHangLienKet = 0; // Mặc định là 0 nếu không tìm thấy

            // Câu lệnh SQL để gọi function dbo.fn_GetMaNganHangLienKet
            string query = "SELECT dbo.fn_GetMaNganHangLienKet(@TenNganHang)";

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Tạo command để thực hiện câu lệnh SQL
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenNganHang", tenNganHang); // Thêm tham số tên ngân hàng

                    // Thực thi câu lệnh và lấy kết quả
                    maNganHangLienKet = (int)cmd.ExecuteScalar(); // ExecuteScalar sẽ trả về giá trị đầu tiên trong kết quả

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return maNganHangLienKet; // Trả về giá trị MaNganHangLienKet
        }
    }
}
