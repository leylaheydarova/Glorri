using Glorri.API.Enums;

namespace Glorri.API.DTOs.Advertisement
{
    public class AdvertisementUpdateDto
    {
        public string? Description { get; set; }
        public string? Requirement { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public VacancyType? VacancyType { get; set; }
    }
}
