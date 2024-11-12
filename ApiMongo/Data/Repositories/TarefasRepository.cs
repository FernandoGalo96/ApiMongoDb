using ApiMongo.Data.Configurations;
using ApiMongo.Models;
using MongoDB.Driver;

namespace ApiMongo.Data.Repositories;

public class TarefasRepository : ITarefasRepository
{
    private readonly IMongoCollection<Tarefa> _tarefas;

    public TarefasRepository(IDataBaseConfig dataBaseConfig)
    {
        var client = new MongoClient(dataBaseConfig.ConnectionString);
        var database = client.GetDatabase(dataBaseConfig.DatabaseName);

        _tarefas = database.GetCollection<Tarefa>("tarefas");
    }

    public void Adicionar(Tarefa tarefa)
    {
        _tarefas.InsertOne(tarefa);
    }

    public async Task Atualizar(string id, Tarefa tarefa)
    {
        await _tarefas.ReplaceOneAsync(t => t.Id == id, tarefa);
    }

    public async Task<Tarefa> BuscarTarefasPorId(string id)
    {
        return await _tarefas.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Tarefa>> BuscarTarefas()
    {
        return await _tarefas.Find(t => true).ToListAsync();
    }

    public async Task Remover(string id)
    {
        await _tarefas.DeleteOneAsync(t => t.Id == id);
    }
}