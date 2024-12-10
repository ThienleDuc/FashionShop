using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_ThemDiaChiGiaoHang
    {
        public int MaDiaChi { get; set; }        // Mã địa chỉ giao hàng (Primary Key, IDENTITY)
        public string MaAccount { get; set; }    // Mã tài khoản người dùng
        public int MaTinhThanh { get; set; }     // Mã tỉnh/thành phố
        public int MaQuanHuyen { get; set; }     // Mã quận/huyện
        public int MaXaPhuong { get; set; }      // Mã xã/phường
        public string TenKhachHang { get; set; } // Tên khách hàng
        public string SDT { get; set; }          // Số điện thoại của khách hàng
        public string DiaChiGiaoHang { get; set; } // Địa chỉ giao hàng chi tiết

        public ent_ThemDiaChiGiaoHang()
        {
        }

        public ent_ThemDiaChiGiaoHang(int maDiaChi, string maAccount, int maTinhThanh, int maQuanHuyen, int maXaPhuong, string tenKhachHang, string sDT, string diaChiGiaoHang)
        {
            MaDiaChi = maDiaChi;
            MaAccount = maAccount;
            MaTinhThanh = maTinhThanh;
            MaQuanHuyen = maQuanHuyen;
            MaXaPhuong = maXaPhuong;
            TenKhachHang = tenKhachHang;
            SDT = sDT;
            DiaChiGiaoHang = diaChiGiaoHang;
        }
    }
}