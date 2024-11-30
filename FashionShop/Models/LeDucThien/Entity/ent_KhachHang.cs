using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.LeDucThien.Entity
{
    public class ent_KhachHang
    {
        // Các thuộc tính tương ứng với các cột trong bảng KhachHang
        public string Username { get; set; }
        public string MatKhau { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Day { get; set; }
        public int Moth { get; set; } // Lỗi chính tả trong bảng (moth -> month)
        public int Year { get; set; }
        public string Gender { get; set; }
        public string Anh { get; set; } // Đường dẫn tới ảnh

        // Constructor mặc định
        public ent_KhachHang() { }

        // Constructor có tham số
        public ent_KhachHang(string username, string matKhau, string firstName, string lastName, int day, int moth, int year, string gender, string anh)
        {
            Username = username;
            MatKhau = matKhau;
            FirstName = firstName;
            LastName = lastName;
            Day = day;
            Moth = moth; // Chú ý sửa chính tả của "Month"
            Year = year;
            Gender = gender;
            Anh = anh;
        }
    }
}