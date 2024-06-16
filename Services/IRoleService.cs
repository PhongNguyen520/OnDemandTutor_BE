using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoleService
    {
        //public bool AddRole(Role role);

        //public bool DelRoles(int id);

        //public List<Role> GetRoles();

        //public bool UpdateAccounts(Role role);
        Task<List<IdentityRole>> GetRole();
        Task<IdentityRole> GetRoleById(String id);
        Task<IdentityResult> CreateRole(String roleName);
        Task<int> UpdateRole(String roleName, String id);
        Task<IdentityResult> DeleteRole(String roleId);
        Task<String[]> GetUserRole(string userId);
        Task<IdentityResult> AddRoleUser(List<string> roleNames, String userId);
        Task<List<UserRolesVM>> GetListUsers();
    }
}
