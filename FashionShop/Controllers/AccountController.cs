using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.Entity;

namespace FashionShop.Controllers
{
    public class AccountController : Controller
    {

        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            // Kiểm tra thông tin đăng nhập (ví dụ với tài khoản admin)
            if (username == "admin" && password == "admin12345") // Đăng nhập thành công
            {
                // Lưu trạng thái đăng nhập vào cookie
                HttpCookie loginCookie = new HttpCookie("IsLoggedIn", "true");
                loginCookie.Expires = DateTime.Now.AddYears(1); // Thiết lập cookie hết hạn sau 1 năm
                Response.Cookies.Add(loginCookie);

                // Lưu tên người dùng vào cookie
                HttpCookie usernameCookie = new HttpCookie("Username", username);
                usernameCookie.Expires = DateTime.Now.AddYears(1); // Thiết lập cookie hết hạn sau 1 năm
                Response.Cookies.Add(usernameCookie);

                // Trả về kết quả thành công
                return Json(new { success = true });
            }
            else
            {
                // Nếu đăng nhập không hợp lệ
                return Json(new { success = false });
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Xóa cookie khi người dùng đăng xuất
            HttpCookie loginCookie = new HttpCookie("IsLoggedIn");
            loginCookie.Expires = DateTime.Now.AddDays(-1); // Đặt thời gian hết hạn của cookie về quá khứ
            Response.Cookies.Add(loginCookie);

            HttpCookie usernameCookie = new HttpCookie("Username");
            usernameCookie.Expires = DateTime.Now.AddDays(-1); // Xóa cookie tên người dùng
            Response.Cookies.Add(usernameCookie);

            return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ
        }

        [HttpPost]
        public JsonResult Register(string username, string password)
        {
            // Kiểm tra thông tin đăng ký (ví dụ kiểm tra với tài khoản admin, bạn có thể mở rộng logic này)
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) // Đảm bảo username và password không rỗng
            {
                // Lưu thông tin đăng ký vào cookie (có thể mã hóa mật khẩu nếu cần thiết)
                HttpCookie usernameCookie = new HttpCookie("Username", username);
                usernameCookie.Expires = DateTime.Now.AddYears(1); // Thiết lập cookie hết hạn sau 1 năm
                Response.Cookies.Add(usernameCookie);

                HttpCookie passwordCookie = new HttpCookie("Password", password); // Lưu password (không an toàn trong thực tế)
                passwordCookie.Expires = DateTime.Now.AddYears(1); // Thiết lập cookie hết hạn sau 1 năm
                Response.Cookies.Add(passwordCookie);

                // Trả về kết quả thành công
                return Json(new { success = true });
            }
            else
            {
                // Nếu thông tin không hợp lệ
                return Json(new { success = false });
            }
        }

        public ActionResult Register()
        {
            return View();
        }

    }
}