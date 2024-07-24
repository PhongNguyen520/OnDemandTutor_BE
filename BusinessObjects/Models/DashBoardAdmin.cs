using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class DashBoardAdmin
    {
        public string Title { get; set; } = string.Empty;
        public float? Value { get; set; } = 0;
        public List<float?> Data { get; set; } = new List<float?>();
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
    }
}
