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
        dc.DiaChi
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
GO
CREATE PROCEDURE pr_UpdateMatKhau
    @username VARCHAR(255),
    @newMatKhau VARCHAR(255)
AS
BEGIN
    -- Cập nhật mật khẩu cho khách hàng theo username
    UPDATE KhachHang
    SET matKhau = @newMatKhau
    WHERE username = @username;

    -- Trả về thông báo nếu không tìm thấy username
    IF @@ROWCOUNT = 0
    BEGIN
        PRINT 'Không tìm thấy khách hàng với username ' + @username;
    END
    ELSE
    BEGIN
        PRINT 'Mật khẩu đã được cập nhật thành công.';
    END
END;
GO
CREATE PROCEDURE pr_ThemTaiKhoanNganHangCuaToi
    @maAccount VARCHAR(255),
    @maNganHangLienKet INT,
    @SoTaiKhoan VARCHAR(14),
    @TenChuSoHuu NVARCHAR(50),
    @TenChiNhanh NVARCHAR(255)
AS
BEGIN
    -- Kiểm tra nếu tài khoản ngân hàng đã tồn tại
    IF EXISTS (SELECT 1 FROM TaiKhoanNganHangCuaToi WHERE maAccount = @maAccount AND SoTaiKhoan = @SoTaiKhoan)
    BEGIN
        RAISERROR('Tài khoản ngân hàng đã tồn tại!', 16, 1);
        RETURN;
    END

    -- Thêm tài khoản ngân hàng mới vào bảng TaiKhoanNganHangCuaToi
    INSERT INTO TaiKhoanNganHangCuaToi (maAccount, maNganHangLienKet, SoTaiKhoan, TenChuSoHuu, TenChiNhanh)
    VALUES (@maAccount, @maNganHangLienKet, @SoTaiKhoan, @TenChuSoHuu, @TenChiNhanh);
    
    -- Trả về thông báo thành công
    SELECT 'Thêm tài khoản ngân hàng thành công!' AS Message;
END
GO
CREATE PROCEDURE pr_LayTenChiNhanh
    @maNganHangLienKet INT
AS
BEGIN
    -- Truy vấn để lấy tên chi nhánh của ngân hàng
    SELECT maChiNhanh, TenChiNhanh
    FROM ChiNhanhNganHang
    WHERE maNganHangLienKet = @maNganHangLienKet
END
GO
CREATE PROCEDURE pr_ThemDiaChiGiaoHang
    @maAccount VARCHAR(255),
    @MaTinhThanh INT,
    @MaQuanHuyen INT,
    @MaXaPhuong INT,
    @TenKhachHang NVARCHAR(255),
    @SDT VARCHAR(20),
    @DiaChiGiaoHang NVARCHAR(255)
AS
BEGIN
    -- Thêm dữ liệu vào bảng DiaChiGiaoHang
    INSERT INTO DiaChiGiaoHang (maAccount, MaTinhThanh, MaQuanHuyen, MaXaPhuong, TenKhachHang, SDT, DiaChi)
    VALUES (@maAccount, @MaTinhThanh, @MaQuanHuyen, @MaXaPhuong, @TenKhachHang, @SDT, @DiaChiGiaoHang);
END;
GO
CREATE PROCEDURE pr_CapNhatDiaChiGiaoHang
    @MaDiaChi INT,
    @MaAccount VARCHAR(255),
    @MaTinhThanh INT,
    @MaQuanHuyen INT,
    @MaXaPhuong INT,
    @TenKhachHang NVARCHAR(255),
    @SDT VARCHAR(20),
    @DiaChiGiaoHang NVARCHAR(MAX)
AS
BEGIN
    -- Kiểm tra xem địa chỉ có tồn tại không
    IF EXISTS (SELECT 1 FROM DiaChiGiaoHang WHERE MaDiaChi = @MaDiaChi)
    BEGIN
        -- Cập nhật thông tin địa chỉ giao hàng
        UPDATE DiaChiGiaoHang
        SET
            MaAccount = @MaAccount,
            MaTinhThanh = @MaTinhThanh,
            MaQuanHuyen = @MaQuanHuyen,
            MaXaPhuong = @MaXaPhuong,
            TenKhachHang = @TenKhachHang,
            SDT = @SDT,
            DiaChi = @DiaChiGiaoHang
        WHERE MaDiaChi = @MaDiaChi;

        -- Trả về kết quả thành công
        SELECT 1 AS Result, 'Cập nhật địa chỉ thành công!' AS Message;
    END
    ELSE
    BEGIN
        -- Trả về thông báo không tìm thấy địa chỉ
        SELECT 0 AS Result, 'Không tìm thấy địa chỉ để cập nhật!' AS Message;
    END
END;
GO
CREATE PROCEDURE pr_LayTatCaBoiMaDiaChi
    @MaDiaChi INT
AS
BEGIN
    -- Lấy tất cả các thông tin từ bảng DiaChiGiaoHang theo MaDiaChi
    SELECT
        MaDiaChi,
        maAccount,
        MaTinhThanh,
        MaQuanHuyen,
        MaXaPhuong,
        TenKhachHang,
        SDT,
        DiaChi
    FROM DiaChiGiaoHang
    WHERE MaDiaChi = @MaDiaChi;
END;

exec pr_LayTatCaBoiMaDiaChi 1;
GO
CREATE PROCEDURE pr_LayChiTietDonHang (@maDonHang VARCHAR(15))
AS
BEGIN
    SELECT 
        sp.tenSanPham AS TenSanPham,
        sp.anh AS AnhSanPham,
        -- Phân loại = màu sắc + kích thước
        CONCAT(ISNULL(ms.mauSac, ''), '; ', ISNULL(kt.kichthuoc, '')) AS PhanLoai,
        ctdh.soLuongMua AS SoLuong,
        sp.price AS GiaTien
    FROM 
        ChiTietDonHang ctdh
    JOIN 
        SanPham sp ON ctdh.maSanPham = sp.id
    LEFT JOIN 
        MauSac ms ON ms.maSanPham = sp.id
    LEFT JOIN 
        KichThuoc kt ON kt.maSanPham = sp.id
    WHERE 
        ctdh.maDonHang = @maDonHang;
END;
EXEC pr_LayChiTietDonHang 'DH001';
GO
CREATE PROCEDURE pr_LayDonHangThanhToan (@maDonHang VARCHAR(15))
AS
BEGIN
    SELECT 
        -- Thông tin từ bảng ThanhToan
        tt.maDonHang AS MaDonHang,
        tt.luuYKhiNhanHang AS LuuYKhiNhanHang,
        
        -- Thông tin trạng thái giao hàng từ bảng TrangThaiGiaoHang
        tg.trangThai AS TrangThai,
        
        -- Thông tin thời gian, định dạng dd/MM/yyyy
        FORMAT(MIN(tg.thoiGian), 'dd/MM/yyyy') AS ThoiGianDat,
        FORMAT(DATEADD(DAY, 3, MIN(tg.thoiGian)), 'dd/MM/yyyy') AS ThoiGianBD, -- 3 ngày sau
        FORMAT(DATEADD(DAY, 5, MIN(tg.thoiGian)), 'dd/MM/yyyy') AS ThoiGianKT, -- 5 ngày sau
        
        -- Thông tin từ bảng DiaChiGiaoHang
        dc.TenKhachHang AS TenKhachHang,
        dc.DiaChi AS DiaChi,
        dc.SDT AS SDT,
        
        -- Mức giảm từ bảng Voucher
        v.mucGiam AS MucGiam,  -- Lấy mức giảm từ bảng Voucher
        
        -- Tổng tiền từ bảng ThanhToan
        tt.TongTien AS TongTien
    FROM 
        ThanhToan tt
    LEFT JOIN 
        TrangThaiGiaoHang tg ON tt.maDonHang = tg.maDonHang
    LEFT JOIN 
        DiaChiGiaoHang dc ON tt.MaDiaChi = dc.MaDiaChi  -- JOIN với DiaChiGiaoHang qua MaDiaChi
    LEFT JOIN 
        VoucherCuaToi vct ON tt.maVoucherCuaToi = vct.maVoucherCuaToi  -- JOIN với VoucherCuaToi
    LEFT JOIN 
        Voucher v ON vct.maVoucher = v.maVoucher  -- JOIN với bảng Voucher để lấy mucGiam
    WHERE 
        tt.maDonHang = @maDonHang
    GROUP BY 
        tt.maDonHang, tt.luuYKhiNhanHang, tg.trangThai, dc.TenKhachHang, dc.DiaChi, dc.SDT, v.mucGiam, tt.TongTien;
END;

EXEC pr_LayDonHangThanhToan 'DH001';
GO
CREATE PROCEDURE pr_LayDonHangTheoTrangThai
    @TrangThai NVARCHAR(50)
AS
BEGIN
    SELECT 
        dh.maDonHang, 
        dh.maAccount,
        tt.TongTien
    FROM 
        DonHang dh
    JOIN 
        TrangThaiGiaoHang tgh ON dh.maDonHang = tgh.maDonHang
    LEFT JOIN 
        ThanhToan tt ON dh.maDonHang = tt.maDonHang
    WHERE 
        tgh.trangThai = @TrangThai;
END;

