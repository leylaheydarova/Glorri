namespace Glorri.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptino { get; set; }
        public DateOnly FoundationDate { get; set; }
        public int EmployeeCount { get; set; }
        public string ImageName { get; set; }
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
