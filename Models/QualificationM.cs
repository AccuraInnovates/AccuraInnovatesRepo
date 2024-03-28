using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class QualificationM
{
    public int QualiId { get; set; }

    public string QualiName { get; set; } = null!;

    public string? QualiDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1HighQuali2Navigations { get; set; } = new List<EmployeeMasterData1>();

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1HighQualiNavigations { get; set; } = new List<EmployeeMasterData1>();
}
