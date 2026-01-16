using Glorri.API.DTOs.Category;

namespace Glorri.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<string> CreateAsync(CategoryCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<string> UpdateAsync(int id, CategoryUpdateDto dto);
        Task<List<CategoryGetDto>> GetAllAsync();
    }
}
