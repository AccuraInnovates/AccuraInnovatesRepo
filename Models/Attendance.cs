namespace Accura_Innovatives.Models
{
    public class Attendance
    {
        public int AttDedId { get; set; } = 0;
        public string Month { get; set; } = null!;
        public int EmpCode { get; set; } = 0;
        public string EmpName { get; set; } = null!;
        public double TotalShift { get; set; } = 0;
        public int OTDay { get; set; }
        public TimeOnly TotalOTHrs { get; set; } = new TimeOnly(0, 0, 0);
        public int NationalHolidays { get; set; } = 0;
        public double CompOffDays { get; set; } = 0;
        public double ClDays { get; set; } = 0;
        public double SlDays { get; set; } = 0;
        public double LOPDays { get; set; } = 0;
        public double Advance1Amount { get; set; } = 0;
        public double Advance2Amount { get; set; } = 0;
        public double MessDeductionAmount { get; set; } = 0;
        public double PenaltyDeductionAmount { get; set; } = 0;
        public double IncentiveAmountSalesOthers { get; set; } = 0;
        public double OtherEarnings { get; set; } = 0;
        public double OtherDeductions { get; set; } = 0;
        public double TDS { get; set; } = 0;
        public int NoOfCalenderDaysInCurrentMonth { get; set; } = 0;
        public int MonthlyWeekOffDays { get; set; } = 0;
        public virtual EmployeeMasterData1? Emp { get; set; }
    }
}
