using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_ChiTietDonHang
    {
        public string TenSanPham { get; set; }    // Tên sản phẩm
        public string AnhSanPham { get; set; }    // Hình ảnh sản phẩm
        public string PhanLoai { get; set; }      // Phân loại (màu sắc + kích thước)
        public int SoLuong { get; set; }          // Số lượng mua
        public decimal GiaTien { get; set; }      // Giá tiền

        public ent_ChiTietDonHang(string tenSanPham, string anhSanPham, string phanLoai, int soLuong, decimal giaTien)
        {
            TenSanPham = tenSanPham;
            AnhSanPham = anhSanPham;
            PhanLoai = phanLoai;
            SoLuong = soLuong;
            GiaTien = giaTien;
        }

        public ent_ChiTietDonHang()
        {
        }
    }
}