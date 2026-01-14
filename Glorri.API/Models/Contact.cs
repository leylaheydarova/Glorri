using Glorri.API.Models.BaseModels;

namespace Glorri.API.Models
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
