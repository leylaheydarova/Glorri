namespace Glorri.API.DTOs.Contact
{
    public record ContactCreateDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
    }
}
