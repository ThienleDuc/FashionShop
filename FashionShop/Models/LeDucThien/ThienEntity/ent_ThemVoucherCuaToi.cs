namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_ThemVoucherCuaToi
    {
        public string MaVoucher { get; set; } // Mã voucher
        public string MaAccount { get; set; } // Mã tài khoản người dùng
        public string TrangThaiSuDung { get; set; } // Trạng thái sử dụng voucher

        // Constructor để khởi tạo đối tượng
        public ent_ThemVoucherCuaToi(string maVoucher, string maAccount)
        {
            MaVoucher = maVoucher;
            MaAccount = maAccount;
            TrangThaiSuDung = "Chưa sử dụng"; // Mặc định trạng thái là "Chưa sử dụng"
        }

        // Constructor mặc định (không tham số)
        public ent_ThemVoucherCuaToi()
        {
        }
    }
}