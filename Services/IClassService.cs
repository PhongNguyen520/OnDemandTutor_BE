using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClassService
    {
        public bool AddClass(Class @class);

        public bool DelClasses(int id);

        public List<Class> GetClasses();

        public bool UpdateClasses(Class @class);

        public Form? CheckTypeForm(string id);

        Task<ReturnBalance> PaymentTutor(string userId);

        Task<List<ListClassVMPhuc>> GetClassByDay();

        Task<List<ListClassVMPhucMonthYear>> GetClassByMonth();
    }
}
