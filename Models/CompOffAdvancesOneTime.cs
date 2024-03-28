namespace Accura_Innovatives.Models
{
    public partial class CompOffAdvancesOneTime
    {
        public int CompOffAdvancesID { get; set; } = 0;
        public string DateOfImport { get; set; } = null!;
        public string Month { get; set; } = null!;
        public string? HeadOpfAccount { get; set; }
        public int EmpCode { get; set; } = 0;
        public string? EmpName { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
        //public virtual EmployeeMasterData1? EmpNavi { get; set; }
    }
}
