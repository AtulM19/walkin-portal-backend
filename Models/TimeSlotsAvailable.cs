using System;

namespace api_backend.Models;

public partial class TimeSlotsAvailable
{
    public int TimeslotId { get; set; }

    public int WalkinId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateTime DateModified { get; set; }

    public DateTime DateCreated { get; set; }

}
