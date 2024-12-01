using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_QuanHuyen
    {
        public int MaQuanHuyen { get; set; }
        public int MaTinhThanh { get; set; }
        public string TenQuanHuyen { get; set; }

        // Constructor đầy đủ
        public ent_QuanHuyen(int maQuanHuyen, int maTinhThanh, string tenQuanHuyen)
        {
            MaQuanHuyen = maQuanHuyen;
            MaTinhThanh = maTinhThanh;
            TenQuanHuyen = tenQuanHuyen;
        }

        // Constructor rỗng
        public ent_QuanHuyen() { }
    }
}