using BusinessObjects.Entities;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Repositories.Interfaces;

namespace Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(IRoleRepository roleRepository, RoleManager<Role> roleManager)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        public async Task<Role?> GetRoleByIdAsync(string roleId)
        {
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _roleRepository.GetByNameAsync(roleName);
        }

        public async Task<Role?> GetRoleLoadRelatedAsync(string roleName)
        {
            return await _roleRepository.GetLoadRelatedAsync(roleName);
        }

        public async Task<List<Role>> GetRolesLoadRelatedAsync(int page, int pageSize)
        {
            return await _roleRepository.GetAllLoadRelatedAsync(page, pageSize);
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(Role role, IEnumerable<string> claims)
        {
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            role = await _roleRepository.GetByNameAsync(role.Name!);

            foreach (var claim in claims.Distinct())
            {
                result = await _roleManager.AddClaimAsync(role, new Claim(claim, claim));

                if (!result.Succeeded)
                {
                    await DeleteRoleAsync(role);
                    return (false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(Role role, IEnumerable<string>? claims)
        {
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            if (claims != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var roleClaimValues = roleClaims.Select(c => c.Value).ToArray();

                var claimsToRemove = roleClaimValues.Except(claims).ToArray();
                var claimsToAdd = claims.Except(roleClaimValues).Distinct().ToArray();

                foreach (var claim in claimsToRemove)
                {
                    result = await _roleManager.RemoveClaimAsync(role, roleClaims.First(c => c.Value == claim));
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }

                foreach (var claim in claimsToAdd)
                {
                    result = await _roleManager.AddClaimAsync(role, new Claim(claim, claim));
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName)
        {
            var role = await _roleRepository.GetByNameAsync(roleName);

            if (role != null)
                return await DeleteRoleAsync(role);

            return (true, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
