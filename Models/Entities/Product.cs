using WebApiMongoDB.Models.MongoDB_g;

namespace WebApiMongoDB.Models.Entities
{
    [BsonCollection("product")]
    public class Product : Document
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}
