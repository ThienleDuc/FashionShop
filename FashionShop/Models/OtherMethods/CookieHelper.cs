using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Models.OtherMethods
{
    public class CookieHelper
    {
        // Phương thức để lưu trạng thái đăng nhập vào cookie
        public static void SetLoginCookies(string username)
        {
            HttpCookie loginCookie = new HttpCookie("IsLoggedIn", "true");
            loginCookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(loginCookie);

            HttpCookie usernameCookie = new HttpCookie("Username", username);
            usernameCookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(usernameCookie);
        }

        // Phương thức để xóa cookie khi người dùng đăng xuất
        public static void ClearLoginCookies()
        {
            HttpCookie loginCookie = new HttpCookie("IsLoggedIn")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            HttpContext.Current.Response.Cookies.Add(loginCookie);

            HttpCookie usernameCookie = new HttpCookie("Username")
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            HttpContext.Current.Response.Cookies.Add(usernameCookie);
        }

        // Phương thức để lưu thông tin đăng ký vào cookie (có thể thêm các logic bảo mật khác nếu cần)
        public static void SetRegisterCookies(string username, string password)
        {
            HttpCookie usernameCookie = new HttpCookie("Username", username)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            HttpContext.Current.Response.Cookies.Add(usernameCookie);

            HttpCookie passwordCookie = new HttpCookie("Password", password) // Không nên lưu mật khẩu như vậy trong thực tế
            {
                Expires = DateTime.Now.AddYears(1)
            };
            HttpContext.Current.Response.Cookies.Add(passwordCookie);
        }
    }
}