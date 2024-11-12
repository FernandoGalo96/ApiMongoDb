namespace ApiMongo.Models.Dtos;

public class TarefaDto
{
    public string Nome { get; set; }

    public string Detalhes { get; set; }

    public bool? Concluido { get; set; }
}