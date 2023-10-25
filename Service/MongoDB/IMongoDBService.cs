

namespace WebApiMongoDB.Service.MongoDB
{
    public interface IMongoDBService<T>
    {
        Task<object> AddOne(T obj);
        Task<bool> AddMany(List<T> listObj);
        Task<object> FindByIdAsync(string id);
        Task<object> ReplaceOneAsync(T obj);
        Task<object> DeleteOne(string id);
        Task<bool> DeleteMany();
        Task<List<object>> GetCollectionFilter(string expression);
        Task<List<object>> GetCollection();
    }
}
