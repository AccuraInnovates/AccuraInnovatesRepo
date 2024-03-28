using System;
using System.Collections.Generic;

namespace Accura_Innovatives.Models;

public partial class UserLoginM
{
    public string LoginId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string EmpRole { get; set; } = null!;

    public string? EmpState { get; set; }

    public string? EmpGroup { get; set; }

    public string? EmpSubgroup { get; set; }

    public string? OldPassword1 { get; set; }

    public string? OldPassword2 { get; set; }

    public string? OldPassword3 { get; set; }

    public DateOnly LastLogged { get; set; }

    public virtual UserRightsM EmpRoleNavigation { get; set; } = null!;
}
