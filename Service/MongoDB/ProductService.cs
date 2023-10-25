using System;
using WebApiMongoDB.Controllers.Dtos;
using WebApiMongoDB.Models.Entities;
using WebApiMongoDB.Models.Views;
using WebApiMongoDB.Models.ViewsModels;
using WebApiMongoDB.Repository.MongoDB_g;

namespace WebApiMongoDB.Service.MongoDB
{
    public class ProductService : IMongoDBService<ProductDto>
    {

        private readonly IMongoRepository<Product> _mongoRepository;

        public ProductService(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public Task<bool> AddMany(List<ProductDto> listObj)
        {
            throw new NotImplementedException();
        }

        public async Task<object> AddOne(ProductDto objDto)
        {
            try
            {
                if (objDto != null)
                {
                    var product = new Product()
                    {
                        Name = objDto.Name,
                        Description = objDto.Description,
                        Price = objDto.Price,
                        ValidityDate = objDto.ValidityDate,
                    };

                    _mongoRepository.InsertOne(product);

                    return product;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Error when inserting Product :" + ex.Message);
            }

            return default;
        }

        public Task<bool> DeleteMany()
        {
            throw new NotImplementedException();
        }

        public Task<object> DeleteOne(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> FindByIdAsync(string id)
        {
            var product = new Product();

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    product = await _mongoRepository.FindByIdAsync(id);
                }
                else if (product == null) { return null; }

                return new ProductViewModel()
                {
                    ProductId = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ValidityDate = product.ValidityDate

                };
            }
            catch (Exception ex)
            {

                throw new Exception("Error when find Product :" + ex.Message);
            }
        }

        public async Task<List<object>> GetCollection()
        {
            var listProduct = new  List<Product>();
            try
            {

                listProduct = _mongoRepository.FilterBy(x => x.Id != null).ToList();

                if (listProduct.Count > 0)
                {
                    return await ProductInsertList(listProduct);
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error when find  list Products  :  " + ex.Message);
            }
            return default;
        }

        public Task<List<object>> GetCollectionFilter(string expression)
        {
            throw new NotImplementedException();
        }

        public Task<object> ReplaceOneAsync(ProductDto obj)
        {
            throw new NotImplementedException();
        }




        private async Task<List<object>> ProductInsertList(List<Product> listProduct)
        {
            var listProductViewModel = new List<object>();

            foreach (var product in listProduct)
            {
                listProductViewModel.Add(

                        new ProductViewModel()
                        {
                            ProductId = product.Id.ToString(),
                            Description = product.Description,
                            Name = product.Name,
                            Price = product.Price,
                            ValidityDate = product.ValidityDate
                            
                        }
                    );
            }

            return listProductViewModel;

        }
    }
}
