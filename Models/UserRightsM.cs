using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class UserRightsM
{
    public int EmpRoleId { get; set; }

    public string EmpRole { get; set; } = null!;

    public string? RoleCreate { get; set; }

    public string? RoleEdit { get; set; }

    public string? RoleDelete { get; set; }

    public string? RoleView { get; set; }

    public virtual ICollection<EmployeeMasterData1> EmployeeMasterData1s { get; set; } = new List<EmployeeMasterData1>();

    public virtual ICollection<UserLoginM> UserLoginMs { get; set; } = new List<UserLoginM>();
}
