using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Models.LeDucThien.ProcessData
{
    public class pd_TaiKhoanNganHangCuaToi
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy tài khoản ngân hàng của tôi theo điều kiện (bao gồm các trường mới)
        public List<ent_TaiKhoanNganHangCuaToi> GetTaiKhoanNganHangCuaToiWhere(string condition)
        {
            string query = "SELECT * FROM TaiKhoanNganHangCuaToi WHERE maAccount = @maAccount";
            List<ent_TaiKhoanNganHangCuaToi> list = new List<ent_TaiKhoanNganHangCuaToi>();

            using (SqlConnection connection = con.GetConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@maAccount", condition);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Tạo đối tượng ent_TaiKhoanNganHangCuaToi từ dữ liệu trong mỗi bản ghi
                        ent_TaiKhoanNganHangCuaToi account = new ent_TaiKhoanNganHangCuaToi
                        {
                            MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                            MaAccount = reader["maAccount"].ToString(),
                            MaNganHangLienKet = Convert.ToInt32(reader["maNganHangLienKet"]),
                            SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                            TenChuSoHuu = reader["TenChuSoHuu"].ToString(),
                            TenChiNhanh = reader["TenChiNhanh"].ToString()
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
    }
}
