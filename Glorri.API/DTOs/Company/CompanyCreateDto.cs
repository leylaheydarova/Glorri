namespace Glorri.API.DTOs.Company
{
    public record CompanyCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly FoundationDate { get; set; }
        public int EmployeeCount { get; set; }
        public int IndustryId { get; set; }
        public IFormFile Image { get; set; }
    }
}
