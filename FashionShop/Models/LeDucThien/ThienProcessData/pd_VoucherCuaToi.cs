using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_VoucherCuaToi
    {
        private ConnectionDatabase con = new ConnectionDatabase(); // Khởi tạo đối tượng ConnectionDatabase

        // Phương thức lấy voucher của tôi theo maAccount
        public List<ent_VoucherCuaToi> GetVoucherCuaToiByAccount(string maAccount)
        {
            string query = "SELECT * FROM VoucherCuaToi WHERE maAccount = @maAccount";
            List<ent_VoucherCuaToi> list = new List<ent_VoucherCuaToi>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@maAccount", maAccount);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_VoucherCuaToi voucherCuaToi = new ent_VoucherCuaToi
                        {
                            maVoucherCuaToi = reader["maVoucherCuaToi"].ToString(),
                            maVoucher = reader["maVoucher"].ToString(),
                            maAccount = reader["maAccount"].ToString(),
                            trangThaiSuDung = reader["trangThaiSuDung"].ToString()
                        };

                        list.Add(voucherCuaToi);
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

        // Phương thức cập nhật trạng thái voucher của tôi
        public bool UpdateTrangThaiSuDung(string maVoucherCuaToi, string trangThaiSuDung)
        {
            string query = "UPDATE VoucherCuaToi SET trangThaiSuDung = @trangThaiSuDung WHERE maVoucherCuaToi = @maVoucherCuaToi";

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@maVoucherCuaToi", maVoucherCuaToi);
                    cmd.Parameters.AddWithValue("@trangThaiSuDung", trangThaiSuDung);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi cập nhật trạng thái voucher: " + ex.Message);
                    return false;
                }
            }
        }
        
    }
}
