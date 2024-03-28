using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class ContractorM
{
    public int ContractorId { get; set; }

    public string ContractorName { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string CommAddressLine1 { get; set; } = null!;

    public string? CommAddressLine2 { get; set; }

    public string CommAddressCity { get; set; } = null!;

    public string CommAddressState { get; set; } = null!;

    public string CommAddressZipcode { get; set; } = null!;

    public string PermAddress { get; set; } = null!;

    public string? PermAddressLine2 { get; set; }

    public string PermAddressCity { get; set; } = null!;

    public string PermAddressState { get; set; } = null!;

    public string PermAddressZipcode { get; set; } = null!;

    public string AadharNo { get; set; } = null!;

    public string ContractorLicenceNo { get; set; } = null!;

    public string PanNo { get; set; } = null!;

    public string? GstNo { get; set; }

    public string TdsApplicable { get; set; } = null!;

    public int? TdsPers { get; set; }

    public int? Commision { get; set; }

    public double? CommisionPers { get; set; }

    public double? FoodCast { get; set; }

    public string? AccomEligibility { get; set; }

    public string? CookEligibility { get; set; }

    public string? UniformEligibility { get; set; }

    public string? ShoesEligibility { get; set; }

    public double? EmpWage { get; set; }

    public string ContractorStatus { get; set; } = null!;

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly EffectiveTo { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();
}
