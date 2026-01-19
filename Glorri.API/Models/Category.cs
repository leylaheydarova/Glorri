using Glorri.API.Models.BaseModels;

namespace Glorri.API.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
