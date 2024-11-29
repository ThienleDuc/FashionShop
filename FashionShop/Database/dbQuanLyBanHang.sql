Create database dbQuanLyBanHang;
go
use dbQuanLyBanHang;
go
-- 1: KhachHang
CREATE TABLE KhachHang (
    username VARCHAR(255) PRIMARY KEY,
    password VARCHAR(255),
    firstName NVARCHAR(255),
    lastName NVARCHAR(255),
    day INT,
    moth INT,
    year INT,
    gender NVARCHAR(50)
);

-- 2: SanPham
CREATE TABLE SanPham (
    id VARCHAR(20) PRIMARY KEY,
    maphanLoai INT,
    anh VARCHAR(MAX),
    name NVARCHAR(255),
    price DECIMAL(10, 2),
    description NVARCHAR(MAX),
    SoLuongHienCo INT,
    soLuongBanRa INT DEFAULT 0
);

-- 2.1: KichThuoc
CREATE TABLE KichThuoc (
    maKichThuoc INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    kichthuoc VARCHAR(35)
);

-- 2.2: MauSac
CREATE TABLE MauSac (
    maMauSac INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    mauSac NVARCHAR(255)
);

-- 3: ChiTietSanPham
CREATE TABLE ChiTietSanPham (
    maChiTiet INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    TieuDe NVARCHAR(100),
    MoTa NVARCHAR(200)
);

-- 4: PhanLoai
CREATE TABLE PhanLoai (
    maPhanLoai INT PRIMARY KEY IDENTITY(1,1),
    tenPhanLoai NVARCHAR(255)
);

-- 5: PhanLoaiCon
CREATE TABLE PhanLoaiCon (
    maPhanLoaiCon INT PRIMARY KEY IDENTITY(1,1),
    maPhanLoaiCha INT
);

-- 6: DanhGia
CREATE TABLE DanhGia (
    maDanhGia INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    maAccount VARCHAR(255),
    xepHang DECIMAL(2,1),
    reviewer NVARCHAR(255)
);

-- 7: NganHang
CREATE TABLE NganHang (
    MaNganHang INT PRIMARY KEY IDENTITY(1,1),
    maAccount VARCHAR(255),
    TenNganHang NVARCHAR(255),
    TenChuSoHuu NVARCHAR(50),
    SoTaiKhoan VARCHAR(255),
    ChiNhanh NVARCHAR(50)
);

-- 8: TinhThanhPho
CREATE TABLE TinhThanhPho (
    MaTinhThanh INT PRIMARY KEY IDENTITY(1,1),
    TenTinhThanh NVARCHAR(255)
);

-- 9: QuanHuyen
CREATE TABLE QuanHuyen (
    MaQuanHuyen INT PRIMARY KEY IDENTITY(1,1),
    MaTinhThanh INT,
    TenQuanHuyen NVARCHAR(255)
);

-- 10: XaPhuong
CREATE TABLE XaPhuong (
    MaXaPhuong INT PRIMARY KEY IDENTITY(1,1),
    MaQuanHuyen INT,
    TenXaPhuong NVARCHAR(255)
);

-- 11: DiaChiGiaoHang
CREATE TABLE DiaChiGiaoHang (
    MaDiaChi INT PRIMARY KEY IDENTITY(1,1),
    maAccount VARCHAR(255),
    MaTinhThanh INT,
    MaQuanHuyen INT,
    MaXaPhuong INT,
    SDT VARCHAR(20),
    DiaChiGiaoHang NVARCHAR(255)
);

-- 12: GioHang
CREATE TABLE GioHang (
    maGioHang INT PRIMARY KEY IDENTITY(1,1),
    maAccount VARCHAR(255)
);

-- 13: ChiTietGioHang
CREATE TABLE ChiTietGioHang (
    maChiTiet INT PRIMARY KEY IDENTITY(1,1),
    maGioHang INT,
    maSanPham VARCHAR(20),
    soLuongMua INT
);

-- 14: ThanhToan
CREATE TABLE ThanhToan (
    maThanhToan INT PRIMARY KEY IDENTITY(1,1),
    maGioHang INT,
    phuongThucThanhToan NVARCHAR(255),
    luuYKhiNhanHang NVARCHAR(255)
);

-- 15: DonHang
CREATE TABLE DonHang (
    maDonHang VARCHAR(15) PRIMARY KEY,
    maGioHang INT
);

-- 16: TrangThaiGiaoHang
CREATE TABLE TrangThaiGiaoHang (
    maTrangThai INT PRIMARY KEY IDENTITY(1,1),
    maDonHang VARCHAR(15),
    trangThai NVARCHAR(50),
    thoiGian DATETIME
);

-- 17: ThongBao
CREATE TABLE ThongBao (
    maThongBao INT PRIMARY KEY IDENTITY(1,1),
    maDonHang VARCHAR(15)
);

-- 18: Voucher
CREATE TABLE Voucher (
    maVoucher VARCHAR(10) PRIMARY KEY,
    maAccount VARCHAR(255),
    hanSuDung DATE,
    MaDieuKien INT,
    maTrangThaiGiam INT
);

-- 19: DieuKienGiam
CREATE TABLE DieuKienGiam (
    maDieuKien INT PRIMARY KEY IDENTITY(1,1),
    DieuKien NVARCHAR(50)
);

-- 20: TrangThaiGiam
CREATE TABLE TrangThaiGiam (
    maTrangThaiGiam INT PRIMARY KEY IDENTITY(1,1),
    trangThaiGiam NVARCHAR(255),
    mucGiam INT
);

-- Bảng 1: KhachHang
-- (Không có khóa ngoại, không cần ràng buộc)

-- Bảng 4: PhanLoai
-- (Không có khóa ngoại, không cần ràng buộc)

-- Bảng 19: DieuKienGiam
-- (Không có khóa ngoại, không cần ràng buộc)

-- Bảng 20: TrangThaiGiam
-- (Không có khóa ngoại, không cần ràng buộc)

-- Bảng 8: TinhThanhPho
-- (Không có khóa ngoại, không cần ràng buộc)

-- Bảng 9: QuanHuyen
ALTER TABLE QuanHuyen
ADD CONSTRAINT FK_QuanHuyen_TinhThanh FOREIGN KEY (MaTinhThanh) REFERENCES TinhThanhPho(MaTinhThanh);

-- Bảng 10: XaPhuong
ALTER TABLE XaPhuong
ADD CONSTRAINT FK_XaPhuong_QuanHuyen FOREIGN KEY (MaQuanHuyen) REFERENCES QuanHuyen(MaQuanHuyen);

-- Bảng 2: SanPham
ALTER TABLE SanPham
ADD CONSTRAINT FK_SanPham_PhanLoai FOREIGN KEY (maphanLoai) REFERENCES PhanLoai(maPhanLoai);

-- Bảng 2.1: KichThuoc
ALTER TABLE KichThuoc
ADD CONSTRAINT FK_KichThuoc_SanPham FOREIGN KEY (maSanPham) REFERENCES SanPham(id);

-- Bảng 2.2: MauSac
ALTER TABLE MauSac
ADD CONSTRAINT FK_MauSac_SanPham FOREIGN KEY (maSanPham) REFERENCES SanPham(id);

-- Bảng 5: PhanLoaiCon
ALTER TABLE PhanLoaiCon
ADD CONSTRAINT FK_PhanLoaiCon_PhanLoai FOREIGN KEY (maPhanLoaiCha) REFERENCES PhanLoai(maPhanLoai);

-- Bảng 3: ChiTietSanPham
ALTER TABLE ChiTietSanPham
ADD CONSTRAINT FK_ChiTietSanPham_SanPham FOREIGN KEY (maSanPham) REFERENCES SanPham(id);

-- Bảng 6: DanhGia
ALTER TABLE DanhGia
ADD CONSTRAINT FK_DanhGia_SanPham FOREIGN KEY (maSanPham) REFERENCES SanPham(id),
    CONSTRAINT FK_DanhGia_KhachHang FOREIGN KEY (maAccount) REFERENCES KhachHang(username);

-- Bảng 7: NganHang
ALTER TABLE NganHang
ADD CONSTRAINT FK_NganHang_KhachHang FOREIGN KEY (maAccount) REFERENCES KhachHang(username);

-- Bảng 11: DiaChiGiaoHang
ALTER TABLE DiaChiGiaoHang
ADD CONSTRAINT FK_DiaChi_KhachHang FOREIGN KEY (maAccount) REFERENCES KhachHang(username),
    CONSTRAINT FK_DiaChi_TinhThanh FOREIGN KEY (MaTinhThanh) REFERENCES TinhThanhPho(MaTinhThanh),
    CONSTRAINT FK_DiaChi_QuanHuyen FOREIGN KEY (MaQuanHuyen) REFERENCES QuanHuyen(MaQuanHuyen),
    CONSTRAINT FK_DiaChi_XaPhuong FOREIGN KEY (MaXaPhuong) REFERENCES XaPhuong(MaXaPhuong);

-- Bảng 12: GioHang
ALTER TABLE GioHang
ADD CONSTRAINT FK_GioHang_KhachHang FOREIGN KEY (maAccount) REFERENCES KhachHang(username);

-- Bảng 13: ChiTietGioHang
ALTER TABLE ChiTietGioHang
ADD CONSTRAINT FK_ChiTietGioHang_GioHang FOREIGN KEY (maGioHang) REFERENCES GioHang(maGioHang),
    CONSTRAINT FK_ChiTietGioHang_SanPham FOREIGN KEY (maSanPham) REFERENCES SanPham(id);

-- Bảng 14: ThanhToan
ALTER TABLE ThanhToan
ADD CONSTRAINT FK_ThanhToan_GioHang FOREIGN KEY (maGioHang) REFERENCES GioHang(maGioHang);

-- Bảng 15: DonHang
ALTER TABLE DonHang
ADD CONSTRAINT FK_DonHang_GioHang FOREIGN KEY (maGioHang) REFERENCES GioHang(maGioHang);

-- Bảng 16: TrangThaiGiaoHang
ALTER TABLE TrangThaiGiaoHang
ADD CONSTRAINT FK_TrangThaiGiaoHang_DonHang FOREIGN KEY (maDonHang) REFERENCES DonHang(maDonHang);

-- Bảng 17: ThongBao
ALTER TABLE ThongBao
ADD CONSTRAINT FK_ThongBao_DonHang FOREIGN KEY (maDonHang) REFERENCES DonHang(maDonHang);

-- Bảng 18: Voucher
ALTER TABLE Voucher
ADD CONSTRAINT FK_Voucher_KhachHang FOREIGN KEY (maAccount) REFERENCES KhachHang(username),
    CONSTRAINT FK_Voucher_DieuKien FOREIGN KEY (MaDieuKien) REFERENCES DieuKienGiam(maDieuKien),
    CONSTRAINT FK_Voucher_TrangThaiGiam FOREIGN KEY (maTrangThaiGiam) REFERENCES TrangThaiGiam(maTrangThaiGiam);
