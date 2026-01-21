using Glorri.API.DTOs.Contact;

namespace Glorri.API.Services.Interfaces
{
    public interface IContactService
    {
        Task<string> CreateAsync(ContactCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<string> ToggleAsync(int id);
        Task<List<ContactGetDto>> GetAllAsync();
        Task<ContactGetDto> GetSingleAsync(int id);
        Task<string> UpdateAsync(int id, ContactUpdateDto dto);
    }
}
