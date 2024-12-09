namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_DiaChiGiaoHang
    {
        public int MaDiaChi { get; set; }
        public string TenTinhThanh { get; set; }
        public string TenQuanHuyen { get; set; }
        public string TenXaPhuong { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public string DiaChiGiaoHang { get; set; }

        public ent_DiaChiGiaoHang(int maDiaChi, string tenTinhThanh, string tenQuanHuyen, string tenXaPhuong, string tenKhachHang, string sDT, string diaChiGiaoHang)
        {
            MaDiaChi = maDiaChi;
            TenTinhThanh = tenTinhThanh;
            TenQuanHuyen = tenQuanHuyen;
            TenXaPhuong = tenXaPhuong;
            TenKhachHang = tenKhachHang;
            SDT = sDT;
            DiaChiGiaoHang = diaChiGiaoHang;
        }

        public ent_DiaChiGiaoHang()
        {
        }
    }
}
