using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class AssetsListM
{
    public int AssetId { get; set; }

    public string AssetName { get; set; } = null!;

    public string? AssetDesc { get; set; }

    public virtual ICollection<AssetEmpM> AssetEmpMs { get; set; } = new List<AssetEmpM>();
}
