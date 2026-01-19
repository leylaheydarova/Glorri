using Glorri.API.Models.BaseModels;

namespace Glorri.API.Models
{
    public class Industry : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Company> Companies { get; set; } = new List<Company>();
    }
}
