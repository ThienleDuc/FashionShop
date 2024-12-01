namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_DiaChiGiaoHang
    {
        public int MaDiaChi { get; set; }
        public string MaAccount { get; set; }
        public int MaTinhThanh { get; set; }
        public int MaQuanHuyen { get; set; }
        public int MaXaPhuong { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public string DiaChiGiaoHang { get; set; }

        // Constructor đầy đủ
        public ent_DiaChiGiaoHang(int maDiaChi, string maAccount, int maTinhThanh, int maQuanHuyen, int maXaPhuong, string tenKhachHang, string sdt, string diaChiGiaoHang)
        {
            MaDiaChi = maDiaChi;
            MaAccount = maAccount;
            MaTinhThanh = maTinhThanh;
            MaQuanHuyen = maQuanHuyen;
            MaXaPhuong = maXaPhuong;
            TenKhachHang = tenKhachHang;
            SDT = sdt;
            DiaChiGiaoHang = diaChiGiaoHang;
        }

        // Constructor rỗng
        public ent_DiaChiGiaoHang() { }
    }
}
