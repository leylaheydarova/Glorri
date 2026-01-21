using Glorri.API.DTOs.Advertisement;

namespace Glorri.API.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Task<string> CreateAsync(AdvertisementCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<string> ToggleAsync(int id);
        Task<string> UpdateAsync(int id, AdvertisementUpdateDto dto);
        Task<List<AdvertisementGetDto>> GetAllAsync();
        Task<AdvertisementGetDto> GetSingleAsync(int id);
    }
}
