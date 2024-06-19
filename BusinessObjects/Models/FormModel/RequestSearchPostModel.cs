using BusinessObjects.Models.TutorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FormModel
{
    public class RequestSearchPostModel
    {
        public string Search { get; set; }
        public double? MaxRate { get; set; } = 150;
        public double? MinRate { get; set; } = 10;
        public string? GradeId { get; set; }
        public bool? Gender { get; set; }
        public string? TypeOfDegree { get; set; }
        public int pageIndex { get; set; }
        public SortContent SortContent { get; set; }
    }
        public class SortContent
        {
            public SortPostByEnum sortPostBy { get; set; }
            public SortPostTypeEnum sortPostType { get; set; }
        }

        public enum SortPostByEnum
    {
            HourlyRate = 1,
            CreateDay = 2,
        }
        public enum SortPostTypeEnum
    {
            Ascending = 1,
            Descending = 2,
        }

    
}
