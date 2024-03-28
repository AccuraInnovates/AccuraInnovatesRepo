using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class BloodGrpM
{
    public int BloodGrpId { get; set; }

    public string BloodGrp { get; set; } = null!;

    public string? BloodGrpDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();
}
