use dbQuanLyBanHang
go
CREATE FUNCTION dbo.fn_GetMaNganHangLienKet
(
    @TenNganHang NVARCHAR(255)  -- Tham số đầu vào là tên ngân hàng
)
RETURNS INT  -- Trả về kiểu dữ liệu INT (MaNganHangLienKet)
AS
BEGIN
    DECLARE @MaNganHangLienKet INT;

    -- Lấy MaNganHangLienKet từ bảng NganHangDuocLienKet theo tên ngân hàng
    SELECT @MaNganHangLienKet = MaNganHangLienKet
    FROM NganHangDuocLienKet
    WHERE TenNganHang = @TenNganHang;

    -- Trả về giá trị MaNganHangLienKet
    RETURN @MaNganHangLienKet;
END;
GO
SELECT dbo.fn_GetMaNganHangLienKet(N'Ngân Hàng Công Thương');
GO
CREATE FUNCTION GetTenChuSoHuu 
(
    @SoTaiKhoan VARCHAR(14), 
    @MaNganHangLienKet INT
)
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @TenChuSoHuu NVARCHAR(50);
    
    -- Truy vấn dữ liệu từ bảng TaiKhoanNganHangDuocLienKet với cả SoTaiKhoan và MaNganHangLienKet
    SELECT @TenChuSoHuu = TenChuSoHuu
    FROM TaiKhoanNganHangDuocLienKet
    WHERE SoTaiKhoan = @SoTaiKhoan
    AND MaNganHangLienKet = @MaNganHangLienKet;
    
    -- Trả về kết quả
    RETURN @TenChuSoHuu;
END;
GO
SELECT dbo.GetTenChuSoHuu('12345678901234', 1);
GO
CREATE FUNCTION GetTenTinhThanh (@MaTinhThanh INT)
RETURNS NVARCHAR(255)
AS
BEGIN
    DECLARE @TenTinhThanh NVARCHAR(255);

    SELECT @TenTinhThanh = TenTinhThanh
    FROM TinhThanhPho
    WHERE MaTinhThanh = @MaTinhThanh;

    RETURN @TenTinhThanh;
END;
GO
CREATE FUNCTION GetTenQuanHuyen (@MaQuanHuyen INT)
RETURNS NVARCHAR(255)
AS
BEGIN
    DECLARE @TenQuanHuyen NVARCHAR(255);

    SELECT @TenQuanHuyen = TenQuanHuyen
    FROM QuanHuyen
    WHERE MaQuanHuyen = @MaQuanHuyen;

    RETURN @TenQuanHuyen;
END;
GO
CREATE FUNCTION GetTenXaPhuong (@MaXaPhuong INT)
RETURNS NVARCHAR(255)
AS
BEGIN
    DECLARE @TenXaPhuong NVARCHAR(255);

    SELECT @TenXaPhuong = TenXaPhuong
    FROM XaPhuong
    WHERE MaXaPhuong = @MaXaPhuong;

    RETURN @TenXaPhuong;
END;
GO
SELECT 
    dbo.GetTenTinhThanh(1) AS TenTinhThanh,
    dbo.GetTenQuanHuyen(5) AS TenQuanHuyen,
    dbo.GetTenXaPhuong(10) AS TenXaPhuong;
Go
CREATE FUNCTION dbo.fn_GetDieuKien (@maDieuKien INT)
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @DieuKien NVARCHAR(50)

    -- Lấy giá trị DieuKien từ bảng DieuKienGiam theo maDieuKien
    SELECT @DieuKien = DieuKien
    FROM DieuKienGiam
    WHERE maDieuKien = @maDieuKien

    RETURN @DieuKien
END
GO
CREATE FUNCTION dbo.fn_GetTrangThaiGiam (@maTrangThaiGiam INT)
RETURNS NVARCHAR(255)
AS
BEGIN
    DECLARE @trangThaiGiam NVARCHAR(255)

    -- Lấy giá trị trangThaiGiam từ bảng TrangThaiGiam theo maTrangThaiGiam
    SELECT @trangThaiGiam = trangThaiGiam
    FROM TrangThaiGiam
    WHERE maTrangThaiGiam = @maTrangThaiGiam

    RETURN @trangThaiGiam
END
Go
SELECT dbo.fn_GetDieuKien(1) AS DieuKien;
GO
SELECT dbo.fn_GetTrangThaiGiam(1) AS TrangThaiGiam;
GO