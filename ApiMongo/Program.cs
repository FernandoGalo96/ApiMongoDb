using ApiMongo.Data.Configurations;
using ApiMongo.Data.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configurar as configurações do MongoDB
builder.Services.Configure<DataBaseConfig>(
    builder.Configuration.GetSection(nameof(DataBaseConfig))
);

// Adicionar cliente MongoDB como serviço singleton
builder.Services.AddSingleton<IDataBaseConfig>(sp => sp.GetRequiredService<IOptions<DataBaseConfig>>().Value);
builder.Services.AddSingleton<ITarefasRepository, TarefasRepository>();

// Adicionar suporte para controladores (mesmo sem MVC completo)
builder.Services.AddControllers();

// Adicionar serviços do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapear controladores para que as rotas sejam detectadas
app.MapControllers();

app.Run();