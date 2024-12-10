use dbQuanLyBanHang
go

-- Thêm một dòng dữ liệu mới vào bảng KhachHang
INSERT INTO KhachHang (username, matKhau, firstName, lastName, day, moth, year, gender, anh)
VALUES ('LeDucThien', '12345', N'Lê Đức', N'Thiện', 02, 09, 2004, N'Nam', '~/img/avatar-anime-1.jpg');

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
INSERT INTO DiaChiGiaoHang (maAccount, MaTinhThanh, MaQuanHuyen, MaXaPhuong, TenKhachHang, SDT, DiaChi)
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

INSERT INTO PhanLoai (tenPhanLoai) VALUES
(N'Trang phục Nam'),
(N'Trang phục Nữ'),
(N'Trang phục Trẻ Em'),
(N'Áo sơ mi'),
(N'Quần jean'),
(N'Đồ bơi'),
(N'Đồ ngủ'),
(N'Đồ thể thao'),
(N'Áo liền quần'),
(N'Áo blazer'),
(N'Áo khoác'),
(N'Giầy');

INSERT INTO SanPham (id, maphanLoai, anh, tenSanPham, price, moTa, SoLuongHienCo, soLuongBanRa) VALUES
('SP001', 1, 'dongphucnam1.jpg', N'Áo Sơ Mi Nam Trắng', 300000.00, N'Áo sơ mi nam màu trắng, chất liệu cotton', 20, 0),
('SP002', 1, 'dongphucnam2.jpg', N'Quần Tây Nam', 500000.00, N'Quần tây nam cao cấp, phù hợp công sở', 15, 0),
('SP003', 1, 'dongphucnam3.jpg', N'Áo Polo Nam', 350000.00, N'Áo polo nam thể thao, thoáng mát', 25, 0),
('SP004', 2, 'dongphucnu1.jpg', N'Áo Sơ Mi Nữ', 280000.00, N'Áo sơ mi nữ thời trang, chất liệu lụa', 18, 0),
('SP005', 2, 'dongphucnu2.jpg', N'Chân Váy Công Sở', 450000.00, N'Chân váy công sở thanh lịch', 12, 0),
('SP006', 2, 'dongphucnu3.jpg', N'Áo Khoác Cardigan', 600000.00, N'Áo khoác cardigan nữ mềm mại', 10, 0),
('SP007', 3, 'treem1.jpg', N'Áo Thun Trẻ Em', 150000.00, N'Áo thun trẻ em nhiều màu sắc', 30, 0),
('SP008', 3, 'treem2.jpg', N'Quần Short Trẻ Em', 180000.00, N'Quần short thoải mái cho trẻ em', 25, 0),
('SP009', 3, 'treem3.jpg', N'Bộ Đồ Thể Thao Trẻ Em', 220000.00, N'Bộ đồ thể thao trẻ em năng động', 20, 0);

INSERT INTO KichThuoc (maSanPham, kichthuoc) VALUES
('SP001', 'M'),
('SP002', 'L'),
('SP003', 'M'),
('SP004', 'S'),
('SP005', 'M'),
('SP006', 'L'),
('SP007', 'XXL'),
('SP008', 'S'),
('SP009', 'M');

INSERT INTO MauSac (maSanPham, mauSac) VALUES
('SP001', 'Trắng'),
('SP002', 'Đen'),
('SP003', 'Xanh dương'),
('SP004', 'Hồng'),
('SP005', 'Đen'),
('SP006', 'Be'),
('SP007', 'Đỏ'),
('SP008', 'Xanh lá cây'),
('SP009', 'Đen');

INSERT INTO AnhSanPham (maSanPham, anh) VALUES
('SP001', 'dongphucnam1.jpg'),
('SP002', 'dongphucnam2.jpg'),
('SP003', 'dongphucnam3.jpg'),
('SP004', 'dongphucnu1.jpg'),
('SP005', 'dongphucnu2.jpg'),
('SP006', 'dongphucnu3.jpg'),
('SP007', 'treem1.jpg'),
('SP008', 'treem2.jpg'),
('SP009', 'treem3.jpg');

INSERT INTO DonHang (maDonHang, maAccount) VALUES
('DH001', 'LeDucThien'),
('DH002', 'LeDucThien'),
('DH003', 'LeDucThien'),
('DH004', 'LeDucThien'),
('DH005', 'LeDucThien'),
('DH006', 'LeDucThien'),
('DH007', 'LeDucThien'),
('DH008', 'LeDucThien'),
('DH009', 'LeDucThien'),
('DH010', 'LeDucThien');

INSERT INTO ChiTietDonHang (maDonHang, maSanPham, soLuongMua)
VALUES 
('DH002', 'SP001', 2),
('DH001', 'SP002', 1),
('DH002', 'SP003', 3),
('DH002', 'SP004', 4),
('DH003', 'SP005', 1);

INSERT INTO ThanhToan (maDonHang, phuongThucThanhToan, luuYKhiNhanHang, TongTien) VALUES
('DH001', N'Thẻ tín dụng', N'Giao hàng khi có đủ số lượng', 1500000),
('DH002', N'Chuyển khoản', N'Kiểm tra hàng trước khi thanh toán', 2500000),
('DH003', N'Tiền mặt', N'Giao hàng trong vòng 24h', 1200000),
('DH004', N'Thẻ ghi nợ', N'Chuyển tiền khi nhận hàng', 1000000),
('DH005', N'Chuyển khoản', N'Giao hàng trong 48h', 1500000),
('DH006', N'Tiền mặt', N'Giao hàng sau 3 ngày', 1800000),
('DH007', N'Thẻ tín dụng', N'Giao hàng miễn phí', 2100000),
('DH008', N'Thẻ ghi nợ', N'Kiểm tra mã khuyến mãi', 2200000),
('DH009', N'Tiền mặt', N'Cảm ơn đã mua hàng', 1600000),
('DH010', N'Chuyển khoản', N'Giao hàng tận nơi', 2000000);

