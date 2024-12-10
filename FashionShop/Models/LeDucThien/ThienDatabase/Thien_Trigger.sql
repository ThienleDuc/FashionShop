use dbQuanLyBanHang
go
CREATE TRIGGER trg__UpdateTrangThaiSuDung
ON VoucherCuaToi
AFTER INSERT
AS
BEGIN
    -- Cập nhật cột trangThaiSuDung thành 'Chưa sử dụng' sau khi insert vào bảng VoucherCuaToi
    UPDATE vc
    SET vc.trangThaiSuDung = N'Chưa sử dụng'
    FROM VoucherCuaToi vc
    INNER JOIN INSERTED i ON vc.maVoucherCuaToi = i.maVoucherCuaToi
    WHERE vc.trangThaiSuDung IS NULL;
END;
GO