using System.Linq;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.ThienProcessData;
using FashionShop.Models.OtherMethods;

namespace FashionShop.Controllers
{
    public class AccountController : Controller
    {
        private pd_KhachHang accountUserProcess = new pd_KhachHang();

        // Trang đăng nhập
        public ActionResult Login()
        {
            return View();
        }

        // Phương thức POST để xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Tên người dùng và mật khẩu không được để trống.";
                return View();
            }

            // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu
            var user = accountUserProcess.GetAccountUsers().FirstOrDefault(u => u.Username == username && u.MatKhau == password);

            if (user != null) // Nếu tìm thấy người dùng hợp lệ
            {
                // Lưu trạng thái đăng nhập vào cookie (nếu cần)
                CookieHelper.SetLoginCookies(username);

                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
            }
            else
            {
                // Nếu không tìm thấy người dùng hoặc mật khẩu sai
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return View(); // Trả về lại trang đăng nhập với thông báo lỗi
            }
        }

        // Phương thức đăng xuất
        public ActionResult Logout()
        {
            // Xóa cookie khi người dùng đăng xuất
            CookieHelper.ClearLoginCookies();

            return RedirectToAction("Index", "Home");
        }

        // Trang đăng ký
        public ActionResult Register()
        {
            return View();
        }

    }
}