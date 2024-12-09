﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_KhachHang
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        // Phương thức lấy danh sách người dùng từ cơ sở dữ liệu
        public List<ent_KhachHang> GetAccountUsers()
        {
            string query = "SELECT * FROM KhachHang"; // Câu truy vấn SQL để lấy tất cả người dùng
            List<ent_KhachHang> list = new List<ent_KhachHang>();

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader(); // Đọc kết quả trả về

                    // Lặp qua các bản ghi trả về và chuyển đổi thành đối tượng ent_KhachHang
                    while (reader.Read())
                    {
                        // Tạo đối tượng ent_KhachHang từ dữ liệu trong mỗi bản ghi
                        ent_KhachHang user = new ent_KhachHang
                        {
                            Username = reader["username"].ToString(),
                            MatKhau = reader["matKhau"].ToString(),
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Day = Convert.ToInt32(reader["day"]),
                            Moth = Convert.ToInt32(reader["moth"]),  // Sửa lại tên cột "moth" nếu cần
                            Year = Convert.ToInt32(reader["year"]),
                            Gender = reader["gender"].ToString(),
                            Anh = reader["anh"].ToString() // Đường dẫn ảnh
                        };

                        // Thêm đối tượng vào danh sách
                        list.Add(user);
                    }

                    reader.Close(); // Đóng SqlDataReader
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return list; // Trả về danh sách người dùng
        }

        /// Phương thức lấy thông tin khách hàng theo điều kiện (ví dụ: theo tên đăng nhập)
        public List<ent_KhachHang> GetKhachHangWhere(string condition)
        {
            // Sử dụng tham số trong câu truy vấn
            string query = "SELECT * FROM KhachHang WHERE username = @username";
            List<ent_KhachHang> list = new List<ent_KhachHang>();

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    // Thực hiện câu truy vấn với tham số đầu vào
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", condition); // Thêm tham số username với giá trị điều kiện

                    SqlDataReader reader = cmd.ExecuteReader(); // Đọc kết quả trả về

                    // Lặp qua các bản ghi trả về và chuyển đổi thành đối tượng ent_KhachHang
                    while (reader.Read())
                    {
                        // Tạo đối tượng ent_KhachHang từ dữ liệu trong mỗi bản ghi
                        ent_KhachHang user = new ent_KhachHang
                        {
                            Username = reader["username"].ToString(),
                            MatKhau = reader["matKhau"].ToString(),
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Day = Convert.ToInt32(reader["day"]),
                            Moth = Convert.ToInt32(reader["moth"]),
                            Year = Convert.ToInt32(reader["year"]),
                            Gender = reader["gender"].ToString(),
                            Anh = reader["anh"].ToString()
                        };

                        // Thêm đối tượng vào danh sách
                        list.Add(user);
                    }

                    reader.Close(); // Đóng SqlDataReader
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }

            return list; // Trả về danh sách khách hàng tìm thấy
        }

        public void CapNhatKhachHang(string username, string firstName,
            string lastName, int day, int moth, int year, string gender)
        {
            string procedureName = "pr_CapNhatKhachHang"; // Tên stored procedure

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Đặt kiểu là stored procedure
                    };

                    // Thêm tham số đầu vào cho stored procedure
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@moth", moth);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@gender", gender);

                    cmd.ExecuteNonQuery(); // Thực thi stored procedure
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi: Ghi log hoặc ném lại lỗi
                    Console.WriteLine("Lỗi khi cập nhật dữ liệu: " + ex.Message);
                    throw; // Nếu cần ném lại lỗi cho cấp trên xử lý
                }
            }
        }

        public void CapNhatAnhKhachHang(string username, string avatar)
        {
            string procedureName = "pr_CapNhatAnhKhachHang"; // Tên stored procedure

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@anh", avatar);

                    cmd.ExecuteNonQuery(); // Thực thi stored procedure
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine("Lỗi khi cập nhật ảnh: " + ex.Message);
                    throw;
                }
            }
        }
    }

}
