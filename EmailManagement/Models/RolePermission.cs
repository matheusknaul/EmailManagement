using EmailManagement.Models;

namespace EmailManagement.Models
{
    public class RolePermission
    {
        public int role_id { get; set; }
        public Role Role { get; set; }
        public int permission_id { get; set; }
        public Permission Permission { get; set; }
    }

    
}
