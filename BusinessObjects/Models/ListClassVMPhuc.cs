using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ListClassVMPhuc
    {
        public DateTime Date {  get; set; }
        public List<ClassVMPhuc> ClassVMPhuc { get; set; }
    }

    public class ListClassVMPhucMonthYear
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ClassVMPhuc> ClassVMPhuc { get; set; }
    }
}
