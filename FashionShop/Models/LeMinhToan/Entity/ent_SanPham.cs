using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeMinhToan.Entity
{
    public class ent_SanPham
    {
        // Các thuộc tính
        public string IdSanPham { get; set; }
        public int MaPhanLoai { get; set; }
        public string Anh { get; set; }
        public string TenSanPham { get; set; }
        public float Price { get; set; }
        public string MoTa { get; set; }
        public int SoLuongHienCo { get; set; }
        public int SoLuongBanRa { get; set; }

        // Constructor rỗng
        public ent_SanPham() { }

        // Constructor có tham số
        public ent_SanPham(string idSanPham, int maPhanLoai, string anh, string tenSanPham, float price, string moTa, int soLuongHienCo, int soLuongBanRa)
        {
            IdSanPham = idSanPham;
            MaPhanLoai = maPhanLoai;
            Anh = anh;
            TenSanPham = tenSanPham;
            Price = price;
            MoTa = moTa;
            SoLuongHienCo = soLuongHienCo;
            SoLuongBanRa = soLuongBanRa;
        }
    }
}