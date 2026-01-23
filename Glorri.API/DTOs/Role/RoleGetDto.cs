namespace Glorri.API.DTOs.Role
{
    public record RoleGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
