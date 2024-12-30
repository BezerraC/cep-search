# Cep Search Project

Este projeto consiste em uma aplicação que busca dados de um CEP (Código de Endereçamento Postal) por meio de uma API, utilizando .NET 6 para o backend e Vue.js com Vuetify 3 para o frontend. Ele também utiliza Docker para facilitar a execução do ambiente de desenvolvimento.

## Tecnologias Utilizadas

- **Backend**: .NET 6 (C#)
- **Frontend**: Vue.js com Vuetify 3
- **Containerização**: Docker (Docker Compose)
- **API de CEP**: ViaCEP (https://viacep.com.br/)

## Funcionalidades

- O backend realiza a consulta ao ViaCEP, que retorna informações sobre o endereço relacionado a um determinado CEP.
- O sistema utiliza um cache local para armazenar as informações de CEP por 5 minutos, evitando consultas repetidas à API externa.
- O frontend permite que o usuário digite um CEP, faça a consulta e visualize os dados do endereço em um formato amigável.

## Backend (.NET 6)

**CepController**: Controlador que expõe a API para consulta de CEP. Utiliza cache para armazenar os resultados da consulta à API ViaCEP por 5 minutos. A consulta ao CEP é feita através do HttpClient e os dados retornados são armazenados no cache.

### Exemplo de código (CepController):

```csharp
[HttpGet("{cep}")]
public async Task<IActionResult> Get(string cep)
{
    // Verifica se o CEP já está no cache
    if (_cepCache.ContainsKey(cep) && _cepCache[cep].timestamp.AddMinutes(5) > DateTime.Now)
    {
        return Ok(DeserializeCepData(_cepCache[cep].data));
    }
    // Lógica para consulta externa
}
```

## Frontend (Vue.js + Vuetify)
**CepSearch.vue**: Componente Vue.js para buscar e exibir informações sobre o CEP.
Utiliza Vuetify para a criação de uma interface de usuário moderna e responsiva.
A consulta ao backend é feita através de fetch e o resultado é exibido no frontend.

## Docker Compose
O projeto utiliza Docker Compose para orquestrar os containers do backend e frontend.
O backend está configurado para rodar na porta 5220 (HTTP) e 7220 (HTTPS), enquanto o frontend está na porta 8080.

# Como Rodar o Projeto

## Pré-requisitos
Docker e Docker Compose instalados.
.NET SDK 6.0 (caso queira rodar o backend localmente fora do Docker).

## Rodando o Projeto com Docker
Clonar o repositório:
```bash
git clone https://github.com/BezerraC/cep-search.git
cd cep-search
```

## Construir e rodar os containers:
```bash
docker-compose up --build
```

Isso irá construir as imagens do Docker para o frontend e o backend, além de iniciar os containers.

### Acessar o frontend:

Abra o navegador e acesse http://localhost:8080 para utilizar a interface de busca de CEP.

### Acessar o backend (API):

A API estará disponível em http://localhost:5220/api/cep/{cep}, onde você pode testar diretamente usando o navegador

### Testando a API
Você pode testar a API diretamente através do frontend ou utilizando ferramentas como curl ou Postman.

Exemplo com curl:
```curl
http://localhost:5220/api/cep/01001000
```

Este comando retornará os dados do CEP informado (no caso, o CEP 01001000 de São Paulo).

## Licença
Este projeto está licenciado sob a <a href="https://github.com/BezerraC/cep-search/blob/main/LICENSE">MIT License</a>.