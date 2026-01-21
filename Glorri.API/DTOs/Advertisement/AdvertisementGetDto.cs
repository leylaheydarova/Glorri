namespace Glorri.API.DTOs.Advertisement
{
    public class AdvertisementGetDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublishDate { get; set; }
        public string VacancyType { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
