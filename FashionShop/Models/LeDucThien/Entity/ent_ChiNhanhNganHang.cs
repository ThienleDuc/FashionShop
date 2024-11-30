using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.Entity
{
    public class ent_ChiNhanhNganHang
    {
        public int MaChiNhanh { get; set; }          // ID tự tăng
        public int MaNganHangLienKet { get; set; }   // Khóa phụ liên kết với NganHangDuocLienKet
        public string TenChiNhanh { get; set; }      // Tên chi nhánh ngân hàng

        // Constructor mặc định
        public ent_ChiNhanhNganHang() { }

        // Constructor có tham số
        public ent_ChiNhanhNganHang(int maChiNhanh, int maNganHangLienKet, string tenChiNhanh)
        {
            MaChiNhanh = maChiNhanh;
            MaNganHangLienKet = maNganHangLienKet;
            TenChiNhanh = tenChiNhanh;
        }
    }
}