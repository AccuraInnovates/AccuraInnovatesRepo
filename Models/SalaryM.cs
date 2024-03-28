using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class SalaryM
{
    public int SalId { get; set; }

    public string SalCategory { get; set; } = null!;

    public string SalType { get; set; } = null!;

    public string? SalDesc { get; set; }

    public virtual CategoryM SalCategoryNavigation { get; set; } = null!;
}
