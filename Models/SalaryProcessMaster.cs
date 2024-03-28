namespace Accura_Innovatives.Models
{
    public partial class SalaryProcessMaster
    {
        public int EmpCode { get; set; }
        public string EmpName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Grade { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string DateOfJoining { get; set; } = null!;
        public string? Qualification { get; set; }
        public string MaritalStatus { get; set; } = null!;
        public string PaymentMode { get; set; } = null!;
        public string? BankName { get; set; }
        public string? AccNo { get; set; }
        public string Category { get; set; } = null!;
        public string? EsiNo { get; set; }
        public string? EpfNo { get; set; }

    }
}
