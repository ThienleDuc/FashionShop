using System;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_VoucherCuaToi
    {
        public string maVoucherCuaToi { get; set; }
        public string maVoucher { get; set; }
        public string maAccount { get; set; }
        public string trangThaiSuDung { get; set; }

        // Constructor đầy đủ
        public ent_VoucherCuaToi(string maVoucherCuaToi, string maVoucher, string maAccount, string trangThaiSuDung)
        {
            this.maVoucherCuaToi = maVoucherCuaToi;
            this.maVoucher = maVoucher;
            this.maAccount = maAccount;
            this.trangThaiSuDung = trangThaiSuDung;
        }

        // Constructor rỗng
        public ent_VoucherCuaToi() { }
    }
}
