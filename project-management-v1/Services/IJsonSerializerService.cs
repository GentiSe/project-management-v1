namespace project_management_v1.Services
{
    public interface IJsonSerializerService
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string json);
    }
}
