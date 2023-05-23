using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPI.ViewModels.AccountRoles
{
    public class AccountRoleVM
    {
        public Guid? Guid { get; set; }
        public Guid AccountGuid { get; set; }
        
        public Guid RoleGuid { get; set; }
    }
}
