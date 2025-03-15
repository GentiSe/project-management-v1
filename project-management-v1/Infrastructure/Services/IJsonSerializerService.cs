namespace project_management_v1.Infrastructure.Services
{
    public interface IJsonSerializerService
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string json);
    }
}
