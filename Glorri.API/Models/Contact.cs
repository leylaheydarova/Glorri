namespace Glorri.API.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
