using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_ThemTaiKhoanNganHang
    {
        public string MaAccount { get; set; } // Mã tài khoản người dùng
        public int MaNganHangLienKet { get; set; } // Mã ngân hàng liên kết
        public string SoTaiKhoan { get; set; } // Số tài khoản ngân hàng
        public string TenChuSoHuu { get; set; } // Tên chủ sở hữu tài khoản
        public string TenChiNhanh { get; set; } // Tên chi nhánh ngân hàng

        // Constructor mặc định
        public ent_ThemTaiKhoanNganHang() { }

        // Constructor với đầy đủ tham số
        public ent_ThemTaiKhoanNganHang(string maAccount, int maNganHangLienKet, string soTaiKhoan, string tenChuSoHuu, string tenChiNhanh)
        {
            MaAccount = maAccount;
            MaNganHangLienKet = maNganHangLienKet;
            SoTaiKhoan = soTaiKhoan;
            TenChuSoHuu = tenChuSoHuu;
            TenChiNhanh = tenChiNhanh;
        }
    }
}