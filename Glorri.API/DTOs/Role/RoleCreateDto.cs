namespace Glorri.API.DTOs.Role
{
    public record RoleCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
