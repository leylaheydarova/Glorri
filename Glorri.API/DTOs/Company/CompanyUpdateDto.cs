namespace Glorri.API.DTOs.Company
{
    public record CompanyUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? FoundationDate { get; set; }
        public int? EmployeeCount { get; set; }
        public IFormFile? Image { get; set; }
    }
}
