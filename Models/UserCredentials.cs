using System;

namespace api_backend.Models;

public partial class UserCredentials
{
    public int UserCredentialId { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime DateCreated { get; set; }
    
    public DateTime DateModified { get; set; }
}
