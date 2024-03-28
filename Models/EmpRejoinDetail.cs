using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class EmpRejoinDetail
{
    public int RejoinId { get; set; }

    public int? OldEmpId { get; set; }

    public int? NewEmpId { get; set; }

    public int? EmpNewJoineeId { get; set; }

    public virtual EmployeeMasterData1? OldEmp { get; set; }
}
