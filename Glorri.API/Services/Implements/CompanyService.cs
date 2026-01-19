using Glorri.API.DTOs.Company;
using Glorri.API.Exceptions;
using Glorri.API.Extensions;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class CompanyService : ICompanyService
    {
        readonly IGenericRepository<Company> _repository;
        readonly IWebHostEnvironment _environment;
        readonly IHttpContextAccessor _accessor;

        public CompanyService(IGenericRepository<Company> repository, IWebHostEnvironment environment, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _environment = environment;
            _accessor = accessor;
        }

        public async Task<string> CreateAsync(CompanyCreateDto dto)
        {
            var company = new Company()
            {
                Name = dto.Name,
                Descriptino = dto.Description,
                EmployeeCount = dto.EmployeeCount,
                FoundationDate = dto.FoundationDate,
                IndustryId = dto.IndustryId,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            company.ImageName = dto.Image.UploadFile(_environment.WebRootPath, "assets/images/companies");
            company.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/assets/images/companies/{company.ImageName}";
            await _repository.AddAsync(company);
            await _repository.SaveAsync();
            return "Company was created successfully!";
        }

        public async Task<List<CompanyGetDto>> GetAllAsync()
        {
            var query = _repository.GetAllWhere(c => !c.IsDeleted, false, "Industry");
            var dtos = await query.Select(company => new CompanyGetDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Descriptino,
                EmployeeCount = company.EmployeeCount,
                FoundationDate = company.FoundationDate,
                ImageName = company.ImageName,
                ImageUrl = company.ImageUrl,
                IndustryId = company.IndustryId,
                IndustryName = company.Industry.Name
            }).ToListAsync();
            return dtos;
        }

        public async Task<string> RemoveAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id, true);
            if (company == null) throw new NotFoundException("company");
            var path = $"{_environment.WebRootPath}/assets/images/companies/{company.ImageName}";
            if (File.Exists(path)) File.Delete(path);
            _repository.Remove(company);
            await _repository.SaveAsync();
            return "Company was removed permanently!";
        }
    }
}
