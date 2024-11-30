using System;

namespace FashionShop.Models.LeDucThien.Entity
{
    public class ent_TaiKhoanNganHangCuaToi
    {
        public int MaTaiKhoan { get; set; }            // ID tự tăng
        public string MaAccount { get; set; }          // Mã tài khoản (Khóa phụ)
        public int MaNganHangLienKet { get; set; }     // Khóa phụ liên kết với NganHangDuocLienKet
        public string SoTaiKhoan { get; set; }         // Số tài khoản
        public string TenChuSoHuu { get; set; }        // Tên chủ sở hữu
        public string TenChiNhanh { get; set; }        // Tên chi nhánh ngân hàng

        // Constructor mặc định
        public ent_TaiKhoanNganHangCuaToi() { }

        // Constructor có tham số
        public ent_TaiKhoanNganHangCuaToi(int maTaiKhoan, string maAccount, int maNganHangLienKet,
                                          string soTaiKhoan, string tenChuSoHuu, string tenChiNhanh)
        {
            MaTaiKhoan = maTaiKhoan;
            MaAccount = maAccount;
            MaNganHangLienKet = maNganHangLienKet;
            SoTaiKhoan = soTaiKhoan;
            TenChuSoHuu = tenChuSoHuu;
            TenChiNhanh = tenChiNhanh;
        }
    }
}
