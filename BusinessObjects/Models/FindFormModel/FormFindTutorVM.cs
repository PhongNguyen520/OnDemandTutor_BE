﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FindFormModel
{
    public class FormFindTutorVM
    {
        public required string FormId { get; set; }
        public string CreateDay { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Avatar {  get; set; }
        public string? Title { get; set; }
        public double? MinHourlyRate { get; set; }
        public double? MaxHourlyRate { get; set; }
        public string SubjectName { get; set; } = string.Empty ;
        public string? Description { get; set; }
        public bool? TutorGender { get; set; }
        public string SubjectId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
    }

    public class TutorApplyVM
    {
        public string TutorId { get; set; } = string.Empty;
        public string TutorName { get; set; } = string.Empty;
        public string TutorAvatar { get; set; } = string.Empty;
        public DateTime DayApply { get; set; }
    }
}
