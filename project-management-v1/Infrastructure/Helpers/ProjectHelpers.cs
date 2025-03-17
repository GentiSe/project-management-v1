using project_management_v1.Application.Domain.Constants;
using project_management_v1.Application.Domain.Enums;

namespace project_management_v1.Infrastructure.Helpers
{
    public static class ProjectHelpers
    {
        public static AccesType GetAccesTypeForRole(string role)
        {
            var accessType = role switch
            {
                UserRoles.Admin => AccesType.Write,
                UserRoles.Analyst => AccesType.Read,
                UserRoles.Basic => AccesType.Read,
                UserRoles.CategoryManager => AccesType.Read,
                _ => AccesType.Read,
            };
            return accessType;
        }
    }
}
