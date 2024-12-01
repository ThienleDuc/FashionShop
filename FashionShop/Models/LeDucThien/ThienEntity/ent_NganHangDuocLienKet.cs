using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_NganHangDuocLienKet
    {
        public int MaNganHangLienKet { get; set; }   // ID tự tăng
        public string TenNganHang { get; set; }      // Tên ngân hàng
        public string AnhNganHang { get; set; }      // Đường dẫn ảnh ngân hàng

        // Constructor mặc định
        public ent_NganHangDuocLienKet() { }

        // Constructor có tham số
        public ent_NganHangDuocLienKet(int maNganHangLienKet, string tenNganHang, string anhNganHang)
        {
            MaNganHangLienKet = maNganHangLienKet;
            TenNganHang = tenNganHang;
            AnhNganHang = anhNganHang;
        }
    }
}