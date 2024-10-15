using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface IRoleService
    {
        Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(Role role, IEnumerable<string> claims);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(Role role);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName);
        Task<Role?> GetRoleByIdAsync(string roleId);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<Role?> GetRoleLoadRelatedAsync(string roleName);
        Task<List<Role>> GetRolesLoadRelatedAsync(int page, int pageSize);
        Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(Role role, IEnumerable<string>? claims);
    }
}
