using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Controllers.Dtos;
using WebApiMongoDB.Service.MongoDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMongoDBService<PersonDto> mongoDBService;

        public PersonController(IMongoDBService<PersonDto> mongoDBService)
        {
            this.mongoDBService = mongoDBService;
        }

        // POST api/<PersonController>
        [HttpPost]
        [Route("Person")]
        public async Task AddOnePerson([FromBody] PersonDto persondto)
        {
            await mongoDBService.AddOne(persondto);
        }

        // POST api/<PersonController>
        [HttpPost]
        [Route("AddListOfPersons")]
        public async Task AddManyPerson([FromBody] List<PersonDto> listPerson)
        {
            await mongoDBService.AddMany(listPerson);
        }

        // GEt api/<PersonController>/5
        [HttpGet]
        [Route("FindById/{id}")]
        public async Task<object> Get(string id)
        {          

            return await mongoDBService.FindByIdAsync(id);
        }


        // GEt api/<PersonController>/5
        [HttpGet]
        [Route("GetCollectionFilter")]
        public async Task<List<object>> GetCollectionFilterPersons(string filterExpression)
        {

            return await mongoDBService.GetCollectionFilter(filterExpression);
        }


        // GEt api/<PersonController>/5
        [HttpGet]
        [Route("GetCollection")]
        public async Task<List<object>> GetCollectionPersons()
        {
            return await mongoDBService.GetCollection();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete]
        [Route("DeletePerson")]
        public async Task<object> DeleteOnePerson(string id)
        {
            return await mongoDBService.DeleteOne(id);
        }


        [HttpDelete]
        [Route("DeleteManyPerson")]
        public async Task<bool> DeleteManyPerson()
        {
            return await mongoDBService.DeleteMany();
        }


        // PUT api/<PersonController>/5
        [HttpPut]
        [Route("PutPerson")]
        public async Task<object> ReplaceOneAsync([FromBody] PersonDto personDto)
        {

            return await mongoDBService.ReplaceOneAsync(personDto);
        }

       
    }
}
