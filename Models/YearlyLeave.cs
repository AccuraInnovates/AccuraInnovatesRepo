namespace Accura_Innovatives.Models
{
    public partial class YearlyLeave
    {
        public int YearlyLeaveId { get; set; } = 0;
        public string DateOfImport { get; set; } = null!;
        public int EmpCode { get; set; } = 0;
        public string? EmpName { get; set; }
        public int? CL { get; set; }
        public int? SL { get; set; }
        //public virtual EmployeeMasterData1? Emp { get; set; }
    }
}
