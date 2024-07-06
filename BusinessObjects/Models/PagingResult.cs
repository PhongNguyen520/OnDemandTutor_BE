using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class PagingResult<T>
    {
        public List<T> ListResult { get; set; } = new List<T>();
        public int LimitPage { get; set; }
    }
}
