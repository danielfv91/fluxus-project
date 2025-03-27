# Fluxus

Fluxus é um projeto de exemplo para controle de fluxo de caixa com base em uma arquitetura limpa, modular e escalável utilizando .NET.

## Estrutura do Projeto

```
src/
├── Fluxus.WebApi/         # API principal
├── Fluxus.Application/    # Casos de uso e serviços de aplicação
├── Fluxus.Domain/         # Entidades e interfaces de domínio
├── Fluxus.ORM/            # Acesso a dados, EF Core, mapeamentos e seed
├── Fluxus.Common/         # Logging, validações, segurança, middlewares
├── Fluxus.IoC/            # Registro modular de dependências
tests/
└── Fluxus.Tests/          # Testes de unidade e integração
```

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- MediatR
- AutoMapper
- Entity Framework Core
- FluentValidation
- Serilog
- JWT (autenticação)
- PostgreSQL

## Como executar o projeto

Para um guia completo de configuração e execução local, acesse:

[`docs/setup.md`](docs/setup.md)

## Autenticação

- Autenticação via JWT
- Seed de usuário admin controlado por configuração
- Passwords com BCrypt
- Proteção de endpoints com `[Authorize]`

Para mais informações sobre o fluxo de autenticação, veja:

[`docs/auth.md`](docs/auth.md)

## Arquitetura

Clean Architecture com separação clara de camadas e responsabilidades.

[`docs/architecture.md`](docs/architecture.md)

## Licença

Este projeto está sob a licença MIT.
