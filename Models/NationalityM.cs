using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class NationalityM
{
    public int NationalityId { get; set; }

    public string Nationality { get; set; } = null!;

    public string? NationalityDesc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();
}
