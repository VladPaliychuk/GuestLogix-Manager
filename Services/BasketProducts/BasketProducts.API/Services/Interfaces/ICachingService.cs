namespace BasketProducts.API.Services.Interfaces
{
    public interface ICachingService
    {
        T GetData<T>(string key);
        T SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
