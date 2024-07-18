using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class RoleDTO
    {
        public string Id;
        public string Name;
    }

    public class ChangRoleVM
    {
        public string UserId { get; set; }
        public List<string> ListRoleName { get; set; }
    }
}
