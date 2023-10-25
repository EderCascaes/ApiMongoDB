using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Controllers.Dtos;
using WebApiMongoDB.Models.Views;
using WebApiMongoDB.Service.MongoDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoDBService<ProductDto> _mongoDBService;

        public ProductController(IMongoDBService<ProductDto> _mongoDBService)
        {
            this._mongoDBService = _mongoDBService;
        }

        // POST api/<PersonController>
        [HttpPost]
        [Route("Product")]
        public async Task AddOneProduct([FromBody] ProductDto productdto)
        {
            await _mongoDBService.AddOne(productdto);
        }

        // GEt api/<PersonController>/5
        [HttpGet]
        [Route("FindById/{id}")]
        public async Task<object> Get(string id)
        {

            return await _mongoDBService.FindByIdAsync(id);
        }



        // GEt api/<PersonController>/5
        [HttpGet]
        [Route("GetCollection")]
        public async Task<List<object>> GetCollectionPersons()
        {
            return await _mongoDBService.GetCollection();
        }

        /* // POST api/<PersonController>
         [HttpPost]
         [Route("AddListOfPersons")]
         public async Task AddManyPerson([FromBody] List<Person> listPerson)
         {
             await _productService.AddManyPerson(listPerson);
         }




         // GEt api/<PersonController>/5
         [HttpGet]
         [Route("GetCollectionFilter")]
         public async Task<List<PersonViewModel>> GetCollectionFilterPersons(string filterExpression)
         {

             return await _productService.GetCollectionFilterPersons(filterExpression);
         }



         // DELETE api/<PersonController>/5sdfsjk56489sfs489
         [HttpDelete]
         [Route("DeletePerson")]
         public async Task<PersonViewModel> DeleteOnePerson(string id)
         {
             return await _productService.DeleteOnePerson(id);
         }


         [HttpDelete]
         [Route("DeleteManyPerson")]
         public async Task<bool> DeleteManyPerson()
         {
             return await _productService.DeleteManyPerson();
         }


         // PUT api/<PersonController>/5
         [HttpPut]
         [Route("PutPerson")]
         public async Task<PersonViewModel> ReplaceOneAsync([FromBody] PersonDto personDto)
         {

             return await _personService.ReplaceOnePersonAsync(personDto);
         }*/

    }
}
