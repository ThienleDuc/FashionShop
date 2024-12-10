using System;
using System.Collections.Generic;
using System.Data;
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
                            DiaChiGiaoHang = reader["DiaChi"].ToString()
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

        public void XoaDiaChiGiaoHang(int maDiaChi)
        {
            string query = "DELETE FROM DiaChiGiaoHang WHERE MaDiaChi = @MaDiaChi"; // Câu lệnh SQL

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaDiaChi", maDiaChi); // Thêm tham số vào câu truy vấn

                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL (không cần kiểm tra số dòng bị ảnh hưởng)
                }
                catch (Exception ex)
                {
                    // Log hoặc xử lý lỗi tùy theo yêu cầu
                    Console.WriteLine("Lỗi khi xóa địa chỉ: " + ex.Message);
                    // Ném ngoại lệ hoặc xử lý lỗi tùy thuộc vào yêu cầu của ứng dụng
                    throw new Exception("Không thể xóa địa chỉ giao hàng", ex);
                }
            }
        }

        public void ThemDiaChiGiaoHang(ent_ThemDiaChiGiaoHang diaChi)
        {
            string query = "pr_ThemDiaChiGiaoHang"; // Tên stored procedure
            using (SqlConnection connection = con.GetConnection()) // Giả sử con.GetConnection() là kết nối đến cơ sở dữ liệu
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@maAccount", diaChi.MaAccount);
                    cmd.Parameters.AddWithValue("@MaTinhThanh", diaChi.MaTinhThanh);
                    cmd.Parameters.AddWithValue("@MaQuanHuyen", diaChi.MaQuanHuyen);
                    cmd.Parameters.AddWithValue("@MaXaPhuong", diaChi.MaXaPhuong);
                    cmd.Parameters.AddWithValue("@TenKhachHang", diaChi.TenKhachHang);
                    cmd.Parameters.AddWithValue("@SDT", diaChi.SDT);
                    cmd.Parameters.AddWithValue("@DiaChiGiaoHang", diaChi.DiaChiGiaoHang);

                    // Thực thi stored procedure
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu có
                    Console.WriteLine("Lỗi khi thêm địa chỉ giao hàng: " + ex.Message);
                    // Ném lỗi nếu cần
                    throw new Exception("Không thể thêm địa chỉ giao hàng", ex);
                }
            }
        }


        public void CapNhatDiaChiGiaoHang(ent_ThemDiaChiGiaoHang diaChi)
        {
            string query = "pr_CapNhatDiaChiGiaoHang"; // Tên stored procedure
            using (SqlConnection connection = con.GetConnection()) // Giả sử con.GetConnection() là kết nối đến cơ sở dữ liệu
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@MaDiaChi", diaChi.MaDiaChi); // Thêm tham số cho MaDiaChi để xác định địa chỉ cần cập nhật
                    cmd.Parameters.AddWithValue("@MaAccount", diaChi.MaAccount);
                    cmd.Parameters.AddWithValue("@MaTinhThanh", diaChi.MaTinhThanh);
                    cmd.Parameters.AddWithValue("@MaQuanHuyen", diaChi.MaQuanHuyen);
                    cmd.Parameters.AddWithValue("@MaXaPhuong", diaChi.MaXaPhuong);
                    cmd.Parameters.AddWithValue("@TenKhachHang", diaChi.TenKhachHang);
                    cmd.Parameters.AddWithValue("@SDT", diaChi.SDT);
                    cmd.Parameters.AddWithValue("@DiaChiGiaoHang", diaChi.DiaChiGiaoHang);

                    // Thực thi stored procedure
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu có
                    Console.WriteLine("Lỗi khi cập nhật địa chỉ giao hàng: " + ex.Message);
                    // Ném lỗi nếu cần
                    throw new Exception("Không thể cập nhật địa chỉ giao hàng", ex);
                }
            }
        }

        public List<ent_ThemDiaChiGiaoHang> getAllDiaChiWhereMaDiaChi(int maDiaChi)
        {
            string query = "pr_LayTatCaBoiMaDiaChi"; // Tên stored procedure
            List<ent_ThemDiaChiGiaoHang> diaChiList = new List<ent_ThemDiaChiGiaoHang>();

            using (SqlConnection connection = con.GetConnection()) // Giả sử con.GetConnection() là kết nối đến cơ sở dữ liệu
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@MaDiaChi", maDiaChi); 

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Đọc dữ liệu từ SqlDataReader và chuyển thành đối tượng
                    while (reader.Read())
                    {
                        ent_ThemDiaChiGiaoHang diaChi = new ent_ThemDiaChiGiaoHang
                        {
                            MaDiaChi = reader.GetInt32(reader.GetOrdinal("MaDiaChi")),
                            MaAccount = reader.GetString(reader.GetOrdinal("maAccount")),
                            MaTinhThanh = reader.GetInt32(reader.GetOrdinal("MaTinhThanh")),
                            MaQuanHuyen = reader.GetInt32(reader.GetOrdinal("MaQuanHuyen")),
                            MaXaPhuong = reader.GetInt32(reader.GetOrdinal("MaXaPhuong")),
                            TenKhachHang = reader.GetString(reader.GetOrdinal("TenKhachHang")),
                            SDT = reader.GetString(reader.GetOrdinal("SDT")),
                            DiaChiGiaoHang = reader.GetString(reader.GetOrdinal("DiaChi"))
                        };
                        diaChiList.Add(diaChi);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu có
                    Console.WriteLine("Lỗi khi lấy địa chỉ giao hàng: " + ex.Message);
                    // Ném lỗi nếu cần
                    throw new Exception("Không thể lấy địa chỉ giao hàng", ex);
                }
            }

            return diaChiList;
        }
    }
}
