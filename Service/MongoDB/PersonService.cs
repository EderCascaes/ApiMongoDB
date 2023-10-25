using MongoDB.Bson;
using WebApiMongoDB.Controllers.Dtos;
using WebApiMongoDB.Models.Entities;
using WebApiMongoDB.Models.Views;
using WebApiMongoDB.Repository.MongoDB_g;

namespace WebApiMongoDB.Service.MongoDB
{
    public class PersonService : IMongoDBService<PersonDto>
    {

        private readonly IMongoRepository<Person> _mongoRepository;

        public PersonService(IMongoRepository<Person> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }


        public async Task<object> AddOne(PersonDto personDto)
        {
            try
            {
                if (personDto != null)
                {
                    var person = new Person()
                    {
                        FirstName = personDto.FirstName,
                        LastName = personDto.LastName,
                        BirthDate = personDto.BirthDate
                    };

                    _mongoRepository.InsertOne(person);
                    
                    return person;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Error when inserting Person :" + ex.Message);
            }


            return default;
        }



        public async  Task<bool> AddMany(List<PersonDto> listObj)
        {
            var listPerson = new List<Person>();

            try
            {

                if (listObj.Count > 0)
                    foreach (var personDto in listObj) 
                    {
                        listPerson.Add(

                            new Person()
                            {
                                Id = new ObjectId(personDto.PersonId),
                                FirstName = personDto.FirstName,
                                LastName = personDto.LastName,
                                BirthDate = personDto.BirthDate
                            }
                        );               
                
                    }

                    _mongoRepository.InsertMany(listPerson);

            }
            catch (Exception ex)
            {

                throw new Exception("Error when inserting list of People :" + ex.Message);
            }


            return  true;
        }


    

        public async Task<object> FindByIdAsync(string id)
        {

            var person = new Person();
            var personViewModel = new PersonViewModel();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    person = await _mongoRepository.FindByIdAsync(id);
                }
                else if (person == null) { return null; }

                return new PersonViewModel()
                {
                    PersonId = person.Id.ToString(),
                    FirstName = person.FirstName,
                    BirthDate = person.BirthDate,
                    LastName = person.LastName

                };
            }
            catch (Exception ex)
            {

                throw new Exception("Error when find Person :" + ex.Message);
            }

        }

        public async Task<List<object>> GetCollectionFilter(string filterExpression)
        {
            var listPerson = new List<Person>();
            try
            {

                if (!string.IsNullOrEmpty(filterExpression))
                    listPerson = _mongoRepository.FilterBy(x => x.FirstName.Contains(filterExpression) || x.LastName.Contains(filterExpression)).ToList();


                if (listPerson.Count > 0)
                {
                    if (listPerson.Count > 0)
                    {
                        return await PersonInsertList(listPerson);
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error when find  Person  for filter :{filterExpression},  " + ex.Message);
            }


            return default;
        }


        public async Task<List<object>> GetCollection()
        {
            var listPerson = new List<Person>();
            try
            {

                listPerson = _mongoRepository.FilterBy(x => x.Id != null).ToList();

                if (listPerson.Count > 0)
                {
                    return await PersonInsertList(listPerson);
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error when find  list Person  :  " + ex.Message);
            }
            return default;
        }



        public async Task<object> DeleteOne(string id)
        {
            try
            {
                var personDelete = await _mongoRepository.FindByIdAsync(id);

                if (personDelete != null)
                {
                    await _mongoRepository.DeleteByIdAsync(id);

                    return new PersonViewModel()
                    {
                        PersonId = personDelete.Id.ToString(),
                        FirstName = personDelete.FirstName,
                        BirthDate = personDelete.BirthDate,
                        LastName = personDelete.LastName

                    };

                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw new Exception($"Error when delete  Person  :  " + ex.Message);
            }
        }



        public async Task<bool> DeleteMany()
        {
            try
            {

                await _mongoRepository.DeleteManyAsync(x => x.Id != null);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error when delete all Persons  :  " + ex.Message);
            }
        }


        public async Task<object> ReplaceOneAsync(PersonDto personDto)
        {
            try
            {
                var person = new Person()
                {
                    Id = new ObjectId(personDto.PersonId),
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    BirthDate = personDto.BirthDate
                };

                await _mongoRepository.ReplaceOneAsync(person);

                return await FindByIdAsync(personDto.PersonId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when Edit Person  :  " + ex.Message);
            }
        }

        private async Task<List<object>> PersonInsertList(List<Person> listPerson)
        {
            var listPersonViewModel = new List<object>();

            foreach (var person in listPerson)
            {
                listPersonViewModel.Add(

                        new PersonViewModel()
                        {
                            PersonId = person.Id.ToString(),
                            FirstName = person.FirstName,
                            BirthDate = person.BirthDate,
                            LastName = person.LastName

                        }
                    );
            }

            return listPersonViewModel;

        }

      
    }
}
