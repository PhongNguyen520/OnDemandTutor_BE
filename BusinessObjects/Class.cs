using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Class
{
    public string ClassId { get; set; } = null!;

    public string ClassName { get; set; } = null !;

    //public int Quantity { get; set; }

    // Change: float => double
    public float Price { get; set; }

    public string Description { get; set; } = null!;

    // Change: có thể null
    public bool? Status { get; set; }

    public string TutorId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    //------- New ---------
    public DateTime CreateDay { get; set; }

    public double HourPerDay { get; set; }

    public int DayPerWeek { get; set; }

    public bool? IsApprove { get; set; }

    //---------------------

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;
}
