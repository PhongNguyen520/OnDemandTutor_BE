﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class TutorAd
{
    public string AdsId { get; set; } = null!;

    public DateTime CreateDay { get; set; }

    public string Video { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string TutorId { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;

    public string? Title { get; set; }

    public bool? IsActived { get; set; }

    public string? RejectReason { get; set; }
}
