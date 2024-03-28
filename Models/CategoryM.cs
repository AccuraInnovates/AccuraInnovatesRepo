using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class CategoryM
{
    public string CtgCode { get; set; } = null!;

    public string CtgDetails { get; set; } = null!;

    public string? CtgDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();

    public virtual ICollection<SalaryM> SalaryMs { get; set; } = new List<SalaryM>();
}
