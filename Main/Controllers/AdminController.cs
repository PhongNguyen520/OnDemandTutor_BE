using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IRoleService iRoleService;
        private readonly IAccountService iAccountService;

        public AdminController(IRoleService _iRoleService, IAccountService _iAccountService)
        {
            iRoleService = _iRoleService;
            iAccountService = _iAccountService;
    }

        [HttpGet("getRole")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await iRoleService.GetRole();
            var listRoles = result.Select(_ => new
            {
                _.Id,
                _.Name
            });
            return Ok(listRoles);
}

        [HttpGet("getRoleBy/{id}")]
        public async Task<IActionResult> GetRoleById(String id)
        {
            var result = await iRoleService.GetRoleById(id);
            if (result != null) { return Ok(result); }
            return BadRequest("Cannot found");
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(String roleName)
        {
            var result = await iRoleService.CreateRole(roleName);
            return Ok(result);
        }

        [HttpPut("updateRole/{id}")]
        public async Task<IActionResult> UpdateRole(String roleName, String id)
        {
            var result = await iRoleService.UpdateRole(roleName, id);
            if (result > 0) return Ok();
            return BadRequest("Cannot update");
        }

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> DeleteRole(String roleId)
        {
            var result = await iRoleService.DeleteRole(roleId);
            return Ok(result);
        }

        [HttpGet("getUserRole")]
        public async Task<IActionResult> GetListUsers()
        {
            var result = await iRoleService.GetListUsers();
            return Ok(result);

        }
        [HttpGet("getUserRole/{userId}")]
        public async Task<IActionResult> GetUserRole(String userId)
        {
            var result = await iRoleService.GetUserRole(userId);
            if (result != null) return Ok(result);
            return BadRequest("Cannot found");
        }


        [HttpPost("addUserRole")]
        public async Task<IActionResult> AddRoleUser(List<string> roleNames, String userId)
        {
            var result = await iRoleService.AddRoleUser(roleNames, userId);
            return Ok(result);
        }

        [HttpPost("changeUserStatus")]
        public async Task<IActionResult> EnalbleUser(String userId)
        {
            return Ok(await iAccountService.EnalbleUser(userId));
        }

    }
}

