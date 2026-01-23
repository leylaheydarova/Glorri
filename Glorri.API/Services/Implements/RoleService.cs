using Glorri.API.DTOs.Role;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> CreateAsync(RoleCreateDto dto)
        {
            var role = new Role()
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _roleManager.CreateAsync(role);
            return $"{role.Name} role was created succesfully!";
        }

        public Task<List<RoleGetDto>> GetAllAsync()
        {
            var query = _roleManager.Roles;
            var dtos = query.Select(role => new RoleGetDto()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            }).ToListAsync();
            return dtos;
        }

        public async Task<RoleGetDto> GetSingleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) throw new NotFoundException("role");
            var dto = new RoleGetDto()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name
            };
            return dto;
        }

        public async Task<string> RemoveAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) throw new NotFoundException("role");
            var roleName = role.Name;
            await _roleManager.DeleteAsync(role);
            return $"{roleName} was removed permanently!";
        }
    }
}
