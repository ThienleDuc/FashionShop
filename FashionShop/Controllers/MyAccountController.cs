using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.ThienProcessData;
using FashionShop.Models.LeDucThien.ThienEntity;


namespace FashionShop.Controllers
{
    public class MyAccountController : Controller
    {

        [HttpPost]
        public ActionResult CapNhatHoSo(string username, string firstname, string lastname, int day, int month, int year, string gender)
        {
            try
            {
                // Khởi tạo lớp xử lý
                pd_KhachHang khachHangProcess = new pd_KhachHang();

                // Gọi phương thức cập nhật mà không truyền mật khẩu và ảnh
                khachHangProcess.CapNhatKhachHang(username, firstname, lastname, day, month, year, gender);

                // Thêm thông báo thành công
                TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và thông báo lỗi
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }

            // Trả về lại trang Hồ Sơ
            return RedirectToAction("HoSo");
        }

        [HttpPost]
        public ActionResult CapNhatAnhHoSo(HttpPostedFileBase avatar)
        {
            try
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    // Xác định đường dẫn lưu ảnh
                    var fileName = Path.GetFileName(avatar.FileName);
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);

                    // Lưu ảnh vào thư mục "img"
                    avatar.SaveAs(path);

                    // Lấy tên ảnh đã lưu và cập nhật trong cơ sở dữ liệu
                    string username = User.Identity.Name;  // Lấy username của người dùng đang đăng nhập
                    pd_KhachHang khachHangProcess = new pd_KhachHang();
                    khachHangProcess.CapNhatAnhKhachHang(username, fileName); // Cập nhật ảnh trong cơ sở dữ liệu

                    TempData["SuccessMessage"] = "Cập nhật ảnh thành công!";
                    TempData["ShowModal"] = true;  // Thêm thông tin để hiển thị modal

                }
                else
                {
                    TempData["ErrorMessage"] = "Vui lòng chọn ảnh!";
                    TempData["ShowModal"] = false; // Không hiển thị modal khi có lỗi
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật ảnh: " + ex.Message;
            }

            // Quay lại trang Hồ Sơ
            return RedirectToAction("HoSo");
        }


        // GET: MyAccount
        public ActionResult HoSo()
        {
            // Giả sử bạn lấy thông tin từ Cookie hoặc Database
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            pd_KhachHang khachHangProcess = new pd_KhachHang();

            // Truy vấn thông tin người dùng từ cơ sở dữ liệu theo tên đăng nhập
            var user = khachHangProcess.GetKhachHangWhere(username).FirstOrDefault();

            // Lưu trữ các giá trị vào ViewBag để gửi sang View
            ViewBag.Username = user.Username;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Gender = user.Gender;
            ViewBag.Day = user.Day;
            ViewBag.Month = user.Moth;
            ViewBag.Year = user.Year;
            ViewBag.Avatar = user.Anh;

            return View();
        }

        [HttpGet]
        public JsonResult LayMaChiNhanh(int maNganHangLienKet)
        {
            try
            {
                pd_ChiNhanhNganHang chiNhanhProcess = new pd_ChiNhanhNganHang();
                List<ent_ChiNhanhNganHang> ent_ChiNhanhNganHangs = chiNhanhProcess.GetChiNhanhNganHangWhereMaNganHangLienKet(maNganHangLienKet);

                // Kiểm tra kết quả trả về
                if (ent_ChiNhanhNganHangs == null || !ent_ChiNhanhNganHangs.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy chi nhánh!" }, JsonRequestBehavior.AllowGet);
                }

                return Json(ent_ChiNhanhNganHangs, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetTenChuSoHuuBySoTaiKhoan(string soTaiKhoan, int maNganHangLienKet)
        {
            try
            {
                pd_TaiKhoanNganHangDuocLienKet taiKhoanProcess = new pd_TaiKhoanNganHangDuocLienKet();
                string tenChuSoHuu = taiKhoanProcess.GetTenChuSoHuuBySoTaiKhoan(soTaiKhoan, maNganHangLienKet);

                if (string.IsNullOrEmpty(tenChuSoHuu))
                {
                    return Json(new { success = false, message = "Không tồn tại tài khoản này" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, tenChuSoHuu = tenChuSoHuu }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteTaiKhoanNganHang(int maTaiKhoan)
        {
            // Logic xóa tài khoản ngân hàng theo maTaiKhoan
            pd_TaiKhoanNganHangCuaToi taiKhoanNganHangCuaToiProcess = new pd_TaiKhoanNganHangCuaToi();
            taiKhoanNganHangCuaToiProcess.DeleteTaiKhoanNganHangCuaToi(maTaiKhoan);

            // Sau khi xóa thành công, chuyển hướng lại trang danh sách tài khoản ngân hàng
            return RedirectToAction("NganHang");
        }


        public ActionResult NganHang()
        {
            // Lấy thông tin username từ Cookie
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            // Khởi tạo lớp xử lý dữ liệu ngân hàng
            pd_TaiKhoanNganHangCuaToi taiKhoanNganHangCuaToiProcees = new pd_TaiKhoanNganHangCuaToi();
            List<ent_TaiKhoanNganHangCuaToi> ent_TaiKhoanNganHangCuaTois = taiKhoanNganHangCuaToiProcees.GetTaiKhoanNganHangCuaToiWhere(username);
            ViewBag.taikhoanList = ent_TaiKhoanNganHangCuaTois;

            pd_NganHangDuocLienKet nganHangDuocLienKetProcess = new pd_NganHangDuocLienKet();
            List<ent_NganHangDuocLienKet> ent_NganHangDuocLienKets = nganHangDuocLienKetProcess.GetNganHangDuocLienKet();
            ViewBag.nganhanglienket = ent_NganHangDuocLienKets;

            return View();
        }

        public ActionResult DiaChi()
        {
            // Lấy thông tin username từ Cookie
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            // Khởi tạo lớp xử lý dữ liệu địa chỉ giao hàng
            pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();

            // Lấy danh sách địa chỉ giao hàng của người dùng từ database
            List<ent_DiaChiGiaoHang> diaChiGiaoHangs = diaChiGiaoHangProcess.GetDiaChiGiaoHangByAccount(username);

            // Truyền danh sách địa chỉ giao hàng vào ViewBag
            ViewBag.diachiList = diaChiGiaoHangs;

            // Trả về view
            return View();
        }

        public ActionResult MatKhau()
        {
            return View();
        }
    }
}