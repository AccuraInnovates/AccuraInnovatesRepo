using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class CompanyM
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? CompanyDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();
}
