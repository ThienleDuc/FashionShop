using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_VoucherCuaToi
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        public List<ent_VoucherCuaToi> GetVoucherCuaToi(string maAccount)
        {
            List<ent_VoucherCuaToi> vouchers = new List<ent_VoucherCuaToi>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("pr_VoucherCuaToi", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Thêm tham số đầu vào cho stored procedure
                        cmd.Parameters.AddWithValue("@maAccount", maAccount);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Lấy dữ liệu từ reader và tạo đối tượng ent_VoucherCuaToi
                                ent_VoucherCuaToi voucher = new ent_VoucherCuaToi
                                {
                                    MaVoucherCuaToi = reader["maVoucherCuaToi"].ToString(),
                                    MaVoucher = reader["maVoucher"].ToString(),
                                    TenVoucher = reader["tenVoucher"].ToString(),
                                    HanSuDung = reader["hanSuDung"].ToString(), // Định dạng dd/MM/yyyy đã được xử lý trong stored procedure
                                    MucGiam = Convert.ToInt32(reader["mucGiam"]),
                                    DieuKienGiam = reader["DieuKienGiam"].ToString(),
                                    TrangThaiSuDung = reader["trangThaiSuDung"].ToString()
                                };

                                vouchers.Add(voucher); // Thêm vào danh sách
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy dữ liệu voucher: " + ex.Message);
                    throw;
                }
            }

            return vouchers;
        }

        public void ThemMaVoucherCuaToi(string maVoucher, string maAccount)
        {
            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Khai báo đối tượng SqlCommand để gọi stored procedure pr_ThemMaVoucherCuaToi
                    using (SqlCommand cmd = new SqlCommand("pr_ThemMaVoucherCuaToi", connection))
                    {
                        // Đặt kiểu lệnh là Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm tham số vào cho stored procedure
                        cmd.Parameters.AddWithValue("@maVoucher", maVoucher);
                        cmd.Parameters.AddWithValue("@maAccount", maAccount);

                        // Thực thi lệnh Insert trong stored procedure
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, in ra lỗi
                    Console.WriteLine("Lỗi khi thêm voucher: " + ex.Message);
                }
            }
        }

    }

}
