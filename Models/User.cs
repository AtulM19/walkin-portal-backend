
namespace api_backend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Firstname { get; set; } 

    public string Lastname { get; set; } 

    public string Email { get; set; } 

    public string PhoneNumber { get; set; }

    public string? PortfolioUrl { get; set; }

    public string? JobRolesSelected { get; set; }

    public string? Referral { get; set; }

    public bool? JobRelatedUpdates { get; set; }

    public int Percentage { get; set; }

    public int YearOfPassing { get; set; }

    public string Qualification { get; set; } 

    public string Stream { get; set; } 

    public string College { get; set; } 

    public string? CollegeOthers { get; set; }

    public string CollegeLocation { get; set; } 

    public string ApplicantType { get; set; } 

    public int YearsOfExperience { get; set; }

    public int CurrentCtc { get; set; }

    public int ExpectedCtc { get; set; }

    public bool NoticePeriod { get; set; }

    public DateOnly NoticePeriodEndDate { get; set; }

    public int NoticePeriodDuration { get; set; }

    public bool AppearedForZeus { get; set; }

    public string? AppearedForRoleInZeus { get; set; }

    public string? Resume { get; set; }

    public string? UserExpertiseIn { get; set; }

    public string? UserFamiliarIn { get; set; }

    public string? UserOtherExpertiseIn { get; set; }

    public string? UserOtherFamiliarIn { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
    public string Photo { get; set; }
}
