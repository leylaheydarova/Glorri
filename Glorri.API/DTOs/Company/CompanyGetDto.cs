namespace Glorri.API.DTOs.Company
{
    public record CompanyGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly FoundationDate { get; set; }
        public int EmployeeCount { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
