namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_VoucherCuaToi
    {
        public string MaVoucherCuaToi { get; set; } // Mã voucher của tôi
        public string MaVoucher { get; set; } // Mã voucher
        public string TenVoucher { get; set; } // Tên voucher
        public string HanSuDung { get; set; } // Hạn sử dụng voucher (định dạng dd/MM/yyyy)
        public int MucGiam { get; set; } // Mức giảm giá
        public string DieuKienGiam { get; set; } // Điều kiện giảm giá
        public string TrangThaiSuDung { get; set; } // Trạng thái sử dụng voucher

        public ent_VoucherCuaToi(string maVoucherCuaToi, string maVoucher, string tenVoucher, string hanSuDung, int mucGiam, string dieuKienGiam, string trangThaiSuDung)
        {
            MaVoucherCuaToi = maVoucherCuaToi;
            MaVoucher = maVoucher;
            TenVoucher = tenVoucher;
            HanSuDung = hanSuDung;
            MucGiam = mucGiam;
            DieuKienGiam = dieuKienGiam;
            TrangThaiSuDung = trangThaiSuDung;
        }

        public ent_VoucherCuaToi()
        {
        }
    }
}
