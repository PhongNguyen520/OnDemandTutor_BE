using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class FindTutorForm
{
    public string FormId { get; set; } = null!;

    public DateTime CreateDay { get; set; }

    // ----Chỉnh sửa-----

    public string Tittle { get; set; } = string.Empty;

    public double MinHourlyRate { get; set; }

    public double MaxHourlyRate { get; set; }

    public bool IsActive { get; set; }

    //-------------------
    public string SubjectName { get; set; } = null!;

    public bool TutorGender { get; set; }

    public string TypeOfDegree { get; set; } = null!;

    public string? DescribeTutor { get; set; }

    public bool Status { get; set; }

    public string StudentId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
