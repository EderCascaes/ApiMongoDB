using WebApiMongoDB.Models.MongoDB_g;

namespace WebApiMongoDB.Models.Entities
{
    [BsonCollection("people")]
    public class Person : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
