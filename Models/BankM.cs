using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class BankM
{
    public int BankId { get; set; }

    public string BankName { get; set; } = null!;

    public string? Desc { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1PerBnkNameNavigations { get; set; } = new List<EmployeeMasterData1>();

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1SalBankNameNavigations { get; set; } = new List<EmployeeMasterData1>();
}
