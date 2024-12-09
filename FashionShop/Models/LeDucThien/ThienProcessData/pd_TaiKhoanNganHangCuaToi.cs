using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_TaiKhoanNganHangCuaToi
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        public List<ent_TaiKhoanNganHangCuaToi> GetTaiKhoanNganHangCuaToiWhere(string maAccount)
        {
            List<ent_TaiKhoanNganHangCuaToi> result = new List<ent_TaiKhoanNganHangCuaToi>();
            string procedureName = "pr_TaiKhoanNganHangCuaToi"; // Tên stored procedure

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Đặt kiểu là stored procedure
                    };

                    // Thêm tham số cho stored procedure bằng AddWithValue
                    cmd.Parameters.AddWithValue("@maAccount", maAccount);

                    // Thực thi stored procedure và lấy dữ liệu trả về
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lấy dữ liệu từ SqlDataReader và gán vào đối tượng ent_TaiKhoanNganHangCuaToi
                            var taiKhoan = new ent_TaiKhoanNganHangCuaToi
                            {
                                BankID = Convert.ToInt32(reader["BankID"]), // Mã tài khoản ngân hàng
                                BankName = reader["BankName"].ToString(),   // Tên ngân hàng
                                BankLogo = reader["BankLogo"].ToString(),   // Logo ngân hàng
                                AccountOwner = reader["AccountOwner"].ToString(), // Tên chủ sở hữu tài khoản
                                BranchName = reader["BranchName"].ToString(),  // Tên chi nhánh
                                AccountNumber = reader["AccountNumber"].ToString() // Số tài khoản
                            };

                            // Thêm đối tượng vào danh sách kết quả
                            result.Add(taiKhoan);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine("Lỗi khi lấy thông tin tài khoản ngân hàng: " + ex.Message);
                    throw;
                }
            }

            return result;
        }


        public void DeleteTaiKhoanNganHangCuaToi(int maTaiKhoan)
        {
            string query = "DELETE FROM TaiKhoanNganHangCuaToi WHERE MaTaiKhoan = @MaTaiKhoan";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);  // Đảm bảo sử dụng đúng tham số MaTaiKhoan

                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh DELETE
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi xóa tài khoản ngân hàng: " + ex.Message);
                    throw; // Xử lý lỗi hoặc bỏ qua
                }
            }
        }
    }
}
