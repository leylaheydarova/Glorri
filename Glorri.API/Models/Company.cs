using Glorri.API.Models.BaseModels;

namespace Glorri.API.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Descriptino { get; set; }
        public DateOnly FoundationDate { get; set; }
        public int EmployeeCount { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
