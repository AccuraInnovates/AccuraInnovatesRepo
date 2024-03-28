namespace Accura_Innovatives.Models
{
    public partial class ConsolidatedModel
    {
        public int Id { get; set; }
        public string? MonthYear { get; set; }
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string? CompanyName { get; set; }
        public string? Gender { get; set; }
        public string? Grade { get; set; }
        public string? DOB { get; set; }
        public string? DOJ { get; set; }
        public string? Qualification { get; set; }
        public string? MaritalStatus { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? EsiEpfEligibility { get; set; }
        public string? EsiNo { get; set; }
        public string? EpfNo { get; set; }
        public int? TotalWorkingDays { get; set; }
        public double? NoOfPresentDays { get; set; }
        public double? WagesPerShift { get; set; }
        public double? LOPDays { get; set; }
        public string? OTHrs { get; set; }
        public double? BasicPay { get; set; }
        public double? HRA { get; set; }
        public double? Incentive1 { get; set; }
        public double? Incentive2 { get; set; }
        public double? OtherEarnings { get; set; }
        public double? PfEmployerContribution { get; set; }
        public double? PfEmployeeContribution { get; set; }
        public double? EPSEmployerContribution { get; set; }
        public double? EsiEmployerContribution { get; set; }
        public double? EsiEmployeeContribution { get; set; }
        public double? OpeningAdvance1 { get; set; }
        public double? OpeningAdvance2 { get; set; }
        public double? DeductedAdvance1 { get; set; }
        public double? DeductedAdvance2 { get; set; }
        public double? Advance1 { get; set; }
        public double? Advance2 { get; set; }
        public double? ClosingBalanceAdvance1 { get; set; }
        public double? ClosingBalanceAdvance2 { get; set; }
        public double? Mess { get; set; }
        public double? TDS { get; set; }
        public double? OtherDeductions { get; set; }
        public double? GrossPay { get; set; }
        public double? Deductions { get; set; }
        public double? NetPayBeforeCeiling { get; set; }
        public double? NetPayAfterCeiling { get; set; }
        public double? CTC { get; set; }
        public string? Contractor { get; set; }
        public int? TotalWeekOffDays { get; set;}
        public int? TotalAllowedCL { get; set; }
        public int? TotalAllowedSL { get; set; }
        public double? PreMonthsConsumedCL { get; set; }
        public double? PreMonthsConsumedSL { get; set; }
        public double? CurrentCL { get; set; }
        public double? CurrentSL { get; set;}
        public double? BalanceCL { get; set; }
        public double? BalanceSL { get;set; }
        public double? OpeningBalanceCompOff { get; set; }
        public double? CurrentMonthCompOffToBeAdded { get; set; }
        public double? CurrentMonthCompOffConsumed { get; set; }
        public double? BalanceCompOff { get; set; }
        public int? NH { get; set; }
        public double? Penalty { get; set; }
        public double? ActualGross { get; set; }
        public double? ActualBasic { get; set; }
        public double? ActualHRA { get; set; }
        public double? ActualIncentive1 { get; set; }
        public double? ActualIncentive2 { get; set; }
        public double? ActualOtherEarnings { get; set; }
        public double? ActualSalary { get; set; }
        public double? ActualNetPay { get; set; }

    }
}
