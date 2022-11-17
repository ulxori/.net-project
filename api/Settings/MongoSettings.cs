namespace api.Settings;

public class MongoSettings
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
    public string CollectionName { get; }

    public MongoSettings()
    {
        ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                           ?? "mongodb://root:root@localhost:27017";
        CollectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME")
                         ?? "SPINACH_COLLECTION";
        DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")
                       ?? "SPINACH_DB";
    }
}