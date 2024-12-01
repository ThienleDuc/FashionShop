using System;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_Voucher
    {
        public string maVoucher { get; set; }
        public string tenVoucher { get; set; }  // Thêm thuộc tính tenVoucher
        public DateTime hanSuDung { get; set; }
        public int mucGiam { get; set; }
        public int MaDieuKien { get; set; }
        public int maTrangThaiGiam { get; set; }

        // Constructor đầy đủ
        public ent_Voucher(string maVoucher, string tenVoucher, DateTime hanSuDung, int mucGiam, int MaDieuKien, int maTrangThaiGiam)
        {
            this.maVoucher = maVoucher;
            this.tenVoucher = tenVoucher;  // Thêm tham số cho tenVoucher
            this.hanSuDung = hanSuDung;
            this.mucGiam = mucGiam;
            this.MaDieuKien = MaDieuKien;
            this.maTrangThaiGiam = maTrangThaiGiam;
        }

        // Constructor rỗng
        public ent_Voucher() { }
    }
}
