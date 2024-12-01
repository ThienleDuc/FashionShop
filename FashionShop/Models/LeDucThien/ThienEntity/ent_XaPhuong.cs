using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_XaPhuong
    {
        public int MaXaPhuong { get; set; }
        public int MaQuanHuyen { get; set; }
        public string TenXaPhuong { get; set; }

        // Constructor đầy đủ
        public ent_XaPhuong(int maXaPhuong, int maQuanHuyen, string tenXaPhuong)
        {
            MaXaPhuong = maXaPhuong;
            MaQuanHuyen = maQuanHuyen;
            TenXaPhuong = tenXaPhuong;
        }

        // Constructor rỗng
        public ent_XaPhuong() { }
    }
}