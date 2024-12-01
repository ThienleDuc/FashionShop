namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_TrangThaiGiam
    {
        public int maTrangThaiGiam { get; set; }
        public string trangThaiGiam { get; set; }

        // Constructor đầy đủ
        public ent_TrangThaiGiam(int maTrangThaiGiam, string trangThaiGiam)
        {
            this.maTrangThaiGiam = maTrangThaiGiam;
            this.trangThaiGiam = trangThaiGiam;
        }

        // Constructor rỗng
        public ent_TrangThaiGiam() { }
    }
}
