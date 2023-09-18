using System;

namespace api_backend.Models;

public partial class WalkinRole
{
    public int WalkinRolesId { get; set; }

    public int RolesId { get; set; }

    public int WalkinId { get; set; }

    public DateTime DateModified { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Role Roles { get; set; } = null!;
}
