# Diagrama Textual da Solução (Projeto Fluxus)

Este diagrama textual apresenta uma visão arquitetural clara da solução desenvolvida para o projeto Fluxus, com base na estrutura de pastas e responsabilidades do sistema.

---

## Visão Geral em Camadas

```
Usuário (Requisições HTTP)
    │
    ▼
┌────────────────────────────────────┐
│           Fluxus.WebApi            │ ← Controllers (Endpoints REST)
│  - Validação com FluentValidation  │
│  - Mapeamento com AutoMapper       │
│  - Middleware de Timeout / Errors  │
└────────────────────────────────────┘
    │
    ▼ (via MediatR)
┌────────────────────────────────────┐
│        Fluxus.Application          │ ← CQRS
│  - Commands / Queries              │
│  - Handlers (Casos de uso)         │
│  - Regras de negócio orquestradas  │
└────────────────────────────────────┘
    │
    ▼
┌────────────────────────────────────┐
│          Fluxus.Domain             │ ← DDD
│  - Entidades e Value Objects       │
│  - Repositórios (interfaces)       │
│  - Serviços de domínio             │
│  - Enumerações / Exceções          │
└────────────────────────────────────┘
    │
    ▼
┌────────────────────────────────────┐
│           Fluxus.ORM               │ ← Infraestrutura
│  - Entity Framework Core           │
│  - Migrations e Context            │
│  - Repositórios (implementações)   │
└────────────────────────────────────┘
    │
    ▼
┌────────────────────────────────────┐
│         PostgreSQL Database        │
└────────────────────────────────────┘
```

---

## Componentes Transversais

```
Fluxus.Common
├── Reporting         → Geração de PDF com QuestPDF
├── Security          → Interface IAuthenticatedUser
├── Constants         → Mensagens padrão
```

```
Fluxus.WebApi.Middlewares
├── RequestTimeoutMiddleware.cs  → Controla tempo de execução
├── ValidationExceptionMiddleware.cs  → Captura falhas de validação
```

```
Caching
├── IMemoryCache aplicado no relatório
├── Key baseada em: userId + datas
```

---

## Testes Automatizados

```
tests/Fluxus.Unit
├── Application/Features/... → Testes de Handlers
├── WebApi/Controllers/...   → Testes de Controllers
├── Validators/...           → Testes de validação
├── TestData/...             → Builders com Bogus
```

- Testes com xUnit, Moq e FluentAssertions
- Uso extensivo de dados fake com Bogus
- Cobertura de cenários felizes e falhas

---

## Comunicação e Fluxo

- Controllers → validam e convertem para comandos/queries
- MediatR → envia para handlers corretos
- Handlers → aplicam regras, chamam repositórios
- Repositórios → acessam banco via EF Core
- Resposta → volta mapeada como DTO + `ApiResponse`

---

## Conclusão

Este diagrama textual resume os principais blocos arquiteturais e fluxos da aplicação. Ele complementa o diagrama visual e pode ser usado na documentação para esclarecer a divisão de responsabilidades e padrões aplicados no projeto.
