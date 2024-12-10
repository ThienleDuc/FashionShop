using System;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_DonHang
    {
        public string MaDonHang { get; set; }   // Mã đơn hàng
        public string MaAccount { get; set; }    // Mã tài khoản
        public int TongTien { get; set; }        // Tổng tiền thanh toán

        // Constructor mặc định
        public ent_DonHang() { }

        // Constructor có tham số
        public ent_DonHang(string maDonHang, string maAccount, int tongTien)
        {
            MaDonHang = maDonHang;
            MaAccount = maAccount;
            TongTien = tongTien;
        }
    }
}
