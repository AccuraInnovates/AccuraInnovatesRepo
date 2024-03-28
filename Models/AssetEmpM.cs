using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class AssetEmpM
{
    public int AssetEmpId { get; set; }

    public int? EmpId { get; set; }

    public string? AssetName { get; set; }

    public virtual AssetsListM? AssetNameNavigation { get; set; }

    public virtual EmployeeMasterData1? Emp { get; set; }
}
