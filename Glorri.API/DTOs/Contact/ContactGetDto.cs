namespace Glorri.API.DTOs.Contact
{
    public class ContactGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
    }
}
