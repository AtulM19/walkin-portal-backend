using System;

namespace api_backend.Models;

public partial class WalkinInterview
{
    public int WalkinId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Location { get; set; }

    public string? WalkinTitle { get; set; }
    public string? Internship { get; set; }

    public string? GeneralInstruction { get; set; }

    public string? ExamInstruction { get; set; }

    public string? MinSystemRequirement { get; set; }

    public string? Process { get; set; }

    public DateTime? DateModified { get; set; }

    public DateTime? DateCreated { get; set; }

}
