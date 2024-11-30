using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.Entity
{
    public class ent_TaiKhoanNganHangDuocLienKet
    {
        public string SoTaiKhoan { get; set; }       // Số tài khoản
        public int MaNganHangLienKet { get; set; }   // Khóa phụ liên kết với NganHangDuocLienKet
        public string TenChuSoHuu { get; set; }      // Tên chủ sở hữu

        // Constructor mặc định
        public ent_TaiKhoanNganHangDuocLienKet() { }

        // Constructor có tham số
        public ent_TaiKhoanNganHangDuocLienKet(string soTaiKhoan, int maNganHangLienKet, string tenChuSoHuu)
        {
            SoTaiKhoan = soTaiKhoan;
            MaNganHangLienKet = maNganHangLienKet;
            TenChuSoHuu = tenChuSoHuu;
        }
    }
}