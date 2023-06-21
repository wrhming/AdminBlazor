using System.Collections.Generic;

namespace AdminBlazor.Authentication
{
    public class UserSession
    {
        public string UserName { get; set;}

        public string Role { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Permissions { get; set; }
    }
}
