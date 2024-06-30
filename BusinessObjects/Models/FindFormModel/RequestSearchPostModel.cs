using BusinessObjects.Models.TutorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FindFormModel
{
    public class RequestSearchPostModel
    {
        public string? Search { get; set; }
        public double? HourlyRate { get; set; }
        public string? GradeId { get; set; }
        public bool? Gender { get; set; }
        public string? TypeOfDegree { get; set; }
        public int pageIndex { get; set; }
        public SortContent? SortContent { get; set; }
    }
        public class SortContent
        {
            public SortPostByEnum sortPostBy { get; set; }
            public SortPostTypeEnum sortPostType { get; set; }
        }

        public enum SortPostByEnum
    {
            CreateDay,
        }
        public enum SortPostTypeEnum
    {
            Ascending = 1,
            Descending = 2,
        }

    
}
