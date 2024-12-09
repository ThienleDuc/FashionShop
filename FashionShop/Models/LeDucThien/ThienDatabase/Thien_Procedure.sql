use dbQuanLyBanHang
go
CREATE PROCEDURE pr_CapNhatKhachHang
    @username VARCHAR(255),
    @firstName NVARCHAR(255),
    @lastName NVARCHAR(255),
    @day INT,
    @moth INT,
    @year INT,
    @gender NVARCHAR(50)
AS
BEGIN
    -- Kiểm tra tồn tại của khách hàng
    IF EXISTS (SELECT 1 FROM KhachHang WHERE username = @username)
    BEGIN
        -- Cập nhật thông tin khách hàng (không bao gồm mật khẩu)
        UPDATE KhachHang
        SET 
            firstName = @firstName,
            lastName = @lastName,
            day = @day,
            moth = @moth,
            year = @year,
            gender = @gender
	WHERE 
            username = @username;
        
        PRINT 'Cập nhật thông tin khách hàng thành công.';
    END
    ELSE
    BEGIN
        PRINT 'Khách hàng không tồn tại.';
    END
END;
GO
CREATE PROCEDURE pr_CapNhatAnhKhachHang
    @username VARCHAR(255),
    @anh VARCHAR(MAX)
AS
BEGIN
    -- Kiểm tra nếu khách hàng tồn tại
    IF EXISTS (SELECT 1 FROM KhachHang WHERE username = @username)
    BEGIN
        -- Cập nhật ảnh khách hàng
        UPDATE KhachHang
        SET anh = @anh
        WHERE username = @username;

        PRINT 'Cập nhật ảnh khách hàng thành công.';
    END
    ELSE
    BEGIN
        PRINT 'Khách hàng không tồn tại.';
    END
END;
GO
CREATE PROCEDURE pr_TaiKhoanNganHangCuaToi
    @maAccount VARCHAR(255)
AS
BEGIN
    -- Truy vấn dữ liệu từ các bảng liên kết để lấy thông tin tài khoản ngân hàng của người dùng
    SELECT 
		tkn.MaTaiKhoan AS BankID,
        nh.TenNganHang AS BankName,
        nh.anhnganhang AS BankLogo,
        tkn.TenChuSoHuu AS AccountOwner,
        tkn.TenChiNhanh AS BranchName,
        CONCAT('*', RIGHT(tkn.SoTaiKhoan, 4)) AS AccountNumber
    FROM 
        TaiKhoanNganHangCuaToi tkn
    JOIN 
        NganHangDuocLienKet nh ON tkn.maNganHangLienKet = nh.MaNganHangLienKet
    WHERE 
        tkn.maAccount = @maAccount
END
GO
CREATE PROCEDURE GetDiaChiGiaoHangByAccount
    @maAccount VARCHAR(255)
AS
BEGIN
    SELECT 
        dc.MaDiaChi,
        tt.TenTinhThanh,
        qh.TenQuanHuyen,
        xp.TenXaPhuong,
        dc.TenKhachHang,
        dc.SDT,
        dc.DiaChiGiaoHang
    FROM 
        DiaChiGiaoHang dc
    INNER JOIN 
        TinhThanhPho tt ON dc.MaTinhThanh = tt.MaTinhThanh
    INNER JOIN 
        QuanHuyen qh ON dc.MaQuanHuyen = qh.MaQuanHuyen
    INNER JOIN 
        XaPhuong xp ON dc.MaXaPhuong = xp.MaXaPhuong
    WHERE 
        dc.maAccount = @maAccount;
END;
GO
CREATE PROCEDURE pr_VoucherCuaToi
    @maAccount VARCHAR(255)
AS
BEGIN
    SELECT 
        vt.maVoucherCuaToi,
        v.maVoucher,
        v.tenVoucher, 
        CONVERT(VARCHAR, v.hanSuDung, 103) AS hanSuDung,  -- Định dạng dd/MM/yyyy
        v.mucGiam, 
        dk.DieuKien AS DieuKienGiam,
        vt.trangThaiSuDung
    FROM 
        Voucher v
    INNER JOIN 
        VoucherCuaToi vt ON v.maVoucher = vt.maVoucher 
    LEFT JOIN 
        DieuKienGiam dk ON v.MaDieuKien = dk.maDieuKien
    WHERE 
        vt.maAccount = @maAccount 
        AND vt.trangThaiSuDung = N'Chưa sử dụng'  
END;
GO
CREATE PROCEDURE pr_ThemMaVoucherCuaToi
    @maVoucher VARCHAR(10),
    @maAccount VARCHAR(255)
AS
BEGIN
    -- Thêm một bản ghi vào bảng VoucherCuaToi
    INSERT INTO VoucherCuaToi (maVoucher, maAccount, trangThaiSuDung)
    VALUES (@maVoucher, @maAccount, N'Chưa sử dụng');
   
END;
