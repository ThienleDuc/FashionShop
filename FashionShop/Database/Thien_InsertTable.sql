use dbQuanLyBanHang
go

-- Thêm một dòng dữ liệu mới vào bảng KhachHang
INSERT INTO KhachHang (username, matKhau, firstName, lastName, day, moth, year, gender, anh)
VALUES ('LeDucThien', '12345', N'Lê Đức', N'Thiện', 02, 09, 2004, N'Nam', 'avatar-anime-1.jpg');

INSERT INTO NganHangDuocLienKet (TenNganHang, anhnganhang)
VALUES 
(N'Ngân Hàng Công Thương', 'logo-VietinBank.png');

GO
INSERT INTO ChiNhanhNganHang (maNganHangLienKet, TenChiNhanh)
VALUES 
(1, N'Đà Nẵng');

GO
INSERT INTO TaiKhoanNganHangDuocLienKet (SoTaiKhoan, MaNganHangLienKet, TenChuSoHuu)
VALUES 
('12345678901234', 1, 'LE DUC THIEN');

GO
INSERT INTO TaiKhoanNganHangCuaToi (maAccount, maNganHangLienKet, SoTaiKhoan, TenChuSoHuu, TenChiNhanh)
VALUES ('LeDucThien', 1, '12345678901234', 'LE DUC THIEN', N'Đà Nẵng');


