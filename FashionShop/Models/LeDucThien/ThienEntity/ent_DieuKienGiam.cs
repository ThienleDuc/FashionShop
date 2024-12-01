namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_DieuKienGiam
    {
        public int maDieuKien { get; set; }
        public string DieuKien { get; set; }

        // Constructor đầy đủ
        public ent_DieuKienGiam(int maDieuKien, string DieuKien)
        {
            this.maDieuKien = maDieuKien;
            this.DieuKien = DieuKien;
        }

        // Constructor rỗng
        public ent_DieuKienGiam() { }
    }
}
