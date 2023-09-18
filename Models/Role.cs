using System;

namespace api_backend.Models;

public partial class Role
{
    public int RolesId { get; set; }

    public string? Title { get; set; }

    public int? Package { get; set; }

    public string? RoleDescription { get; set; }

    public string? RoleRequirements { get; set; }

    public DateTime? DateModified { get; set; }

    public DateTime? DateCreated { get; set; }
    public string? RoleLogo { get; set; }
}
