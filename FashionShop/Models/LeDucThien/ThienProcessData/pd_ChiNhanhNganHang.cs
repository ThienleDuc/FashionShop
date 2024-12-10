using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Models.LeDucThien.ThienProcessData
{
    public class pd_ChiNhanhNganHang
    {
        private ConnectionDatabase con = new ConnectionDatabase();

        // Phương thức lấy ngân hàng theo điều kiện (Ví dụ: theo mã ngân hàng)
        public List<ent_ChiNhanhNganHang> GetTenChiNhanh(int maNganHangLienKet)
        {
            List<ent_ChiNhanhNganHang> list = new List<ent_ChiNhanhNganHang>();
            string procedureName = "pr_LayTenChiNhanh";  // Tên của stored procedure

            try
            {
                using (SqlConnection connection = con.GetConnection())
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(procedureName, connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure 
                    };

                    cmd.Parameters.AddWithValue("@maNganHangLienKet", maNganHangLienKet);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ent_ChiNhanhNganHang chiNhanhNganHang = new ent_ChiNhanhNganHang
                        {
                            MaChiNhanh = Convert.ToInt32(reader["maChiNhanh"]),
                            TenChiNhanh = reader["TenChiNhanh"].ToString()
                        };

                        list.Add(chiNhanhNganHang);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
            }
            return list; // Trả về danh sách chi nhánh
        }
    }
}
