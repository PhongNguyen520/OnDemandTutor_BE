using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository iRoleRepository = null;

        public RoleService(IRoleRepository roleRepository)
        {
            iRoleRepository = roleRepository;
        }
        //public bool AddRole(Role role)
        //{
        //    return iRoleRepository.AddRole(role);
        //}

        //public bool DelRoles(int id)
        //{
        //    return iRoleRepository.DelRoles(id);
        //}

        //public List<Role> GetRoles()
        //{
        //    return iRoleRepository.GetRoles();
        //}

        //public bool UpdateAccounts(Role role)
        //{
        //    return iRoleRepository.UpdateAccounts(role);
        //}
        public async Task<List<IdentityRole>> GetRole()
        {
            return await iRoleRepository.GetRole();
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await iRoleRepository.GetRoleById(id);
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await iRoleRepository.CreateRole(roleName);
        }

        public async Task<int> UpdateRole(string roleName, string id)
        {
            return await iRoleRepository.UpdateRole(roleName, id);
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            return await iRoleRepository.DeleteRole(roleId);
        }

        public async Task<String[]> GetUserRole(string userId)
        {
            return await iRoleRepository.GetUserRole(userId);
        }

        public async Task<IdentityResult> AddRoleUser(List<string> roleNames, string userId)
        {
            return await iRoleRepository.AddRoleUser(roleNames, userId);
        }

        public async Task<List<UserRolesVM>> GetListUsers()
        {
            return await iRoleRepository.GetListUsers();
        }
    }
}
