using ApiMongo.Data.Repositories;
using ApiMongo.Models;
using ApiMongo.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiMongo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private ITarefasRepository _tarefasRepository;

    public TarefasController(ITarefasRepository tarefasRepository)
    {
        _tarefasRepository = tarefasRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tarefas = await _tarefasRepository.BuscarTarefas();
        return Ok(tarefas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var tarefa = await _tarefasRepository.BuscarTarefasPorId(id);
        if (tarefa == null) return NotFound();
        return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Post([FromBody] TarefaDto novaTarefa)
    {
        var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);

        _tarefasRepository.Adicionar(tarefa);

        return Created("", tarefa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] TarefaDto tarefaAtualizada)
    {
        var tarefaExistente = await _tarefasRepository.BuscarTarefasPorId(id);

        if (tarefaExistente == null) return NotFound();

        tarefaExistente.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);
        await _tarefasRepository.Atualizar(id, tarefaExistente);
        return Ok(tarefaExistente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var tarefaExistente = await _tarefasRepository.BuscarTarefasPorId(id);

        if (tarefaExistente == null) return NotFound();

        await _tarefasRepository.Remover(id);

        return NoContent();
    }
}