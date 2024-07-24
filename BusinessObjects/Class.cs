using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Class
{
    public string ClassId { get; set; } = null!;
    public string ClassName { get; set; } = null!;

    public string ClassName { get; set; } = null !;

    public float Price { get; set; }

    public string Description { get; set; } = null!;

    public bool? Status { get; set; }

    public string TutorId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public DateTime CreateDay { get; set; }

    public bool? IsApprove { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;

    public virtual ICollection<ClassCalender> ClassCalenders { get; set; } = new List<ClassCalender>();
    public DateTime DayStart { get; set; }

    public DateTime DayEnd { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    public bool? IsCancel { get; set; }
    public DateTime? CancelDay { get; set; }
    public string UrlClass { get; set; } = string.Empty;
}
