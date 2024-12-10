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

        [HttpPost]
        public ActionResult ThayDoiMatKhau(string oldPassword, string newPassword, string confirmPassword)
        {
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            // Khởi tạo lớp xử lý dữ liệu người dùng
            pd_KhachHang khachHangProcess = new pd_KhachHang();

            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var user = khachHangProcess.GetAccountUsers().FirstOrDefault(u => u.Username == username);

            // Kiểm tra mật khẩu cũ có chính xác không
            if (oldPassword != user.MatKhau)
            {
                TempData["ErrorMessage"] = "Mật khẩu cũ không chính xác.";
                return RedirectToAction("MatKhau");
            }

            // Kiểm tra mật khẩu mới không trùng mật khẩu cũ
            if (newPassword == oldPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu mới không thể trùng với mật khẩu cũ.";
                return RedirectToAction("MatKhau");
            }

            // Kiểm tra mật khẩu xác nhận có trùng mật khẩu mới không
            if (confirmPassword != newPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu mới và mật khẩu xác nhận không trùng nhau.";
                return RedirectToAction("MatKhau");
            }

            // Cập nhật mật khẩu mới
            khachHangProcess.CapNhatMatKhau(username, newPassword);

            TempData["SuccessMessage"] = "Mật khẩu đã được cập nhật thành công.";

            // Trả về trang ThayDoiMatKhau
            return RedirectToAction("MatKhau");
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
        public JsonResult GetChiNhanhByNganHang(int maNganHangLienKet)
        {
            try
            {
                // Gọi lớp xử lý dữ liệu chi nhánh ngân hàng
                pd_ChiNhanhNganHang chiNhanhProcess = new pd_ChiNhanhNganHang();
                List<ent_ChiNhanhNganHang> chiNhanhList = chiNhanhProcess.GetTenChiNhanh(maNganHangLienKet);

                // Nếu không có chi nhánh nào, trả về thông báo lỗi
                if (chiNhanhList == null || !chiNhanhList.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy chi nhánh!" }, JsonRequestBehavior.AllowGet);
                }

                // Trả về danh sách chi nhánh
                return Json(new { success = true, data = chiNhanhList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
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
        public JsonResult AddBankAccount(string maNganHangLienKet, string soTaiKhoan, string tenChuSoHuu, string tenChiNhanh)
        {
            try
            {
                // Lấy thông tin username từ Cookie
                HttpCookie usernameCookie = Request.Cookies["Username"];
                var username = usernameCookie != null ? usernameCookie.Value : string.Empty;
                var maNganHang = Convert.ToInt32(maNganHangLienKet);

                // Kiểm tra nếu username không hợp lệ
                if (string.IsNullOrEmpty(username))
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                }

                // Kiểm tra thông tin tài khoản
                if (string.IsNullOrEmpty(soTaiKhoan) || string.IsNullOrEmpty(tenChuSoHuu) || string.IsNullOrEmpty(tenChiNhanh))
                {
                    return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin tài khoản." });
                }

                // Tạo đối tượng để truyền vào phương thức ThemTaiKhoanNganHang
                var taiKhoan = new ent_ThemTaiKhoanNganHang
                {
                    MaAccount = username,
                    MaNganHangLienKet = maNganHang,
                    SoTaiKhoan = soTaiKhoan,
                    TenChuSoHuu = tenChuSoHuu,
                    TenChiNhanh = tenChiNhanh
                };

                // Khởi tạo lớp xử lý dữ liệu
                pd_TaiKhoanNganHangCuaToi taiKhoanProcess = new pd_TaiKhoanNganHangCuaToi();

                // Gọi phương thức thêm tài khoản ngân hàng
                taiKhoanProcess.ThemTaiKhoanNganHang(taiKhoan);

                return Json(new { success = true, message = "Thêm tài khoản ngân hàng thành công!" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return Json(new { success = false, message = "Có lỗi xảy ra khi thêm tài khoản ngân hàng. Lỗi: " + ex.Message });
            }
        }


        [HttpPost]
        public ActionResult XoaTaiKhoanNganHang(int BankID)
        {
            try
            {
                pd_TaiKhoanNganHangCuaToi taiKhoanNganHangCuaToiProcees = new pd_TaiKhoanNganHangCuaToi();
                // Thực hiện xóa tài khoản ngân hàng với ID = BankID
                taiKhoanNganHangCuaToiProcees.DeleteTaiKhoanNganHangCuaToi(BankID);
              
                TempData["SuccessMessage"] = "Xóa tài khoản ngân hàng thành công!";
             
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa tài khoản.";
            }

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

        [HttpPost]
        public ActionResult XoaDiaChi(int MaDiaChi)
        {
            try
            {
                // Khởi tạo lớp xử lý dữ liệu địa chỉ giao hàng
                pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();
                diaChiGiaoHangProcess.XoaDiaChiGiaoHang(MaDiaChi);
                TempData["SuccessMessage"] = "Đã xóa địa chỉ thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa địa chỉ.";
            }

            return RedirectToAction("DiaChi");
        }

        [HttpGet]
        public JsonResult GetDistricts(int maTinhThanh)
        {
            try
            {
                // Gọi lớp xử lý dữ liệu chi nhánh ngân hàng
                pd_QuanHuyen quanHuyenProcess = new pd_QuanHuyen();
                List<ent_QuanHuyen> ent_QuanHuyens = quanHuyenProcess.GetQuanHuyenWhereMaTinhThanh(maTinhThanh);

                // Nếu không có chi nhánh nào, trả về thông báo lỗi
                if (ent_QuanHuyens == null || !ent_QuanHuyens.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy quận hoặc huyện!" }, JsonRequestBehavior.AllowGet);
                }

                // Trả về danh sách chi nhánh
                return Json(new { success = true, data = ent_QuanHuyens }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetProvinces()
        {
            try
            {
                pd_TinhThanh tinhThanhProcess = new pd_TinhThanh();
                List<ent_TinhThanhPho> ent_TinhThanhPhos = tinhThanhProcess.GetAllTinhThanh();

                // Nếu không có chi nhánh nào, trả về thông báo lỗi
                if (ent_TinhThanhPhos == null || !ent_TinhThanhPhos.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy xã hoặc phường!" }, JsonRequestBehavior.AllowGet);
                }

                // Trả về danh sách chi nhánh
                return Json(new { success = true, data = ent_TinhThanhPhos}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetWards(int maQuanHuyen)
        {
            try
            {
                // Gọi lớp xử lý dữ liệu chi nhánh ngân hàng
                pd_XaPhuong xaPhuongProcess = new pd_XaPhuong();
                List<ent_XaPhuong> ent_XaPhuongs = xaPhuongProcess.GetXaPhuongWhereMaQuanHuyen(maQuanHuyen);

                // Nếu không có chi nhánh nào, trả về thông báo lỗi
                if (ent_XaPhuongs == null || !ent_XaPhuongs.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy xã hoặc phường!" }, JsonRequestBehavior.AllowGet);
                }

                // Trả về danh sách chi nhánh
                return Json(new { success = true, data = ent_XaPhuongs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemDiaChi(string maTinhThanh, string maQuanHuyen, string maXaPhuong, string tenKhachHang, string phone, string diaChi)
        {
            try
            {
                // Lấy thông tin username từ Cookie
                HttpCookie usernameCookie = Request.Cookies["Username"];
                var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

                if (string.IsNullOrEmpty(username))
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                }

                // Chuyển đổi các giá trị sang int
                var _maTinhThanh = Convert.ToInt32(maTinhThanh);
                var _maQuanHuyen = Convert.ToInt32(maQuanHuyen);
                var _maXaPhuong = Convert.ToInt32(maXaPhuong);

                // Khởi tạo lớp xử lý dữ liệu địa chỉ giao hàng
                pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();

                // Tạo đối tượng địa chỉ giao hàng từ dữ liệu form
                ent_ThemDiaChiGiaoHang ent_ThemDiaChiGiaoHangs = new ent_ThemDiaChiGiaoHang
                {
                    MaAccount = username,
                    MaTinhThanh = _maTinhThanh,
                    MaQuanHuyen = _maQuanHuyen,
                    MaXaPhuong = _maXaPhuong,
                    TenKhachHang = tenKhachHang,
                    SDT = phone,
                    DiaChiGiaoHang = diaChi
                };

                // Gọi phương thức thêm địa chỉ
                diaChiGiaoHangProcess.ThemDiaChiGiaoHang(ent_ThemDiaChiGiaoHangs);

                return Json(new { success = true, message = "Địa chỉ đã được thêm thành công!" });
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                return Json(new { success = false, message = "Có lỗi xảy ra khi thêm địa chỉ. Lỗi: " + ex.Message });
            }
        }

        [HttpGet]
        public JsonResult LayDuLieuDeCapNhat(int MaDiaChi)
        {
            // Khởi tạo đối tượng xử lý địa chỉ
            pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();

            // Lấy địa chỉ từ danh sách, đảm bảo rằng địa chỉ tồn tại
            var diaChi = diaChiGiaoHangProcess.getAllDiaChiWhereMaDiaChi(MaDiaChi).FirstOrDefault();

            if (diaChi == null)
            {
                return Json(new { error = "Không tìm thấy địa chỉ với mã đã cho!" }, JsonRequestBehavior.AllowGet);
            }

            // Trả về dữ liệu theo định dạng mong muốn
            return Json(new
            {
                MaDiaChi = diaChi.MaDiaChi,
                TenKhachHang = diaChi.TenKhachHang,
                SDT = diaChi.SDT,
                DiaChiGiaoHang = diaChi.DiaChiGiaoHang,
                TinhThanh = diaChi.MaTinhThanh, // Trả về tên tỉnh thành thay vì mã
                QuanHuyen = diaChi.MaQuanHuyen, // Trả về tên quận huyện thay vì mã
                XaPhuong = diaChi.MaXaPhuong   // Trả về tên xã phường thay vì mã
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DiaChi()
        {
            // Lấy thông tin username từ Cookie
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();

            List<ent_DiaChiGiaoHang> diaChiGiaoHangs = diaChiGiaoHangProcess.GetDiaChiGiaoHangByAccount(username);
            ViewBag.diachiList = diaChiGiaoHangs;

            pd_TinhThanh tinhThanhProcess = new pd_TinhThanh();
            List<ent_TinhThanhPho> ent_TinhThanhPhos = tinhThanhProcess.GetAllTinhThanh();
            ViewBag.list_TinhThanh = ent_TinhThanhPhos;

            // Trả về view
            return View();
        }

        public ActionResult MatKhau()
        {
            return View();
        }

    }
}