using System.Collections.Generic;

namespace AdminBlazor.Authentication
{
    public static class Permissions
    {
        public static List<Permission> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission() {
                    Key = PermissionNames.Role,
                    IsMenu= true,
                    Name = "角色管理",
                    Path = "/role/index",
                    Icon = "team",
                    Children = new List<Permission>()
                    {
                        new Permission{
                            Key = PermissionNames.RoleCreate,
                            Name = "增加",
                        },
                        new Permission{
                            Key = PermissionNames.RoleDelete,
                            Name = "删除",
                        },
                        new Permission{
                            Key = PermissionNames.RoleUpdate,
                            Name = "修改",
                        },
                        new Permission{
                            Key = PermissionNames.RoleRead,
                            Name = "查询",
                        },
                    }
                },
                new Permission() {
                    Key = PermissionNames.User,
                    IsMenu= true,
                    Name = "用户管理",
                    Path = "/user/index",
                    Icon = "user",
                    Children = new List<Permission>()
                    {
                        new Permission{
                            Key = PermissionNames.UserCreate,
                            Name = "增加",
                        },
                        new Permission{
                            Key = PermissionNames.UserDelete,
                            Name = "删除",
                        },
                        new Permission{
                            Key = PermissionNames.UserUpdate,
                            Name = "修改",
                        },
                        new Permission{
                            Key = PermissionNames.UserRead,
                            Name = "查询",
                        },
                    }
                }
            };
        }

        public static List<string> GetPermissionNames(List<Permission> permissions) 
        {
            var list = new List<string>();

            foreach (var item in permissions)
            {
                list.Add(item.Key);

                if (item.Children != null) {
                    list.AddRange(GetPermissionNames(item.Children));
                }
            }

            return list;
        }
    }

    public class Permission
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public string Path { get; set; }

        public string Icon { get; set; }

        public bool IsMenu { get; set; }

        public List<Permission> Children { get; set; }
    }
}
