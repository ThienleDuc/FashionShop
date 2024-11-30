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
CREATE FUNCTION GetTenChuSoHuu (@SoTaiKhoan VARCHAR(14))
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @TenChuSoHuu NVARCHAR(50);
    
    -- Truy vấn dữ liệu từ bảng TaiKhoanNganHangDuocLienKet
    SELECT @TenChuSoHuu = TenChuSoHuu
    FROM TaiKhoanNganHangDuocLienKet
    WHERE SoTaiKhoan = @SoTaiKhoan;
    
    -- Trả về kết quả
    RETURN @TenChuSoHuu;
END;
GO
SELECT dbo.GetTenChuSoHuu('12345678901234');
GO