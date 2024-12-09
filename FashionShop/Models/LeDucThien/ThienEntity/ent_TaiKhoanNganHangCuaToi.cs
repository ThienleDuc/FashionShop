namespace FashionShop.Models.LeDucThien.ThienEntity
{
    public class ent_TaiKhoanNganHangCuaToi
    {
        public int BankID { get; set; }           // Mã tài khoản ngân hàng (Trước đây là MaTaiKhoan)
        public string BankName { get; set; }      // Tên ngân hàng
        public string BankLogo { get; set; }      // Logo ngân hàng
        public string AccountOwner { get; set; }  // Tên chủ sở hữu tài khoản
        public string BranchName { get; set; }    // Tên chi nhánh
        public string AccountNumber { get; set; } // Số tài khoản

        // Constructor mặc định
        public ent_TaiKhoanNganHangCuaToi() { }

        // Constructor có tham số
        public ent_TaiKhoanNganHangCuaToi(int bankID, string bankName, string bankLogo, string accountOwner, string branchName, string accountNumber)
        {
            BankID = bankID;
            BankName = bankName;
            BankLogo = bankLogo;
            AccountOwner = accountOwner;
            BranchName = branchName;
            AccountNumber = accountNumber;
        }
    }
}
