using Accura_Innovatives.Models;
using System.ComponentModel.DataAnnotations;


namespace Accura_Innovatives
{
    public class EmployeeViewModel
    {
        public string EmpCtg { get; set; } = null!;
        
        public int EmpCode { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string EmpName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required.")]
        public string? EmpAadharName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? EmpPanName { get; set; } 

        public string? EmpCertifName { get; set; } 

        public string? EmpNameInBank { get; set; }

        public string? EmpBcardName { get; set; }

        public IFormFile? ProfilePhoto { get; set; }

        public string Gender { get; set; } = null!;

        public string? BloodGrp { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? AadharNo { get; set; } 

        public IFormFile? AadharCard { get; set; }

        public string? PanNo { get; set; }

        public IFormFile? PanCard { get; set; }

        public string? DrvLinNo { get; set; }

        public string? DrvLinVal { get; set; }

        public IFormFile? DrvLinCard { get; set; }

        public string? PassportNo { get; set; }

        public string? PassportVal { get; set; }

        public IFormFile? PassportCard { get; set; }
        [Required(ErrorMessage = "The Aadhar Date Of Birth field is required.")]
        public string? AadharDob { get; set; } 

        public string? Dob { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PresentAddressLine1 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PresentAddressLine2 { get; set; }
        
        public string? PresentAddressCity { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PresentAddressState { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int? PresentAddressZipcode { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PermAddressLine1 { get; set; } 

        public string? PermAddressLine2 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PermAddressCity { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PermAddressState { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int? PermAddressZipcode { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? CommAddressLine1 { get; set; } 

        public string? CommAddressLine2 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? CommAddressCity { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? CommAddressState { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int? CommAddressZipcode { get; set; }

        public string? Nationality { get; set; }

        public string? MaritalStatus { get; set; }

        public string? PerEmail { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? PerMobile { get; set; } 

        public string? PerAccNo { get; set; }

        public string? PerBnkName { get; set; }

        public string? PerBnkIfsc { get; set; }

        public string? PerBnkBranch { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemName1 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemRel1 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemContact1 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemName2 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemRel2 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? FamMemContact2 { get; set; } 

        public string? FamMemName3 { get; set; }

        public string? FamMemRel3 { get; set; }

        public string? FamMemContact3 { get; set; }

        public string? FamMemName4 { get; set; }

        public string? FamMemRel4 { get; set; }

        public string? FamMemContact4 { get; set; }

        public string? FamMemName5 { get; set; }

        public string? FamMemRel5 { get; set; }

        public string? FamMemContact5 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? EmerContactName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? EmerContactRel { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? EmerContactNo { get; set; } 

        public string? HighQuali { get; set; }

        public string? HighQualiInstituteName { get; set; }

        public double? HighQualiMark { get; set; }

        public string? HighQualiPassYear { get; set; }

        public IFormFile? HighQualiCerf1 { get; set; }

        public string? HighQuali2 { get; set; }

        public string? HighQualiInstituteName2 { get; set; }

        public double? HighQualiMark2 { get; set; }

        public string? HighQualiPassYear2 { get; set; }

        public IFormFile? HighQualiCerf2 { get; set; }

        public string? HscSchoolName { get; set; }

        public double? HscMark { get; set; }

        public string? HscPassYear { get; set; }

        public IFormFile? HscCerf { get; set; }

        public string? SslcSchoolName { get; set; }

        public double? SslcMark { get; set; }

        public string? SslcPassYear { get; set; }

        public IFormFile? SslcCerf { get; set; }

        public string? OtherCerfName1 { get; set; }

        public string? OtherCerfInstitute1 { get; set; }

        public double? OtherCerfMark1 { get; set; }

        public string? OtherCerfDuration1 { get; set; }

        public string? OtherCerfPassYear1 { get; set; }

        public IFormFile? OtherCerf1 { get; set; }

        public string? OtherCerfName2 { get; set; }

        public string? OtherCerfInstitute2 { get; set; }

        public double? OtherCerfMark2 { get; set; }

        public string? OtherCerfDuration2 { get; set; }

        public string? OtherCerfPassYear2 { get; set; }

        public IFormFile? OtherCerf2 { get; set; }

        public string? OtherCerfName3 { get; set; }

        public string? OtherCerfInstitute3 { get; set; }

        public double? OtherCerfMark3 { get; set; }

        public string? OtherCerfDuration3 { get; set; }

        public string? OtherCerfPassYear3 { get; set; }

        public IFormFile? OtherCerf3 { get; set; }

        public double? ExpYears { get; set; }

        public string? PreWorkCmp1 { get; set; }

        public string? PreWorkCmpSdt1 { get; set; }

        public string? PreWorkCmpEdt1 { get; set; }

        public IFormFile? PreWorkCmpDoc1 { get; set; }

        public string? PreWorkCmp2 { get; set; }

        public string? PreWorkCmpSdt2 { get; set; }

        public string? PreWorkCmpEdt2 { get; set; }

        public IFormFile? PreWorkCmpDoc2 { get; set; }

        public string? PreWorkCmp3 { get; set; }

        public string? PreWorkCmpSdt3 { get; set; }

        public string? PreWorkCmpEdt3 { get; set; }

        public IFormFile? PreWorkCmpDoc3 { get; set; }

        public string? PreWorkCmp4 { get; set; }

        public string? PreWorkCmpSdt4 { get; set; }

        public string? PreWorkCmpEdt4 { get; set; }

        public IFormFile? PreWorkCmpDoc4 { get; set; }

        public string? PreWorkCmp5 { get; set; }

        public string? PreWorkCmpSdt5 { get; set; }

        public string? PreWorkCmpEdt5 { get; set; }

        public IFormFile? PreWorkCmpDoc5 { get; set; }

        public string? WorkExBreak1 { get; set; }

        public string? WorkExBreakSdt1 { get; set; }

        public string? WorkExBreakEdt1 { get; set; }

        public string? WorkExBreak2 { get; set; }

        public string? WorkExBreakSdt2 { get; set; }

        public string? WorkExBreakEdt2 { get; set; }

        public string? WorkExBreak3 { get; set; }

        public string? WorkExBreakSdt3 { get; set; }

        public string? WorkExBreakEdt3 { get; set; }

        public string? WorkExBreak4 { get; set; }

        public string? WorkExBreakSdt4 { get; set; }

        public string? WorkExBreakEdt4 { get; set; }

        public double? PreWorkCmpCtc { get; set; }

        public string? PreWorkCmpEpfStatus { get; set; } 

        public string? PreWorkCmpEsiStatus { get; set; } 

        public string EmpDept { get; set; } = null!;

        public string EmpDesignation { get; set; } = null!;

        public string? EmpRole { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? EmpDoj { get; set; }

        public string? EmpOnboardCtg { get; set; } 

        public int? ContractorId { get; set; }

        public string SalaryPaidBy { get; set; } = null!;

        public string PaymentMode { get; set; } = null!;

        public string? SalAccEligibility { get; set; }

        public string? SalAccNo { get; set; }

        public string? SalBankName { get; set; }

        public string? SalBankIfsc { get; set; }

        public string? SalBankBranch { get; set; }

        public string? SalBenfCode { get; set; }

        public string EsiEpfEligibility { get; set; } = null!;

        public string? Form11Willingness { get; set; }

        public string? Form11Eligibility { get; set; }

        public string? Form11No { get; set; }

        public IFormFile? Form11Doc1 { get; set; }

        public IFormFile? Form11Doc2 { get; set; }

        public string? EsiJdt { get; set; }

        public string? EpfJdt { get; set; }

        public string? EsiNo { get; set; }

        public string? EsiNomineeName { get; set; }

        public string? EpfNo { get; set; }

        public string? EpfNomineeName { get; set; }

        public string? SalCriteria { get; set; } 

        public double? EmpSal { get; set; }

        public double? EmpWageShift { get; set; }

        public double? EmpWageHr { get; set; }

        public double? EmpWageDay { get; set; }

        public string? RoomRent { get; set; }

        public string? MessDeduction { get; set; }

        public string? OffEmail { get; set; }

        public string? OffMobile { get; set; }

        public string? AssetEligibility { get; set; }

        public string? Assets { get; set; }

        public string? OnboardVia { get; set; }

        public string? OnboardRefNo { get; set; }

        public string? OnboardRefName1 { get; set; }

        public string? OnboardRefName2 { get; set; }

        public IFormFile? Attachment1 { get; set; }

        public IFormFile? Attachment2 { get; set; }

        public IFormFile? Attachment3 { get; set; }

        public IFormFile? Attachment4 { get; set; }

        public IFormFile? Attachment5 { get; set; }

        public string? AadharVerf { get; set; } 

        public IFormFile? AadharVerfProof { get; set; }

        public string? CerfVerf { get; set; } 

        public string? OriginalDocSubmission { get; set; }

        public string? OriginalDocList { get; set; }

        public string? OriginalDocAck { get; set; }

        public string? OriginalDocAckNo { get; set; }

        public IFormFile? OriginalDocAckProof { get; set; }

        public string? EmpCurrentStatus { get; set; }

        public string? EmpDoe { get; set; }

        public string? OriginalDocHandover { get; set; }

        public string? OriginalDocAckBack { get; set; }

        public string? ReleavedReason { get; set; }

        public string? EmpRejoinDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int? ReportingTo { get; set; }

        public int? EmpOldCode { get; set; }

        public DateOnly? EmpCreateDate { get; set; }

        public IFormFile? TcCard { get; set; }

        public string? RetainOldEmpCode { get; set; }

        public virtual ICollection<AssetEmpM> AssetEmpMs { get; set; } = new List<AssetEmpM>();

        public virtual BloodGrpM? BloodGrpNavigation { get; set; }

        public virtual ContractorM? Contractor { get; set; }

        public virtual CategoryM EmpCtgNavigation { get; set; } = null!;

        public virtual EmpRejoinDetail? EmpRejoinDetail { get; set; }

        public virtual UserRightsM EmpRoleNavigation { get; set; } = null!;

        public virtual LegalM EsiEpfEligibilityNavigation { get; set; } = null!;

        public virtual QualificationM? HighQuali2Navigation { get; set; }

        public virtual QualificationM? HighQualiNavigation { get; set; }

        public virtual NationalityM? NationalityNavigation { get; set; }

        public virtual BankM? PerBnkNameNavigation { get; set; }

        public virtual BankM? SalBankNameNavigation { get; set; }

        public virtual CompanyM SalaryPaidByNavigation { get; set; } = null!;
    }
}
