# Arquitetura do Projeto Fluxus

Este documento apresenta a visão geral da arquitetura adotada no projeto Fluxus.

---

## Padrão Arquitetural

O projeto segue o padrão **Clean Architecture**, com separação clara de responsabilidades entre as camadas:

### Camadas principais:

```
src/
├── Fluxus.Domain           → Entidades e contratos de domínio
├── Fluxus.Application      → Casos de uso, handlers, comandos, regras de negócio
├── Fluxus.ORM              → Contexto EF Core, mapeamentos e seed
├── Fluxus.WebApi           → Controllers, DTOs, validações e exposição da API
├── Fluxus.Common           → Componentes compartilhados (log, validação, segurança)
├── Fluxus.IoC              → Registro modular de dependências
└── tests/Fluxus.Tests      → Testes automatizados
```

---

## Comunicação entre camadas

- A WebApi consome a Application via **MediatR** (Command → Handler)
- A Application depende apenas de **interfaces** (ex: `IUserRepository`)
- A ORM implementa as interfaces e usa **EF Core** para persistência
- As configurações e injeções são feitas via projeto **IoC**, separando a composição do domínio

---

## Segurança

- Autenticação baseada em JWT
- Senhas com `BCrypt`
- Claims no token: `sub`, `email`, `name`, `role`
- Middleware de exceções captura `UnauthorizedAccessException` e retorna 401

---

## Validação

- `FluentValidation` com comportamento padrão via Pipeline do MediatR
- Middleware global trata validações e regras de negócio com respostas padronizadas

---

## Extensibilidade

- Fácil adicionar novas features com:
  - `Command`, `Handler`, `Validator`, `Result` na Application
  - `Request`, `Response`, `Validator`, `Controller` na WebApi
- AutoMapper para transformação entre camadas
- Modularização com AutoRegister via `DependencyResolver`

---

## Pronta para evolução

- Suporte a testes com injeção de dependência
- Separação clara de responsabilidades
- Configuração segura com `user-secrets`
- Aplicação de boas práticas (SOLID, CQRS, SRP, DRY)

---

Esse modelo garante escalabilidade, manutenção facilitada e organização clara — alinhado com práticas de arquitetura modernas em .NET.
