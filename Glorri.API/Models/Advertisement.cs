namespace Glorri.API.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int VacancyType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
