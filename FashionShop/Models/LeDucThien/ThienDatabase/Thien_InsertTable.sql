﻿use dbQuanLyBanHang
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

GO
-- Insert vào bảng TinhThanhPho (Tỉnh thành)
INSERT INTO TinhThanhPho (TenTinhThanh) 
VALUES 
(N'Hà Nội'), 
(N'TP Hồ Chí Minh'), 
(N'Đà Nẵng');
GO
-- Insert vào bảng QuanHuyen (Quận huyện) liên quan đến các tỉnh thành trên
INSERT INTO QuanHuyen (MaTinhThanh, TenQuanHuyen) 
VALUES 
(1, N'Quận Hoàn Kiếm'), 
(1, N'Quận Ba Đình'), 
(2, N'Quận 1'), 
(2, N'Quận 3'), 
(3, N'Quận Hải Châu'), 
(3, N'Quận Thanh Khê');
GO
-- Insert vào bảng XaPhuong (Xã phường) liên quan đến các quận huyện trên
INSERT INTO XaPhuong (MaQuanHuyen, TenXaPhuong)
VALUES 
(1, N'Phường Tràng Tiền'), 
(1, N'Phường Hàng Bài'), 
(2, N'Phường Phúc Xá'), 
(2, N'Phường Ba Đình'), 
(3, N'Phường Bến Nghé'), 
(3, N'Phường Tân Định'), 
(4, N'Phường Mỹ An'), 
(4, N'Phường Hòa Cường Bắc');
GO
-- Insert vào bảng DiaChiGiaoHang (Địa chỉ giao hàng)
INSERT INTO DiaChiGiaoHang (maAccount, MaTinhThanh, MaQuanHuyen, MaXaPhuong, TenKhachHang, SDT, DiaChiGiaoHang)
VALUES 
('LeDucThien', 1, 1, 1, N'Lê Đức Thiện', '0901234567', N'Số 1, Tràng Tiền, Hoàn Kiếm, Hà Nội'),
('LeDucThien', 2, 3, 5, N'Lê Đức Thiện', '0902345678', N'Số 123, Nguyễn Thị Minh Khai, Quận 1, TP Hồ Chí Minh'),
('LeDucThien', 3, 4, 7, N'Lê Đức Thiện', '0903456789', N'Số 10, Hòa Cường Bắc, Hải Châu, Đà Nẵng');

GO
-- Thêm dữ liệu vào bảng DieuKienGiam
INSERT INTO DieuKienGiam (DieuKien, MucDieuKienGiam)
VALUES 
(N'Đơn hàng tối thiểu 100K', 100000),
(N'Đơn hàng tối thiểu 200K', 200000),
(N'Đơn hàng tối thiểu 500K', 500000);
GO
-- Thêm dữ liệu vào bảng TrangThaiGiam
INSERT INTO TrangThaiGiam (trangThaiGiam)
VALUES 
(N'Phần trăm'),
(N'Giá tiền');
GO
-- Thêm dữ liệu vào bảng Voucher
INSERT INTO Voucher (maVoucher, tenVoucher, hanSuDung, mucGiam, MaDieuKien, maTrangThaiGiam)
VALUES 
('VC001', N'Giảm 10%', '2024-12-31', 10, 1, 1),
('VC002', N'Giảm 20%', '2024-12-15', 20, 2, 1),
('VC003', N'Giảm 50K', '2024-11-30', 50000, 3, 2);
GO
-- Thêm dữ liệu vào bảng VoucherCuaToi chỉ với maVoucher và maAccount
INSERT INTO VoucherCuaToi (maVoucher, maAccount)
VALUES 
('VC001', 'LeDucThien'),
('VC002', 'LeDucThien'),
('VC003', 'LeDucThien');