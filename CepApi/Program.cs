using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços à aplicação
builder.Services.AddControllers();

// Configura o CORS para permitir qualquer origem, método e cabeçalho
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Registra o HttpClient como um serviço Singleton para chamadas externas
builder.Services.AddSingleton<HttpClient>();

var app = builder.Build();

// Configura o pipeline de requisição HTTP
app.UseCors("AllowAll"); // Habilita o CORS

// Configura o redirecionamento para HTTPS apenas em ambientes que não sejam de Desenvolvimento
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // Redireciona para HTTPS
}

app.MapControllers(); // Mapeia os controllers, como o CepController

app.Run();

