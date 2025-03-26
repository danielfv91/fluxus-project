# Fluxus

Fluxus é um projeto de exemplo para controle de fluxo de caixa com base em uma arquitetura limpa, modular e escalável utilizando .NET.

## Estrutura do Projeto

```
src/
├── Fluxus.WebApi/         # API principal
├── Fluxus.Application/    # Casos de uso e serviços de aplicação
├── Fluxus.Domain/         # Entidades e interfaces de domínio
├── Fluxus.Infrastructure/ # Acesso a dados, EF Core, repositórios
tests/
└── Fluxus.Tests/          # Testes de unidade e integração
```

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- FluentValidation
- Serilog (logging)
- Swagger (documentação de API)
- xUnit (testes)
- Docker

## Como executar o projeto localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (opcional)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code

### Passos

1. Clone o repositório:
```bash
git clone https://github.com/danielfv91/fluxus.git
cd fluxus
```

2. Restaure os pacotes:
```bash
dotnet restore
```

3. Execute o projeto:
```bash
dotnet run --project src/Fluxus.WebApi
```

4. Acesse o Swagger:
```
http://localhost:5119/swagger
```
