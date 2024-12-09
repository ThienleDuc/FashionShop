using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_DiaChiGiaoHang
    {
        private readonly ConnectionDatabase con = new ConnectionDatabase();

        public List<ent_DiaChiGiaoHang> GetDiaChiGiaoHangByAccount(string maAccount)
        {
            List<ent_DiaChiGiaoHang> diaChiList = new List<ent_DiaChiGiaoHang>(); // Danh sách kết quả

            string procedureName = "GetDiaChiGiaoHangByAccount"; // Tên stored procedure

            // Tạo kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Khởi tạo command để gọi stored procedure
                    SqlCommand cmd = new SqlCommand(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Đặt kiểu là stored procedure
                    };

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@maAccount", maAccount);

                    // Thực thi và lấy kết quả trả về
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Đọc kết quả trả về từ SQL
                    while (reader.Read())
                    {
                        // Tạo đối tượng ent_DiaChiGiaoHang từ dữ liệu trong mỗi bản ghi
                        ent_DiaChiGiaoHang diaChi = new ent_DiaChiGiaoHang
                        {
                            MaDiaChi = Convert.ToInt32(reader["MaDiaChi"]),
                            TenTinhThanh = reader["TenTinhThanh"].ToString(),
                            TenQuanHuyen = reader["TenQuanHuyen"].ToString(),
                            TenXaPhuong = reader["TenXaPhuong"].ToString(),
                            TenKhachHang = reader["TenKhachHang"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            DiaChiGiaoHang = reader["DiaChiGiaoHang"].ToString()
                        };

                        // Thêm đối tượng vào danh sách kết quả
                        diaChiList.Add(diaChi);
                    }

                    reader.Close(); // Đóng SqlDataReader
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi stored procedure: " + ex.Message);
                }
            }

            return diaChiList; // Trả về danh sách kết quả
        }

    }
}
