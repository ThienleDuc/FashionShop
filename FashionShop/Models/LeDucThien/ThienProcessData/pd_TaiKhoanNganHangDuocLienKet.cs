using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_TaiKhoanNganHangDuocLienKet
    {
        private ConnectionDatabase con = new ConnectionDatabase();
       
        // Phương thức lấy TenChuSoHuu từ số tài khoản sử dụng function SQL
        public string GetTenChuSoHuuBySoTaiKhoan(string soTaiKhoan, int maNganHangLienKet)
        {
            string query = "SELECT dbo.GetTenChuSoHuu(@SoTaiKhoan, @MaNganHangLienKet)";
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
                    cmd.Parameters.AddWithValue("@MaNganHangLienKet", maNganHangLienKet);

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
