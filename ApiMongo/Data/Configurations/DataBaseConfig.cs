namespace ApiMongo.Data.Configurations;

public class DataBaseConfig : IDataBaseConfig
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}