using AdminBlazor.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBlazor.Authentication
{
    public class UserAccountService
    {
        //private List<SysUser> _users;

        private readonly ApplicationDbContext _dbContext;
        private readonly ProtectedSessionStorage _sessionStorage;

        public UserAccountService(ApplicationDbContext dbContext, ProtectedSessionStorage sessionStorage)
        {
            _dbContext = dbContext;
            _sessionStorage = sessionStorage;
        }

        //public UserAccountService()
        //{
        //    _users = new List<SysUser> {
        //        new SysUser{ UserName ="admin",Password="admin",Role=new SysRole{ Id = 1,RoleName = "Admin"} },
        //        new SysUser{ UserName ="user",Password="user",Role=new SysRole{ Id = 1,RoleName = "User"}},
        //    };
        //}

        public Task<SysUser> GetUserName(string userName)
        {
            return _dbContext.SysUser.Include(t=>t.Role).FirstOrDefaultAsync(x => x.UserName == userName);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool CheckPermission(string roleName, string permissionName)
        {
            var query = _dbContext.SysRole.Where(t => t.RoleName == roleName);
            var role = query.Include(t=>t.Permissions).FirstOrDefault();
            if (role == null) return false;

            return role.Permissions.Any(p => permissionName.StartsWith(p.PermissionName));
        }

        public async Task<List<string>> GetPermissions(string roleName) 
        {
            var role = await _dbContext.SysRole.Where(t => t.RoleName == roleName).Include(t => t.Permissions).FirstOrDefaultAsync();
            if (role == null) return null;

            return role.Permissions.Select(t=>t.PermissionName).ToList();
        }

        /// <summary>
        /// 获得当前用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<SysUser> GetCurrentUser()
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
            if(userSessionStorageResult.Success == true && userSessionStorageResult.Value != null)
            {
                var user = await _dbContext.SysUser.FirstOrDefaultAsync(t=>t.UserName == userSessionStorageResult.Value.UserName);

                return user;
            }

            return null;
        }
    }
}
