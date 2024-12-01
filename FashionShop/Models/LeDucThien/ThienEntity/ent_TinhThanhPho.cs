using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_TinhThanhPho
    {
        public int MaTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }

        // Constructor đầy đủ
        public ent_TinhThanhPho(int maTinhThanh, string tenTinhThanh)
        {
            MaTinhThanh = maTinhThanh;
            TenTinhThanh = tenTinhThanh;
        }

        // Constructor rỗng
        public ent_TinhThanhPho() { }
    }
}