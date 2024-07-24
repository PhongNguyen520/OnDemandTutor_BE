﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class HistoryTutorApply
    {
        public string HistoryTutorApplyId { get; set; }
        public DateTime DateInterView { get; set; }
        public string Content { get; set; }
        public string TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }
        public string Email { get; set; }
    }
}
