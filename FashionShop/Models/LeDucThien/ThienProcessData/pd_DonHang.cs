using FashionShop.Models.LeDucThien.ThienEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_DonHang
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        public List<ent_DonHang> GetDonHangByTrangThai(string trangThai)
        {
            string query = "pr_LayDonHangTheoTrangThai";
            List<ent_DonHang> list = new List<ent_DonHang>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_DonHang donHang = new ent_DonHang
                        {
                            MaDonHang = reader["maDonHang"].ToString(),
                            MaAccount = reader["maAccount"].ToString(),
                            TongTien = Convert.ToInt32(reader["TongTien"]) 
                        };
                        list.Add(donHang);
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


        public List<ent_ChiTietDonHang> LayChiTietDonHang(string maDonHang)
        {
            string procedureName = "pr_LayChiTietDonHang"; // Tên stored procedure
            List<ent_ChiTietDonHang> chiTietDonHangList = new List<ent_ChiTietDonHang>(); // Danh sách chứa chi tiết đơn hàng

            if (string.IsNullOrEmpty(maDonHang))
            {
                throw new ArgumentException("Mã đơn hàng không được để trống.", nameof(maDonHang));
            }

            using (SqlConnection connection = con.GetConnection()) // Đảm bảo kết nối được đóng tự động
            {
                try
                {
                    connection.Open(); // Mở kết nối với cơ sở dữ liệu

                    using (SqlCommand cmd = new SqlCommand(procedureName, connection)) // Tạo SqlCommand với stored procedure
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Đặt kiểu của command là stored procedure

                        // Thêm tham số đầu vào cho stored procedure
                        cmd.Parameters.Add(new SqlParameter("@maDonHang", SqlDbType.VarChar, 15) { Value = maDonHang });

                        // Thực thi stored procedure và lấy kết quả trả về
                        using (SqlDataReader reader = cmd.ExecuteReader()) // Đọc dữ liệu từ SqlDataReader
                        {
                            while (reader.Read())
                            {
                                // Tạo đối tượng chi tiết đơn hàng và gán dữ liệu từ reader
                                ent_ChiTietDonHang chiTietDonHang = new ent_ChiTietDonHang
                                {
                                    TenSanPham = reader["TenSanPham"].ToString(),
                                    AnhSanPham = reader["AnhSanPham"].ToString(),
                                    PhanLoai = reader["PhanLoai"].ToString(),
                                    SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                    GiaTien = Convert.ToDecimal(reader["GiaTien"]),
                                };

                                // Thêm đối tượng vào danh sách
                                chiTietDonHangList.Add(chiTietDonHang);
                            }
                        } // SqlDataReader tự động đóng sau khi ra khỏi using block
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi: Ghi log hoặc ném lại lỗi
                    Console.WriteLine("Lỗi khi lấy chi tiết đơn hàng: " + ex.Message);
                    throw; // Nếu cần ném lại lỗi cho cấp trên xử lý
                }
            }

            return chiTietDonHangList; // Trả về danh sách chi tiết đơn hàng
        }

    }
}