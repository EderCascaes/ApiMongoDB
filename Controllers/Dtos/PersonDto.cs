using MongoDB.Bson;

namespace WebApiMongoDB.Controllers.Dtos
{
    public class PersonDto
    {
        public string? PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }


}
