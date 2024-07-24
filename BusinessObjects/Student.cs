﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string? SchoolName { get; set; }

    public string? Address { get; set; }

    public int? Age { get; set; }

    public bool IsParent { get; set; }

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<FindTutorForm> FindTutorForms { get; set; } = new List<FindTutorForm>();

    public virtual ICollection<RequestTutorForm> RequestTutorForms { get; set; } = new List<RequestTutorForm>();
}
