namespace api.Settings;

public class MongoSettings
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
    public string CollectionName { get; }

    public MongoSettings()
    {
        ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                           //?? "mongodb://root:root@mongo:27017";
                           ?? "mongodb://root:student@actina15.maas:27017";
        CollectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME")
                         ?? "SPINACH_COLLECTION";
        DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")
                       ?? "172172_DB";
    }
}