using ApiMongo.Models;

namespace ApiMongo.Data.Repositories;

public interface ITarefasRepository
{
    void Adicionar(Tarefa tarefa);

    Task Atualizar(string id, Tarefa tarefa);

    Task<IEnumerable<Tarefa>> BuscarTarefas();

    Task<Tarefa> BuscarTarefasPorId(string id);

    Task Remover(string id);
}