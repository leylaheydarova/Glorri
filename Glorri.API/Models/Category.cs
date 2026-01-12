namespace Glorri.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
