Create database dbQuanLyBanHang;
go
use dbQuanLyBanHang;
go
-- 1: KhachHang
CREATE TABLE KhachHang (
    username VARCHAR(255) PRIMARY KEY,
    matKhau VARCHAR(255),
    firstName NVARCHAR(255),
    lastName NVARCHAR(255),
    day INT,
    moth INT,
    year INT,
    gender NVARCHAR(50),
	anh VARCHAR(MAX),
);

-- 2: SanPham
CREATE TABLE SanPham (
    id VARCHAR(20) PRIMARY KEY,
    maphanLoai INT,
	anh VARCHAR(MAX),
    tenSanPham NVARCHAR(255),
    price DECIMAL(10, 2),
    moTa NVARCHAR(MAX),
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

-- 2.3: AnhSanPham
CREATE TABLE AnhSanPham (
    maAnhSanPham INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    anh VARCHAR(MAX)
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

-- 6: DanhGia
CREATE TABLE DanhGia (
    maDanhGia INT PRIMARY KEY IDENTITY(1,1),
    maSanPham VARCHAR(20),
    maAccount VARCHAR(255),
    xepHang DECIMAL(2,1),
    reviewer NVARCHAR(255)
);

CREATE TABLE NganHangDuocLienKet (
	MaNganHangLienKet INT PRIMARY KEY IDENTITY(1,1),
    TenNganHang NVARCHAR(255),
	anhnganhang VARCHAR(MAX),
);

CREATE TABLE ChiNhanhNganHang (
    maChiNhanh INT PRIMARY KEY IDENTITY(1,1),   
    maNganHangLienKet INT,    -- làm khóa phụ    
    TenChiNhanh NVARCHAR(255)        
);

CREATE TABLE TaiKhoanNganHangDuocLienKet (
	SoTaiKhoan VARCHAR(14) PRIMARY KEY,
	MaNganHangLienKet INT, -- làm khóa phụ
	TenChuSoHuu NVARCHAR(50)
);

-- 7: NganHang
CREATE TABLE TaiKhoanNganHangCuaToi (
    MaTaiKhoan INT PRIMARY KEY IDENTITY(1,1),
    maAccount VARCHAR(255), -- làm khóa phụ
    maNganHangLienKet INT,
	SoTaiKhoan VARCHAR(14),
	TenChuSoHuu NVARCHAR(50),
    TenChiNhanh NVARCHAR(255)        
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
	TenKhachHang NVARCHAR(255),
    SDT VARCHAR(20),
    DiaChi NVARCHAR(MAX)
);

-- 13: ChiTietGioHang
CREATE TABLE ChiTietDonHang (
    maChiTiet INT PRIMARY KEY IDENTITY(1,1),
    maDonHang VARCHAR(15),
    maSanPham VARCHAR(20),
    soLuongMua INT
);

-- 14: ThanhToan
CREATE TABLE ThanhToan (
    maThanhToan INT PRIMARY KEY IDENTITY(1,1),
    maDonHang VARCHAR(15),
	MaDiaChi INT,
    phuongThucThanhToan NVARCHAR(255),
    luuYKhiNhanHang NVARCHAR(255),
	maVoucherCuaToi INT,
	TongTien INT
);

-- 15: DonHang
CREATE TABLE DonHang (
    maDonHang VARCHAR(15) PRIMARY KEY,
	maAccount VARCHAR(255)
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
	tenVoucher NVARCHAR(50),
	hanSuDung DATE,
	mucGiam INT,
    MaDieuKien INT,
    maTrangThaiGiam INT,
);

-- 19: DieuKienGiam
CREATE TABLE DieuKienGiam (
    maDieuKien INT PRIMARY KEY IDENTITY(1,1),
    DieuKien NVARCHAR(50),
	MucDieuKienGiam INT
);

-- 20: TrangThaiGiam
CREATE TABLE TrangThaiGiam (
    maTrangThaiGiam INT PRIMARY KEY IDENTITY(1,1),
    trangThaiGiam NVARCHAR(255)
);

CREATE TABLE VoucherCuaToi (
    maVoucherCuaToi INT PRIMARY KEY IDENTITY(1,1),
	maVoucher VARCHAR(10), -- làm khóa ngoại
    maAccount VARCHAR(255), -- làm khóa ngoại
	trangThaiSuDung NVARCHAR(255)

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
ADD CONSTRAINT FK_QuanHuyen_TinhThanh FOREIGN KEY (MaTinhThanh) 
REFERENCES TinhThanhPho(MaTinhThanh) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 10: XaPhuong
ALTER TABLE XaPhuong
ADD CONSTRAINT FK_XaPhuong_QuanHuyen FOREIGN KEY (MaQuanHuyen) 
REFERENCES QuanHuyen(MaQuanHuyen) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 2: SanPham
ALTER TABLE SanPham
ADD CONSTRAINT FK_SanPham_PhanLoai FOREIGN KEY (maphanLoai) 
REFERENCES PhanLoai(maPhanLoai) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 2.1: KichThuoc
ALTER TABLE KichThuoc
ADD CONSTRAINT FK_KichThuoc_SanPham FOREIGN KEY (maSanPham) 
REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 2.2: MauSac
ALTER TABLE MauSac
ADD CONSTRAINT FK_MauSac_SanPham FOREIGN KEY (maSanPham) 
REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 2.3: AnhSanPham
ALTER TABLE AnhSanPham
ADD CONSTRAINT FK_AnhSanPham_SanPham FOREIGN KEY (maSanPham) 
REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 3: ChiTietSanPham
ALTER TABLE ChiTietSanPham
ADD CONSTRAINT FK_ChiTietSanPham_SanPham FOREIGN KEY (maSanPham) 
REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 6: DanhGia
ALTER TABLE DanhGia
ADD CONSTRAINT FK_DanhGia_SanPham FOREIGN KEY (maSanPham) 
REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_DanhGia_KhachHang FOREIGN KEY (maAccount) 
	REFERENCES KhachHang(username) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 11: DiaChiGiaoHang
ALTER TABLE DiaChiGiaoHang
ADD CONSTRAINT FK_DiaChi_KhachHang FOREIGN KEY (maAccount) 
REFERENCES KhachHang(username) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_DiaChi_TinhThanh FOREIGN KEY (MaTinhThanh) 
	REFERENCES TinhThanhPho(MaTinhThanh) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_DiaChi_QuanHuyen FOREIGN KEY (MaQuanHuyen) 
	REFERENCES QuanHuyen(MaQuanHuyen) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_DiaChi_XaPhuong FOREIGN KEY (MaXaPhuong) 
	REFERENCES XaPhuong(MaXaPhuong) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 16: TrangThaiGiaoHang
ALTER TABLE TrangThaiGiaoHang
ADD CONSTRAINT FK_TrangThaiGiaoHang_DonHang FOREIGN KEY (maDonHang) 
REFERENCES DonHang(maDonHang) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 17: ThongBao
ALTER TABLE ThongBao
ADD CONSTRAINT FK_ThongBao_DonHang FOREIGN KEY (maDonHang) 
REFERENCES DonHang(maDonHang) ON DELETE CASCADE ON UPDATE CASCADE;

-- Bảng 18: Voucher
ALTER TABLE Voucher
ADD CONSTRAINT FK_Voucher_DieuKienGiam
FOREIGN KEY (MaDieuKien) REFERENCES DieuKienGiam(maDieuKien) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE Voucher
ADD CONSTRAINT FK_Voucher_TrangThaiGiam
FOREIGN KEY (maTrangThaiGiam) REFERENCES TrangThaiGiam(maTrangThaiGiam) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE VoucherCuaToi
ADD CONSTRAINT FK_VoucherCuaToi_Voucher
FOREIGN KEY (maVoucher) REFERENCES Voucher(maVoucher) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE VoucherCuaToi
ADD CONSTRAINT FK_VoucherCuaToi_KhachHang
FOREIGN KEY (maAccount) REFERENCES KhachHang(username)  ON DELETE CASCADE ON UPDATE CASCADE;


-- Ngân hàng --
ALTER TABLE ChiNhanhNganHang
ADD CONSTRAINT FK_ChiNhanhNganHang_NganHangDuocLienKet FOREIGN KEY (maNganHangLienKet)
REFERENCES NganHangDuocLienKet(MaNganHangLienKet) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE TaiKhoanNganHangDuocLienKet
ADD CONSTRAINT FK_TaiKhoanNganHangDuocLienKet_NganHangDuocLienKet FOREIGN KEY (MaNganHangLienKet)
REFERENCES NganHangDuocLienKet(MaNganHangLienKet) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE TaiKhoanNganHangCuaToi
ADD CONSTRAINT FK_TaiKhoanNganHangCuaToi_KhachHang FOREIGN KEY (maAccount)
REFERENCES KhachHang(username) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE TaiKhoanNganHangCuaToi
ADD CONSTRAINT FK_TaiKhoanNganHangCuaToi_NganHangDuocLienKet FOREIGN KEY (maNganHangLienKet)
REFERENCES NganHangDuocLienKet(MaNganHangLienKet) ON DELETE CASCADE ON UPDATE CASCADE;


-- Ensure DonHang table exists
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_ChiTietDonHang_DonHang
FOREIGN KEY (maDonHang) REFERENCES DonHang(maDonHang) ON DELETE CASCADE ON UPDATE CASCADE;

-- Ensure SanPham table exists (if applicable)
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_ChiTietDonHang_SanPham
FOREIGN KEY (maSanPham) REFERENCES SanPham(id) ON DELETE CASCADE ON UPDATE CASCADE;  

-- Ensure KhachHang table exists
ALTER TABLE DonHang
ADD CONSTRAINT FK_DonHang_Account
FOREIGN KEY (maAccount) REFERENCES KhachHang(username) ON DELETE CASCADE ON UPDATE CASCADE; 

-- Ensure DonHang table exists
ALTER TABLE ThanhToan
ADD CONSTRAINT FK_ThanhToan_DonHang
FOREIGN KEY (maDonHang) REFERENCES DonHang(maDonHang) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE ThanhToan
ADD CONSTRAINT FK_ThanhToan_VoucherCuaToi
FOREIGN KEY (maVoucherCuaToi) REFERENCES VoucherCuaToi(maVoucherCuaToi) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE ThanhToan
ADD CONSTRAINT FK_ThanhToan_DiaChiGiaoHang
FOREIGN KEY (MaDiaChi) REFERENCES DiaChiGiaoHang(MaDiaChi) ON DELETE CASCADE ON UPDATE CASCADE;