using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class LegalM
{
    public int LegId { get; set; }

    public string LegName { get; set; } = null!;

    public string? LegDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();
}
