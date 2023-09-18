using System;

namespace api_backend.Models;

public partial class UserApplied
{
    public int UserAppliedId { get; set; }

    public int UserId { get; set; }

    public int WalkingId { get; set; }

    public int TimeslotId { get; set; }

    public DateTime DateModified { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual TimeSlotsAvailable Timeslot { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual WalkinInterview Walking { get; set; } = null!;
}
