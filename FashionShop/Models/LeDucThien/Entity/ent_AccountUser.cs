using System;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.LeDucThien.Entity
{
    public class ent_AccountUser
    {
        // Các thuộc tính của người dùng với các validation
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Tên đệm không được để trống.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ngày không được để trống.")]
        [Range(1, 31, ErrorMessage = "Ngày không hợp lệ.")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Tháng không được để trống.")]
        [Range(1, 12, ErrorMessage = "Tháng không hợp lệ.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Năm không được để trống.")]
        [Range(1900, 2100, ErrorMessage = "Năm không hợp lệ.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên.")]
        public string Password { get; set; }

        // Constructor mặc định
        public ent_AccountUser() { }

        // Constructor có tham số
        public ent_AccountUser(string username, string firstName, string lastName, int day, int month, int year, string gender, string password)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Day = day;
            Month = month;
            Year = year;
            Gender = gender;
            Password = password;
        }
    }
}
